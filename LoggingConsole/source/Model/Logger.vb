﻿
Imports System
Imports System.Threading.Tasks
Imports System.Windows.Threading

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
    
    #Region "Protected Fields"
        
        Protected ReadOnly _Name                As String = Nothing

        Private ReadOnly SyncHandle1            As Object
        Private ReadOnly SyncHandle2            As Object

        ' Quick access links.
        Protected ReadOnly _LogBox              As LogBox = Nothing
        Protected _Console                      As Console = Nothing
        Protected _MessageStore                 As MessageStore = Nothing
        
    #End Region
    
    #Region "Constuctors"
        
        ''' <summary> Creates a Logger with the given name. </summary>
         ''' <param name="LoggerName">   The desired Name, may be empty. </param>
         ''' <param name="ParentLogBox"> The LogBox instance which holds this Logger. </param>
         ''' <remarks> 
         ''' This is called internally only. To get a Logger use the static <see cref="LogBox.getLogger"/> method.
         ''' </remarks>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="ParentLogBox"/> is <see langword="null"/>. </exception>
        Friend Sub New(LoggerName As String, ParentLogBox As LogBox)
            
            If (ParentLogBox Is Nothing) Then Throw New ArgumentNullException("ParentLogBox")

            SyncHandle1 = New Object()
            SyncHandle2 = New Object()
            
            _Name   = LoggerName
            _LogBox = ParentLogBox
        End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Returns this Logger's Name. </summary>
         ''' <remarks> The Name is used to indicate the source of the message. </remarks>
        Public ReadOnly Property Name() As String
            Get
                Name = _Name
            End Get
        End Property
        
        ''' <summary> Returns the LogBox instance which this Logger is connected to. </summary>
         ''' <remarks> This is for internal use only. </remarks>
        Protected ReadOnly Property LogBox() As LogBox
            Get
                Return _LogBox
            End Get
        End Property
        
        ''' <summary> Returns the Console instance which this Logger is connected to via LogBox. </summary>
         ''' <remarks> This is for internal use only. </remarks>
        Protected ReadOnly Property Console() As Console
            Get
                SyncLock (SyncHandle1)
                    If (_Console Is Nothing) Then
                        _Console = _LogBox.Console
                    End If
                    Return _Console
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns the MessageStore instance which this Logger is connected to via LogBox. </summary>
         ''' <remarks> This is for internal use only. </remarks>
        Protected ReadOnly Property MessageStore() As MessageStore
            Get
                SyncLock (SyncHandle2)
                    If (_MessageStore Is Nothing) Then
                        _MessageStore = _LogBox.MessageStore
                    End If
                    Return _MessageStore
                End SyncLock
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
        Public Sub LogError(ex As System.Exception, ByVal Message As String)
            ' Log exception.
            If (ex IsNot Nothing) Then
                
                ' Gather exception source information.
                'Dim TargetName          As String = "?"
                'Dim TargetReflectedType As String = Nothing
                Dim TargetDeclaringType As String = ex.Source
                
                If (ex.TargetSite IsNot Nothing) Then
                    'TargetName = ex.TargetSite.Name
                    'If (ex.TargetSite.ReflectedType IsNot Nothing) Then TargetReflectedType = ex.TargetSite.ReflectedType.FullName
                    If (ex.TargetSite.DeclaringType IsNot Nothing) Then TargetDeclaringType = ex.TargetSite.DeclaringType.FullName
                End If
                
                ' Type-dependent header message.
                Dim ExHeaderLevel As LogLevelEnum = If(Me.LogBox.ExceptionHeaderAsDebug AndAlso (Not String.IsNullOrWhiteSpace(ex.Message)), LogLevelEnum.Debug, LogLevelEnum.Error)
                If (TypeOf ex Is System.IO.FileNotFoundException) Then
                    'logMessage(LogLevelEnum.Error, String.Format("{0} in {1}/{2}():{3}{4} ({5})", ex.GetType().Name, TargetDeclaringType, getTargetName(ex), vbNewLine, ex.Message, CType(ex, System.IO.FileNotFoundException).FileName))
                    LogMessage(ExHeaderLevel, String.Format("{0} in {1}/{2}():", ex.GetType().Name, TargetDeclaringType, GetTargetName(ex)))
                    LogMessage(LogLevelEnum.Error, String.Format("{0} ({1})", ex.Message, CType(ex, System.IO.FileNotFoundException).FileName))
                    
                ElseIf (TypeOf ex Is System.Data.OleDb.OleDbException) Then
                    Dim OleDbEx As System.Data.OleDb.OleDbException = CType(ex, System.Data.OleDb.OleDbException)
                    LogMessage(ExHeaderLevel, String.Format("{0} in {1}/{2}() (FehlerCode={3}, FehlerAnzahl={4}):", ex.GetType().Name, TargetDeclaringType, GetTargetName(ex), OleDbEx.ErrorCode, OleDbEx.Errors.Count))
                    For Each OleDbErr As System.Data.OleDb.OleDbError In OleDbEx.Errors
                        LogMessage(LogLevelEnum.Error, String.Format(" - {0}: {1}", OleDbErr.Source, OleDbErr.Message))
                    Next
                Else
                    'logMessage(LogLevelEnum.Error, String.Format("{0} in {1}/{2}():{3}{4}", ex.GetType().Name, TargetDeclaringType, getTargetName(ex), vbNewLine, ex.Message))
                    LogMessage(ExHeaderLevel, String.Format("{0} in {1}/{2}():", ex.GetType().Name, TargetDeclaringType, GetTargetName(ex)))
                    LogMessage(LogLevelEnum.Error, ex.Message)
                End If
                
                ' Test **************
                'logMessage(LogLevelEnum.Debug, String.Format("   (TargetReflectedType = {0},  TargetDeclaringType = {1})", TargetReflectedType, TargetDeclaringType))
                ' Test **************
                
                ' Stack trace only with debug level.
                LogMessage(LogLevelEnum.Debug, ex.StackTrace)
                
                ' Discover inner exceptions.
                If (TypeOf ex Is AggregateException) Then
                    For Each InnerEx As Exception In CType(ex, AggregateException).InnerExceptions
                        LogError(InnerEx, Nothing)
                    Next
                ElseIf (ex.InnerException IsNot Nothing) Then
                    ' Recursive discover inner exceptions - last first :-(.
                    LogError(ex.InnerException, Nothing)
                End If 
            End If
            
            ' Log given Message.
            'If (Not String.IsNullOrEmpty(Message)) Then logMessage(LogLevelEnum.Error, Message)
            If (Message IsNot Nothing) Then LogMessage(LogLevelEnum.Error, Message)
        End Sub
        
        ''' <summary> Adds a message of Error level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub LogError(ByVal Message As String)
            LogMessage(LogLevelEnum.Error, Message)
        End Sub
        
        ''' <summary> Adds a message of Warning level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub LogWarning(ByVal Message As String)
            LogMessage(LogLevelEnum.Warning, Message)
        End Sub
        
        ''' <summary> Adds a message of Info level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub LogInfo(ByVal Message As String)
            LogMessage(LogLevelEnum.Info, Message)
        End Sub
        
        ''' <summary> Adds a message of Debug level to the log. </summary>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
        Public Sub LogDebug(ByVal Message As String)
            LogMessage(LogLevelEnum.Debug, Message)
        End Sub
        
    #End Region
    
    #Region "Protected Members"
        
        ''' <summary> Adds a message of given level to the MessageStore. </summary>
         ''' <param name="Level">   The LogLevel </param>
         ''' <param name="Message"> The message itself (may contain line breaks). </param>
         ''' <remarks> In fact this method only enqueues the message as a <see cref="LogJob"/> into the LogJobQueue of <see cref="MessageStore"/>. </remarks>
        Protected Sub LogMessage(ByVal Level As LogLevelEnum, ByVal Message As String)
            Try
                Me.MessageStore.EnqueueLogJob(New LogJob(Level, Me.Name, Message))
            Catch ex As System.Exception
                System.Diagnostics.Debug.Fail("logMessage() failed!")
            End Try
        End Sub
        
        ''' <summary> Returns the name of the failed method. </summary>
         ''' <returns> The name of the failed method. </returns>
        Protected Function GetTargetName(ex as Exception) As String
            Return If(ex.TargetSite Is Nothing, "?", ex.TargetSite.Name)
        End Function
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
