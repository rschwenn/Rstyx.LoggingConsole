
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows.Threading
Imports System.Threading.Tasks.Dataflow

''' <summary> One single Log job for the LogQueue. </summary>
Public Class LogJob
    
    #Region "Public Fields"
        
        Public ReadOnly Level   As LogLevelEnum = LogLevelEnum.Info
        Public ReadOnly Source  As String = String.Empty
        Public ReadOnly Message As String = String.Empty
        
    #End Region
    
    #Region "Constuctor"
        
        ''' <summary> Creates a new LogJob with all needed information, that cannot be changed afterwards. </summary>
         ''' <param name="LogLevel">   Indicates the severity of this log job (<see cref="LoggingConsole.LogLevelEnum"/>). </param>
         ''' <param name="LogSource">  Indicates the source or origin of this log entry. In general this is the logger name. </param>
         ''' <param name="LogMessage"> A one or multi line message. If empty or <see langword="null"/>, it leads to an empty message line. </param>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="LogSource"/> is <see langword="null"/>. </exception>
        Public Sub New(LogLevel As LogLevelEnum, LogSource As String, LogMessage As String)
            
            If ((LogSource Is Nothing) OrElse (Trim(LogSource) = String.Empty)) Then Throw New System.ArgumentNullException("Source")

            Level   = LogLevel
            Source  = LogSource
            Message = If(LogMessage, String.Empty)
        End Sub
        
    #End Region

End Class

''' <summary> The MessageStore contains the List of messages and maintaining methods. </summary>
 ''' <remarks> 
 ''' <para>
 ''' This class holds the internal List of messages and provides project-internal methods for adding and removing messages.
 ''' </para>
 ''' <para>
 ''' It also provides the <see cref="MessageStore.ErrorLoggedEvent"/>, which raises when a new error is logged.
 ''' </para>
 ''' <para>
 ''' Internally there is one list for each <see cref="LogLevelEnum"/> maintained.
 ''' The reason for this is the very poor performance of CollectionView.Filter.
 ''' </para>
 ''' <para>
 ''' Usually You don't need to deal with this class unless You want to get or set the 
 ''' <see cref="MessageStore.MaxLogLength"/> property, get the <see cref="MessageStore.TotalMessagesCount"/> property
 ''' or want to listen for <see cref="MessageStore.ErrorLoggedEvent"/>.
 ''' </para>
 ''' <para>
 ''' This "model" class inherits "ViewModelBase" for convenience: This way changes of <see cref="MessageStore.MaxLogLength"/> property are broadcasted to binding.
 ''' </para>
 ''' </remarks>
