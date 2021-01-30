
''' <summary>
''' An <a href="http://logging.apache.org/log4net/release/sdk/index.html" target="_blank"> Apache log4net Appender</a>
''' that appends log events from the <a href="http://logging.apache.org/log4net/" target="_blank"> log4net framework </a>
''' to the one shared instance of <see cref="LoggingConsole.LogBox"/>.
''' </summary>
 ''' <remarks>
 ''' <para>
 ''' <b>Mixed Usage:</b> Using this log4net Appender does not prevent from using the built-in 
 ''' methods of <see cref="Logger"/>. The Appender itself just uses these methods.
 ''' </para>
 ''' <para>
 ''' <b>LoggerName:</b> The log4net LoggerName is used to get a LogBox <see cref="Logger"/> of same name. 
 ''' So the name is finally passed into the "Source" field of the <see cref="LoggingConsole.LogEntry"/>.
 ''' </para>
 ''' <para>
 ''' <b>Level mapping:</b> Because the log4net level set is configurable, 
 ''' the log4net event's level is mapped to one of the 4 levels supported by LogBox.
 ''' </para>
 ''' <para>
 ''' <b>Prerequisites:</b> The application which uses log4net in conjunction with LoggingConsole has
 ''' to ensure that log4net is configured to use this Appender. There are no other preparations needed. 
 ''' Just log from log4net and show the ConsoleView to see the messages sent from log4net.
 ''' </para>
 ''' <para>
 ''' Here's a basic sample of log4net config file, which configures log4net
 ''' to append all messages from all loggers to LoggingConsole and a matching code sample:
 ''' </para>
 ''' </remarks>
 ''' <example> 
 ''' <code lang="xml" title="log4net sample configuration file">
 ''' &lt;?xml version="1.0" encoding="utf-8" ?&gt;
 ''' &lt;log4net&gt;
 '''   &lt;appender name="LoggingConsoleAppender" type="Rstyx.LoggingConsole.Log4netAppender, LoggingConsole"/&gt;
 '''   &lt;root&gt;
 '''     &lt;appender-ref ref="LoggingConsoleAppender" /&gt;
 '''   &lt;/root&gt;
 ''' &lt;/log4net&gt;
 ''' </code> 
 ''' <code title="Visual Basic: sample for using log4net">
 ''' 'Use log4net to log messages
 ''' Dim log As log4net.ILog = log4net.LogManager.GetLogger("any.Name.You.wish")
 ''' log.Debug("Debug message from log4net")
 ''' log.Info("Info message from log4net")
 ''' log.Warning("Warning message from log4net")
 ''' log.Error("Error message from log4net")
 ''' 
 ''' 'Show the messages in a built-in window with embedded ConsoleView
 ''' LogBox.Instance.showFloatingConsoleView()
 ''' </code> 
 ''' </example>
Public Class Log4netAppender
    Inherits log4net.Appender.AppenderSkeleton
    
    #Region "Public Instance Constructors"
        
        ''' <summary> Initializes a new instance of the <see cref="Log4netAppender" />. </summary>
         ''' <remarks>
         ''' <para>
         ''' Usually there's no need to to create an Appender directly, because it is done by log4net configuration..
         ''' </para>
         ''' </remarks>
        Public Sub New()
        End Sub
        
    #End Region
    
    #Region "Override implementation of AppenderSkeleton"
        
        ''' <summary> Writes the logging event to the shared instance of <see cref="LoggingConsole.LogBox"/>. </summary>
         ''' <param name="loggingEvent"> The event to log. </param>
         ''' <remarks>
         ''' <para>
         ''' Because the log4net level set is configurable, 
         ''' the log4net event's level is mapped to one of the 4 levels supported by LogBox.
         ''' </para>
         ''' <para>
         ''' The log4net LoggerName is used to get a LogBox Logger of same name. 
         ''' So it is finally passed into the "Source" column of Console.
         ''' </para>
         ''' </remarks>
        Protected Overrides Sub Append(loggingEvent As log4net.Core.LoggingEvent)
            
            Dim oLogger As Rstyx.LoggingConsole.Logger = LogBox.GetLogger(loggingEvent.LoggerName)
            Dim message As String = loggingEvent.RenderedMessage
            
            Select Case loggingEvent.Level.Value
                Case Is <= log4net.Core.Level.Debug.Value: oLogger.LogDebug(message)
                Case Is <= log4net.Core.Level.Info.Value:  oLogger.LogInfo(message)
                Case Is <= log4net.Core.Level.Warn.Value:  oLogger.LogWarning(message)
                Case Else: oLogger.LogError(message)
            End Select
        End Sub
        
        ''' <summary> This appender doesn't requires a <see cref="Layout"/> to be set. </summary>
         ''' <value><c>true</c></value>
         ''' <remarks>
         ''' <para>
         ''' This appender doesn't requires a <see cref="Layout"/> to be set.
         ''' </para>
         ''' </remarks>
        Protected Overrides ReadOnly Property RequiresLayout() As Boolean
            Get
                Return False
            End Get
        End Property
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=3::tabSize=4::indentSize=4:
