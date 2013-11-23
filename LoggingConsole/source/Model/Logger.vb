
Imports System

''' <summary> The Logger provides public methods to log messages. </summary>
 ''' <remarks>
 ''' <para>
 ''' The Logger cannot instanciated directly. Instead, use the static <see cref="LogBox.getLogger"/> method.
 ''' </para>
 ''' <para>
 ''' The Logger does not only provide public methods to log messages.
 ''' It also has a <see cref="Logger.Name"/> which is used to indicate the source of the message.
 ''' In fact, it's Name is the only reason for the Logger class to exist separately. 
 ''' This seems to be the most convenient way to pass a message source description
 ''' without having to explicitely determine it every time a message is logged.
 ''' </para>
 ''' <para>
 ''' Multithreading: If the <see cref="ConsoleView"/> exists, the Logger adds messages to the <see cref="MessageStore"/> 
 ''' using ConsoleView's dispatcher (UI thread). So it's safe to use the Logger from another thread.
 ''' </para>
 ''' </remarks>
Public Class Logger
    
    #Region "Private Fields"
        
        Private _LogBox             As LogBox = Nothing
        Private _Name               As String = Nothing
        
        Private ConsoleDispatcher   As System.Windows.Threading.Dispatcher = Nothing
        Private Operation           As System.Windows.Threading.DispatcherOperation = Nothing
        Private AddOneLineDelegate  As addOneLine = Nothing
        
    #End Region
    
    #Region "Constuctors"
        
        ''' <summary> Creates a Logger with the given name. </summary>
         ''' <param name="LoggerName">   The desired Name, may be empty. </param>
         ''' <param name="parentLogBox"> The LogBox instance which holds this Logger. </param>
         ''' <remarks> 
         ''' This is called internally only. To get a Logger use the static <see cref="LogBox.getLogger"/> method.
         ''' </remarks>
         ''' <exception cref="T:System.ArgumentNullException"> <paramref name="parentLogBox"/> is <see langword="null"/>. </exception>
        Friend Sub New(LoggerName As String, parentLogBox As LogBox)
            
            If (parentLogBox Is Nothing) Then Throw New ArgumentNullException("parentLogBox")
            
            _Name = LoggerName
            _LogBox = parentLogBox
            
            ' Delegate for running in the UI thread.
            AddOneLineDelegate = New addOneLine(AddressOf addMessageLine)
        End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Returns the LogBox instance which this Logger is connected to. </summary>
         ''' <remarks> This is for internal use only. </remarks>
        Friend ReadOnly Property LogBox() As LogBox
            Get
                Return _LogBox
            End Get
        End Property
        
        ''' <summary> Returns this Logger's Name. </summary>
         ''' <remarks> The Name is used to indicate the source of the message. </remarks>
        Public ReadOnly Property Name() As String
            Get
                Name = _Name
            End Get
        End Property
        
    #End Region
    
    #Region "Methods"
        
        ''' <summary> Adds a message of Error level to the log and inserts info about the passed exception. </summary>
         ''' <param name="ex">      The Exception to log. If <see langword="null"/>, only <paramref name="Message"/> is added. </param>
         ''' <param name="Message"> The message itself (may contain line breaks). If <see langword="null"/>, no message is added. </param>
         ''' <remarks>
         ''' The exception's stack trace is added to DEBUG log.
         ''' All inner exceptions will be logged.
         ''' </remarks>
        Public Sub logError(ex As System.Exception, ByVal Message As String)
            ' Log exception.
            If (ex IsNot Nothing) Then
                
                ' Gather exception source information.
                Dim TargetName          As String = "?"
                Dim TargetDeclaringType As String = ex.Source
                'Dim TargetReflectedType As String = Nothing
                
                If (ex.TargetSite IsNot Nothing) Then
                    TargetName = ex.TargetSite.Name
                    If (ex.TargetSite.DeclaringType IsNot Nothing) Then TargetDeclaringType = ex.TargetSite.DeclaringType.FullName
                    'If (ex.TargetSite.ReflectedType IsNot Nothing) Then TargetReflectedType = ex.TargetSite.ReflectedType.FullName
                End If
                
                ' Type-dependent error message.
                If (TypeOf ex Is System.IO.FileNotFoundException) Then
                    logMessage(LogLevelEnum.Error, String.Format("{0} in {1}/{2}():{3}{4} ({5})", ex.GetType().Name, TargetDeclaringType, getTargetName(ex), vbNewLine, ex.Message, CType(ex, System.IO.FileNotFoundException).FileName))
                    
                ElseIf (TypeOf ex Is System.Data.OleDb.OleDbException) Then
                    Dim OleDbEx As System.Data.OleDb.OleDbException = CType(ex, System.Data.OleDb.OleDbException)
                    logMessage(LogLevelEnum.Error, String.Format("{0} in {1}/{2}() (FehlerCode={3}, FehlerAnzahl={4}):", ex.GetType().Name, TargetDeclaringType, getTargetName(ex), OleDbEx.ErrorCode, OleDbEx.Errors.Count))
                    For Each OleDbErr As System.Data.OleDb.OleDbError In OleDbEx.Errors
                        logMessage(LogLevelEnum.Error, String.Format(" - {0}: {1}", OleDbErr.Source, OleDbErr.Message))
                    Next
                Else
                    logMessage(LogLevelEnum.Error, String.Format("{0} in {1}/{2}():{3}{4}", ex.GetType().Name, TargetDeclaringType, getTargetName(ex), vbNewLine, ex.Message))
                End If
                
                ' Test **************
                'logMessage(LogLevelEnum.Debug, String.Format("   (TargetReflectedType = {0},  TargetDeclaringType = {1})", TargetReflectedType, TargetDeclaringType))
                ' Test **************
                
                ' Stack trace only with debug level.
                logMessage(LogLevelEnum.Debug, ex.StackTrace)
                
                ' Discover inner exceptions.
                If (TypeOf ex Is AggregateException) Then
                    For Each InnerEx As Exception In CType(ex, AggregateException).InnerExceptions
                        logError(InnerEx, Nothing)
                    Next
                ElseIf (ex.InnerException IsNot Nothing) Then
                    ' Recursive discover inner exceptions - last first :-(.
                    logError(ex.InnerException, Nothing)
                End If 
            End If
            
            ' Log given Message.
            'If (Not String.IsNullOrEmpty(Message)) Then logMessage(LogLevelEnum.Error, Message)
            If (Message IsNot Nothing) Then logMessage(LogLevelEnum.Error, Message)
        End Sub
        
        ''' <summary> Adds a message of Error level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub logError(ByVal Message As String)
            logMessage(LogLevelEnum.Error, Message)
        End Sub
        
        ''' <summary> Adds a message of Warning level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub logWarning(ByVal Message As String)
            logMessage(LogLevelEnum.Warning, Message)
        End Sub
        
        ''' <summary> Adds a message of Info level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub logInfo(ByVal Message As String)
            logMessage(LogLevelEnum.Info, Message)
        End Sub
        
        ''' <summary> Adds a message of Debug level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub logDebug(ByVal Message As String)
            logMessage(LogLevelEnum.Debug, Message)
        End Sub
        
    #End Region
    
    #Region "Private Members"
        
        ''' <summary> Adds a message of given level to the MessageStore. </summary>
         ''' <param name="Level"> The LogLevel </param>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
         ''' <remarks> If the message contains line breaks, every contained line is logged as a separate LogEntry. </remarks>
        Private Sub logMessage(ByVal Level As LogLevelEnum, ByVal Message As String)
            Try
                Dim MsgLines() As String
                
                ' Get the ConsoleView's dispatcher.
                Dim ConsoleViewExists = _LogBox.Console.ConsoleViewExists
                If (ConsoleViewExists) Then ConsoleDispatcher = _LogBox.Console.ConsoleView.Dispatcher
                
                ' Split message into single lines.
                If (Not String.IsNullOrEmpty(Message)) Then
                    MsgLines = Message.Split({Constants.vbNewLine}, StringSplitOptions.None)
                Else
                    ' Add the empty message.
                    MsgLines = {String.Empty}
                End If
                
                ' Create the Message: Add each single line of the message as item to the Log.
                For i As Long = MsgLines.GetLowerBound(0) To MsgLines.GetUpperBound(0)
                    
                    If (ConsoleViewExists) Then
                        Dim Operation As Object = ConsoleDispatcher.Invoke(AddOneLineDelegate, Level, MsgLines(i))
                    Else
                        AddOneLineDelegate.Invoke(Level, MsgLines(i))
                    End If
                Next
            Catch ex As System.Exception
                System.Diagnostics.Debug.Fail("logMessage() failed!")
            End Try
        End Sub
        
        ''' <summary> Delegate for adding one line of the message to the log. => Intended for invoking in UI thread. </summary>
         ''' <param name="Level">       The log Level. </param>
         ''' <param name="MessageLine"> The text of the message line. </param>
        Private Delegate Sub addOneLine(ByVal Level As LogLevelEnum, ByVal MessageLine As String)
        
        ''' <summary> Adds one line of the message to the log. It's encapsulated for use as <see cref="addOneLine"/> Delegate. </summary>
         ''' <param name="Level">       The log Level. </param>
         ''' <param name="MessageLine"> The text of the message line. </param>
        Private Sub addMessageLine(ByVal Level As LogLevelEnum, ByVal MessageLine As String)
            _LogBox.MessageStore.addMessage(Level, Me.Name, MessageLine)
        End Sub
        
        ''' <summary> Returns the Logger instance with the empty <see cref="LoggingConsole.Logger.Name"/>. </summary>
         ''' <returns> The requested <see cref="LoggingConsole.Logger"/> instance </returns>
         ''' <remarks> 
         ''' If the Logger with the empty Name doesn't exist yet, 
         ''' it will be created and stored in the internal set of Loggers. 
         ''' </remarks>
        Private Function getTargetName(ex as Exception) As String
            Return If(ex.TargetSite Is Nothing, "?", ex.TargetSite.Name)
        End Function
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