Public Class MessageStore
    Inherits ViewModelBase
    
    #Region "Protected Fields"
        
        Protected ReadOnly InternalLogger As Logger
        
        Protected ReadOnly _LogBox        As LogBox = Nothing
        Protected _HighestLevelInLog      As LogLevelEnum = LogLevelEnum.Debug
        Protected _Messages               As Collection(Of ObservableCollection(Of LogEntry)) = Nothing
        Protected _TotalMessagesCount     As Long = 0
        
        Protected ReadOnly SyncHandle1    As Object
        Protected ReadOnly SyncHandle2    As Object
        Protected ReadOnly SyncHandle3    As Object
        Protected ReadOnly SyncHandle4    As Object

        Protected ReadOnly OnErrorLoggedDeferredAction As DeferredAction
        Protected ReadOnly OnErrorLoggedDelay          As TimeSpan = TimeSpan.FromMilliseconds(300)

        ''' <summary> Delegate for running in the UI thread. </summary>
        Protected ReadOnly AddOneLineDelegate   As Action(Of LogLevelEnum, String, String)

        ''' <summary> Delegate for <see cref="LogJobQueue"/>. </summary>
        Protected ReadOnly ExcuteLogJobDelegate As Action(Of LogJob)

        ''' <summary> The internal Queue of <see cref="LogJob"/>'s. </summary>
        Protected ReadOnly LogJobQueue          As ActionBlock(Of LogJob)
        
    #End Region
    
    #Region "Constuctors"
        
        ''' <summary> Creates a new instance. </summary>
         ''' <param name="parentLogBox"> The <see cref="LogBox"/> instance which holds this MessageStore. </param>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="parentLogBox"/> is <see langword="null"/>. </exception>
        Friend Sub New(parentLogBox As LogBox)
            
            If (parentLogBox Is Nothing) Then Throw New System.ArgumentNullException("parentLogBox")
            
            SyncHandle1 = New Object()
            SyncHandle2 = New Object()
            SyncHandle3 = New Object()
            SyncHandle4 = New Object()
            
            _LogBox = parentLogBox
            _Messages = Me.Messages
            
            AddOneLineDelegate   = AddressOf AddMessage
            ExcuteLogJobDelegate = AddressOf ExcuteLogJob

            LogJobQueue    = New ActionBlock(Of LogJob)(ExcuteLogJobDelegate)
            
            InternalLogger = LogBox.GetLogger("LogBox.MessageStore")

            OnErrorLoggedDeferredAction = New DeferredAction(AddressOf OnErrorLogged)

            'Creates endless loop (isn't worth to investigate ;-)
            'InternalLogger.logDebug(My.Resources.Resources.MessageStore_Initialized)
        End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Returns the LogBox instance which this MessageStore is connected to. </summary>
         ''' <remarks> This is for internal use only. </remarks>
        Friend ReadOnly Property LogBox() As LogBox
            Get
                Return _LogBox
            End Get
        End Property
        
        ''' <summary> Returns the messages (one Collection per log level). </summary>
        Public ReadOnly Property Messages() As Collection(Of ObservableCollection(Of LogEntry))
            Get
                SyncLock (SyncHandle1)
                    If (_Messages Is Nothing) Then
                        _Messages = New Collection(Of ObservableCollection(Of LogEntry))
                        _Messages.Add(New ObservableCollection(Of LogEntry))
                        _Messages.Add(New ObservableCollection(Of LogEntry))
                        _Messages.Add(New ObservableCollection(Of LogEntry))
                        _Messages.Add(New ObservableCollection(Of LogEntry))
                    End If
                    Messages = _Messages
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns the total number of messages logged so far. </summary>
        Public ReadOnly Property TotalMessagesCount() As Long
            Get
                TotalMessagesCount = _TotalMessagesCount
            End Get
        End Property
        
        ''' <summary> Indicates the highest log level of all currently available messages. </summary>
        Public ReadOnly Property HighestLevelInLog() As LogLevelEnum
            Get
                HighestLevelInLog = _HighestLevelInLog
            End Get
        End Property
        
        '' <summary> Indicates whether or not there has been logged any errors. </summary>
         '' <remarks> This is used internally only for initializing the console after meassages has been logged already. </remarks>
        'Friend ReadOnly Property hasErrors() As Boolean
        '    Get
        '        hasErrors = (_HighestLevelInLog = LogLevelEnum.Error)
        '    End Get
        'End Property
        
        
        ''' <summary> Gets or sets the maximum number of lines allowed for the list of log messages. </summary>
         ''' <remarks> 
         ''' <para>
         ''' If the log exceeds this length, the oldest messages are removed from the list.
         ''' </para>
         ''' <para>
         ''' This is an "application setting".
         ''' </para>
         ''' </remarks>
        Public Property MaxLogLength() As UInteger
            Get
                MaxLogLength = My.Settings.MaxLogLength
            End Get
            Set(value As UInteger)
                SyncLock (SyncHandle2)
                    If (Not (value = My.Settings.MaxLogLength)) Then
                        My.Settings.MaxLogLength = value
                        LimitLog()
                    End If 
                End SyncLock
            End Set
        End Property
        
    #End Region
    
    #Region "Methods"
        
        ''' <summary> Enqueues a log job to the <see cref="MessageStore"/>'s  <see cref="LogJobQueue"/>. </summary>
         ''' <param name="Job"> The LogJob to enqueue. </param>
         ''' <remarks>
         ''' The jobs of <see cref="LogJobQueue"/> will be processed in incoming order in a separate background thread by <see cref="ExcuteLogJob"/>. 
         ''' </remarks>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="Job"/> is <see langword="null"/>. </exception>
        Protected Friend Sub EnqueueLogJob(ByVal Job As LogJob)
            If (Job Is Nothing) Then Throw New System.ArgumentNullException("Job")
            LogJobQueue.Post(Job)
        End Sub
        
        ''' <summary> Executes a log job, that is adding a message of given level to the MessageStore. </summary>
         ''' <param name="Job"> The LogJob </param>
         ''' <remarks>
         ''' This method is invoked by the <see cref="LogJobQueue"/> in a background thread. 
         ''' It splits the job's message into single lines and adds them to MessageStore by a delegate running in UI thread.
         ''' </remarks>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="Job"/> is <see langword="null"/>. </exception>
        Protected Sub ExcuteLogJob(ByVal Job As LogJob)

            If (Job Is Nothing) Then Throw New System.ArgumentNullException("Job")
            Try
                Dim MsgLines() As String
                
                ' Split message into single lines.
                If (Not String.IsNullOrEmpty(Job.Message)) Then
                    MsgLines = Job.Message.Split({Constants.vbNewLine}, StringSplitOptions.None)
                Else
                    ' Add the empty message.
                    MsgLines = {String.Empty}
                End If
                
                ' Get matching dispatcher.
                Dim LogDispatcher As Dispatcher = If(Me.LogBox.Console.ConsoleViewExists, Me.LogBox.Console.ConsoleView.Dispatcher, Dispatcher.CurrentDispatcher)
                
                ' Create the Message: Add each single line of the message as item to the Log.
                For i As Long = MsgLines.GetLowerBound(0) To MsgLines.GetUpperBound(0)
                    LogDispatcher.Invoke(AddOneLineDelegate, Job.Level, Job.Source, MsgLines(i))
                Next

            Catch ex As System.Exception
                System.Diagnostics.Debug.Fail("ExcuteLogJob() failed!")
            End Try
        End Sub
        
        ''' <summary> This is the one and only method which really adds a new message to the log. </summary>
         ''' <param name="Level">   This is passed to the <see cref="LoggingConsole.LogEntry"/> constructor. </param>
         ''' <param name="Source">  This is passed to the <see cref="LoggingConsole.LogEntry"/> constructor. </param>
         ''' <param name="Message"> This is passed to the <see cref="LoggingConsole.LogEntry"/> constructor. </param>
         ''' <remarks> This should run in UI thread. It's invoked by <see cref="ExcuteLogJob"/>. </remarks>
        Protected Sub AddMessage(ByVal Level As LogLevelEnum, ByVal Source As String, ByVal Message As String)
            SyncLock (SyncHandle3)
                ' Adds a new LogMessage with the specified level to every relevant Messages Collection.
                _TotalMessagesCount += 1
                Dim oLogEntry as LogEntry = New LogEntry(_TotalMessagesCount, Level, Source, Message)
                
                _Messages(LogLevelEnum.Debug).Add(oLogEntry)
                If (Not (Level < LogLevelEnum.Info))    Then _Messages(LogLevelEnum.Info).Add(oLogEntry)
                If (Not (Level < LogLevelEnum.Warning)) Then _Messages(LogLevelEnum.Warning).Add(oLogEntry)
                If (Not (Level < LogLevelEnum.Error))   Then _Messages(LogLevelEnum.Error).Add(oLogEntry)
                
                ' Cut the Log if it exceeds Me.MaxLogLength.
                LimitLog()
                
                ' Update Me.HighestLevelInLog.
                If (Level > _HighestLevelInLog) Then
                    _HighestLevelInLog = Level
                    MyBase.OnPropertyChanged("HighestLevelInLog")
                End If
                
                ' If an error was logged, then fire the ErrorLogged event.
                If (Level = LogLevelEnum.Error) then
                    'Me.OnErrorLogged()
                    OnErrorLoggedDeferredAction.Defer(OnErrorLoggedDelay)
                End If
            End SyncLock
        End Sub
        
        ''' <summary> Clears the entire Log. </summary>
        Protected Friend Sub ClearLog()
            SyncLock (SyncHandle4)
                For Each LogList as ObservableCollection(Of LogEntry) in _Messages
                    LogList.Clear
                Next
                _HighestLevelInLog = LogLevelEnum.Debug
                _TotalMessagesCount = 0
                InternalLogger.LogDebug(My.Resources.Resources.MessageStore_LogCleared)
            End SyncLock
        End Sub
        
        ''' <summary> Removes old messages from the Log to respect the <see cref="MaxLogLength"/> Property. </summary>
        Protected Friend Sub LimitLog()
            'Cuts the Collection if it exceeds the maximum number of lines.
            Const cutPercent As Long = 20  'part to cut away in percent (ca.)

            ' No SyncLock, because calls to this methods are already synchronized.
            
            Dim OldLen  As UInteger
            Dim MaxLen  As UInteger
            Dim CutLen  As UInteger
            
            MaxLen = Me.MaxLogLength
            
            if (MaxLen > 0) then
                For each LogList as ObservableCollection(Of LogEntry) in _Messages
                    OldLen = LogList.Count
                    If (OldLen > MaxLen) Then
                      CutLen = CUInt((MaxLen * cutPercent / 100) + OldLen - MaxLen)
                      
                      For i As UInteger = 1 To CutLen
                        LogList.RemoveAt(0)
                      Next
                      
                      InternalLogger.LogDebug(String.Format(My.Resources.Resources.MessageStore_LimitLogResult, OldLen, LogList.Count))
                    End If
                Next
            End If
        End Sub
        
    #End Region
    
    #Region "Events"
        
        ''' <summary> Raises when an error message is logged. </summary>
         ''' <remarks> Using a predefined EventHandler delegate without event args. </remarks>
        Public Event ErrorLogged As System.EventHandler
        
        ''' <summary> Raises the ErrorLogged event. </summary>
         ''' <remarks> This event indicates that a new error message has been added to the log. </remarks>
        Private Sub OnErrorLogged()
            Try
                RaiseEvent ErrorLogged(Me, New System.EventArgs)
            Catch ex As System.Exception
                InternalLogger.LogError(ex, "OnErrorLogged: " & My.Resources.Resources.Global_ErrorInEventHandler)
            End Try
        End Sub
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
