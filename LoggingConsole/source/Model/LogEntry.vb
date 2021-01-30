''' <summary> All supported log levels </summary>
Public Enum LogLevelEnum As Integer
    [Error] = 3
    Warning = 2
    Info    = 1
    Debug   = 0
End Enum

''' <summary> One single Log entry </summary>
 ''' <remarks> This class is used internally only. </remarks>
Public Class LogEntry
    
    #Region "Private Fields"
        
        Private ReadOnly _LineNo    As Long
        Private ReadOnly _Date      As Date = DateAndTime.Today
        Private ReadOnly _Time      As Date = DateAndTime.TimeOfDay
        Private ReadOnly _Level     As LogLevelEnum = LogLevelEnum.Info
        Private ReadOnly _Source    As String = String.Empty
        Private ReadOnly _Message   As String = String.Empty
        
    #End Region
    
    #Region "Constuctor"
        
        ''' <summary> Creates a new LogEntry with all needed information, that cannot be changed afterwards. </summary>
         ''' <param name="LineNo">  Line number of this log entry in the whole list </param>
         ''' <param name="Level">   Indicates the severity of this log entry (<see cref="LoggingConsole.LogLevelEnum"/>) </param>
         ''' <param name="Source">  Indicates the source or origin of this log entry. In general this is the logger name. </param>
         ''' <param name="Message"> A one line message or one line of a multi line message </param>
         ''' <remarks> This is called internally only. </remarks>
        Public Sub New(LineNo As Long, Level As LogLevelEnum, Source As String, Message As String)
            _LineNo  = LineNo
            _Level   = Level
            _Source  = Source
            _Message = Message
        End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Line number of this log entry in the whole list as it's been given to the constructer </summary>
        Public ReadOnly Property LineNo As Long
            Get
                LineNo = _LineNo
            End Get
        End Property
        
        ''' <summary> The date of creation of this log entry. This is automatically added. </summary>
        Public ReadOnly Property [Date] As String
            Get
                [Date] = _Date
            End Get
        End Property
        
        ''' <summary> The time of creation of this log entry. This is automatically added. </summary>
        Public ReadOnly Property Time As String
            Get
                Time = _Time
            End Get
        End Property
        
        ''' <summary> Indicates the severity of this log entry as it's been given to the constructer. </summary>
        Public ReadOnly Property Level As String
            Get
                Level = _Level
            End Get
        End Property
        
        ''' <summary> Indicates the source or origin of this log entry as it's been given to the constructer. In general this is the logger name. </summary>
        Public ReadOnly Property Source As String
            Get
                Source = _Source
            End Get
        End Property
        
        ''' <summary> A one line message or one line of a multi line message as it's been given to the constructer. </summary>
        Public ReadOnly Property Message As String
            Get
                Message = _Message
            End Get
        End Property
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
