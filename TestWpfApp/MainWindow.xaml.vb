
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Threading
Imports System.Windows.Input


'An assembly level attribute which tells log4net to read it's configuration from the xml file "log4net.config".
<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config")>


Partial Class MainWindow

    Private MainWindowLogger As Rstyx.LoggingConsole.Logger

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        MainWindowLogger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")
    End Sub

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

        Dim Watch As New Stopwatch()
        Watch.Start()

        ' Log test from additional threads.
        For i As Integer = 0 To ThreadCount - 1
            Threads(i).Start()
        Next

        ' Log test from current thread.
        LogSomething()

        BuiltInLogger.LogInfo("Button1_Click():  " & CStr(Watch.Elapsed.Seconds) & " seconds")
    End Sub

    Private Sub LogSomething()
        ' One single Logger for all threads together.
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")

        ' One Logger for every single thread.
        'Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.LogSomething." & Thread.CurrentThread.ManagedThreadId.ToString())

        For i As ULong = 1 To 1000
            BuiltInLogger.LogDebug("LogSomething() Debug   #" & CStr(i) & ":   Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogInfo("LogSomething() Info    #" & CStr(i) & ":   Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogWarning("LogSomething() Warning #" & CStr(i) & ":   Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
            BuiltInLogger.LogError("LogSomething() Error   #" & CStr(i) & ":   Current thread ID = " & Thread.CurrentThread.ManagedThreadId.ToString() & ",  WPF UI thread ID = " & Me.Dispatcher.Thread.ManagedThreadId.ToString())
        Next
    End Sub


    ''' <summary>
    ''' Log from log4net
    ''' </summary>
    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Dim log4netLogger As log4net.ILog = log4net.LogManager.GetLogger("Test.log4net")

        For i As ULong = 1 To 300
            log4netLogger.Debug("Debug from log4net")
            log4netLogger.Info("Info from log4net")
            log4netLogger.Error("Error from log4net")
            log4netLogger.Warn("Warn from log4net")
        Next
    End Sub


    ''' <summary>
    ''' Show the built-in window
    ''' </summary>
    Private Sub Button3_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button3.Click
        'Remove the ConsoleView from the Application's MainWindow (from ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Nothing

        'Show built-in standalone Window.
        Rstyx.LoggingConsole.LogBox.Instance.ShowFloatingConsoleView(suppressErrorOnFail:=False)
    End Sub


    ''' <summary>
    ''' Embed the ConsoleView
    ''' </summary>
    Private Sub Button4_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button4.Click
        'Ensure that the built-in window isn't shown
        Rstyx.LoggingConsole.LogBox.Instance.HideFloatingConsoleView()

        'Embed the ConsoleView into the MainWíndow (which has a ContentControl with Name="LoggingConsolePanel")
        Dim window As MainWindow = Application.Current.MainWindow
        window.LoggingConsolePanel.Content = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
    End Sub

    ''' <summary>
    ''' Change Window Background Color
    ''' </summary>
    Private Sub Button5_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button5.Click
        Dim BuiltInLogger As Rstyx.LoggingConsole.Logger = Rstyx.LoggingConsole.LogBox.GetLogger("Demo.MainWindow")

        'To not change the active view when an error is logged (see corresponding CheckBox):
        'Rstyx.LoggingConsole.LogBox.Instance.Console.activateErrorViewOnError = False 

        'If (System.Windows.Application.Current IsNot Nothing) Then BuiltInLogger.LogDebug("MainWindow.Button1_Click(): WPF UI thread ID  = " & System.Windows.Application.Current.Dispatcher.Thread.ManagedThreadId.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): App should use Dark Theme = " & IsDarkTheme.ToString())

        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ConsoleView Background Color = " & Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView.Background?.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ConsoleView Foreground Color = " & Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView.Foreground?.ToString())

        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ActiveBorderColor = " & System.Windows.SystemColors.ActiveBorderColor.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ActiveCaptionColor = " & System.Windows.SystemColors.ActiveCaptionColor.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ActiveCaptionTextColor = " & System.Windows.SystemColors.ActiveCaptionTextColor.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): AppWorkspaceColor = " & System.Windows.SystemColors.AppWorkspaceColor.ToString())
        
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): WindowBrush = " & System.Windows.SystemColors.WindowBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): WindowTextBrush = " & System.Windows.SystemColors.WindowTextBrush.Color.ToString())
        
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlBrush  = " & System.Windows.SystemColors.ControlBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlDarkBrush  = " & System.Windows.SystemColors.ControlDarkBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlDarkDarkBrush = " & System.Windows.SystemColors.ControlDarkDarkBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlLightBrush = " & System.Windows.SystemColors.ControlLightBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlLightLightBrush = " & System.Windows.SystemColors.ControlLightLightBrush.Color.ToString())
        BuiltInLogger.LogInfo("MainWindow.Button5_Click(): ControlTextBrush = " & System.Windows.SystemColors.ControlTextBrush.Color.ToString())
    End Sub

    Private Function IsDarkTheme() As Boolean
        Dim RetValue As Boolean = False
        Try
            Using Key As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")
                Dim RegValue As Object = key?.GetValue("AppsUseLightTheme")
                RetValue = Not CBool(RegValue)
            End Using
        Catch ex As System.Exception
            ' 
        End Try
        Return RetValue
    End Function

End Class
