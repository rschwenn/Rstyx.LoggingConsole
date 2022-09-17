
Imports System.Threading


'An assembly level attribute which tells log4net to read it's configuration from the xml file "log4net.config".
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config")>


Partial Class MainWindow 
    
    Private MainWindowLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")
    
    ''' <summary>
    ''' Built-in logging
    ''' </summary>
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")
        
        'To not change the active view when an error is logged (see corresponding CheckBox):
        'Rstyx.LoggingConsole.LogBox.Instance.Console.activateErrorViewOnError = False 
        
        'If (System.Windows.Application.Current IsNot Nothing) Then BuiltInLogger.LogDebug("MainWindow.Button1_Click(): WPF UI thread ID  = " & System.Windows.Application.Current.Dispatcher.Thread.ManagedThreadId.ToString())
        BuiltInLogger.LogDebug("MainWindow.Button1_Click(): WPF UI  thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
        BuiltInLogger.LogDebug("MainWindow.Button1_Click(): Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString())

        ' Prepare additional threads.
        Dim ThreadCount As Integer = 10
        Dim Threads(ThreadCount - 1) As Thread
        For i As Integer = 0 To ThreadCount - 1
            Threads(i) = New Thread(New ThreadStart(AddressOf LogSomething))
        Next

        ' Log test from additional threads.
        For i As Integer = 0 To ThreadCount - 1
            Threads(i).Start()
        Next

        ' Log test from current thread.
        LogSomething()
    End Sub
    
    Private Sub LogSomething()
        ' One single Logger for all threads together.
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")

        ' One Logger for every single thread.
        'Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.LogSomething." & Thread.CurrentThread.ManagedThreadId.ToString())
        
        For i As ULong = 1 To 1000
            BuiltInLogger.LogDebug("LogSomething() Debug:    Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogInfo("LogSomething() Info :    Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogWarning("LogSomething() Warning:  Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogError("LogSomething() Error:    Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
        Next
    End Sub
    
    ''' <summary>
    ''' Show the built-in window
    ''' </summary>
    Private Sub Button2_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button2.Click
        'Remove the ConsoleView from the Application's MainWindow (from ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Nothing
        
        'Show built-in standalone Window.
        Rstyx.LoggingConsole.LogBox.Instance.ShowFloatingConsoleView(suppressErrorOnFail:=false)
    End Sub
    
    
    ''' <summary>
    ''' Log from log4net
    ''' </summary>
    Private Sub Button3_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button3.Click
        Dim log4netLogger As log4net.ILog = log4net.LogManager.GetLogger("Test.log4net")
        
        For i As ULong = 1 To 300
            log4netLogger.Debug("Debug from log4net")
            log4netLogger.Info("Info from log4net")
            log4netLogger.Error("Error from log4net")
            log4netLogger.Warn("Warn from log4net")
        Next
    End Sub
    
    
    ''' <summary>
    ''' Embed the ConsoleView
    ''' </summary>
    Private Sub Button4_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles Button4.Click
        'Ensure that the built-in window isn't shown
        Rstyx.LoggingConsole.LogBox.Instance.HideFloatingConsoleView()
        
        'Embed the ConsoleView into the MainWíndow (which has a ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
    End Sub
End Class
