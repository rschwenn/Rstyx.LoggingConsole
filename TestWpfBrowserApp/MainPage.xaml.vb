
'An assembly level attribute which tells log4net to read it's configuration from the xml file "log4net.config".
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config")>

Class MainPage 
    
    ''' <summary>
    ''' Built-in logging
    ''' </summary>
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
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
    
    
    ''' <summary>
    ''' Log from log4net
    ''' </summary>
    Private Sub Button3_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button3.Click
        Dim log4netLogger As log4net.ILog = log4net.LogManager.GetLogger("Test.log4net")
        
        log4netLogger.Debug("Debug from log4net")
        log4netLogger.Info("Info from log4net")
        log4netLogger.Error("Error from log4net")
        log4netLogger.Warn("Warn from log4net")
    End Sub

End Class
