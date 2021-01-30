
'An assembly level attribute which tells log4net to read it's configuration from the xml file "log4net.config".
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config")>


Public Class MainForm
    
    'This way the ConsoleView can be embedded via code:
    'Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    'Embed LoggingConsole.ConsoleView
    '    LoggingConsolePanel.Child = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
    'End Sub
    
    Private Sub Button1_Click( sender As System.Object,  e As System.EventArgs) Handles Button1.Click
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Test.for Demo")
        
        'To not change the active view when an error is logged (see corresponding CheckBox):
        'Rstyx.LoggingConsole.LogBox.Instance.Console.activateErrorViewOnError = False 
        
        for i As ULong = 1 to 1000
          BuiltInLogger.LogDebug("Test debug")
          BuiltInLogger.LogInfo("Test Info")
          BuiltInLogger.LogWarning("Test Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning")
          BuiltInLogger.LogError("Test Error")
        next
    End Sub
    
    Private Sub Button2_Click( sender As System.Object,  e As System.EventArgs) Handles Button2.Click
        Dim log4netLogger As log4net.ILog = log4net.LogManager.GetLogger("Test.log4net")
        
        log4netLogger.Debug("Debug from log4net")
        log4netLogger.Info("Info from log4net")
        log4netLogger.Error("Error from log4net")
        log4netLogger.Warn("Warn from log4net")
    End Sub
    
    Private Sub Button3_Click( sender As System.Object,  e As System.EventArgs) Handles Button3.Click
        'Remove the ConsoleView from the Application's MainWindow (from ContentControl with Name="LoggingConsolePanel")
        LoggingConsolePanel.Child = Nothing
        
        'Show built-in floating Window.
        Rstyx.LoggingConsole.LogBox.Instance.ShowFloatingConsoleView(suppressErrorOnFail:=false)
    End Sub
    
    Private Sub Button4_Click( sender As System.Object,  e As System.EventArgs) Handles Button4.Click
        'Ensure that the built-in window isn't shown
        Rstyx.LoggingConsole.LogBox.Instance.HideFloatingConsoleView()
        
        'Embed the ConsoleView into the MainWíndow (which has a ContentControl with Name="LoggingConsolePanel")
        'Try
            LoggingConsolePanel.Child = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
        'Catch except As Exception
        'End Try
    End Sub
    
End Class
