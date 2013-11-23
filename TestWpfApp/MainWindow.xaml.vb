
'An assembly level attribute which tells log4net to read it's configuration from the xml file "log4net.config".
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config")>


Partial Class MainWindow 
    
    ''' <summary>
    ''' Built-in logging
    ''' </summary>
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.getLogger("Test.for Demo")
        
        'To not change the active view when an error is logged (see corresponding CheckBox):
        'Rstyx.LoggingConsole.LogBox.Instance.Console.activateErrorViewOnError = False 
        
        for i As ULong = 1 to 1000
          BuiltInLogger.logDebug("Test debug")
          BuiltInLogger.logInfo("Test Info")
          BuiltInLogger.logWarning("Test Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning Warning")
          BuiltInLogger.logError("Test Error")
        next
    End Sub
    
    
    ''' <summary>
    ''' Show the built-in window
    ''' </summary>
    Private Sub Button2_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button2.Click
        'Remove the ConsoleView from the Application's MainWindow (from ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Nothing
        
        'Show built-in standalone Window.
        Rstyx.LoggingConsole.LogBox.Instance.showFloatingConsoleView(suppressErrorOnFail:=false)
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
    
    
    ''' <summary>
    ''' Embed the ConsoleView
    ''' </summary>
    Private Sub Button4_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button4.Click
        'Ensure that the built-in window isn't shown
        Rstyx.LoggingConsole.LogBox.Instance.hideFloatingConsoleView()
        
        'Embed the ConsoleView into the MainWíndow (which has a ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
    End Sub
End Class
