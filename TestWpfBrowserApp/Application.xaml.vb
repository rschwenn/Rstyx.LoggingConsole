
Partial Public Class Application
    
    ''' <summary>
    ''' Code for Testing other Cultures.
    ''' </summary>
    ''' <remarks></remarks>
    Shared Sub New()
        'Enforce LoggingConsole to use american formats for Date and Time.
        'System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US")
        
        'Enforce LoggingConsole to use English UI Language (Note the "UI" in "CurrentUICulture").
        'System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en")
        
        'This enforces LoggingConsole to use English UI Language too, 
        'because there are actually no "it" resources and the built-in resources of LoggingConsole.dll are English.
        'System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("it")
    End Sub
    
    ''' <summary>
    ''' Alternative ways of initialization.
    ''' </summary>
    ''' <param name="sender"> ignored </param>
    ''' <param name="e"> ignored </param>
    Private Sub Application_Startup(sender As Object, e As System.Windows.StartupEventArgs) Handles Me.Startup
        'Initialize and show the MainWindow, if Attribute StartupUri="MainWindow.xaml" in Application.xaml hasn't been stated.
        'Dim window As MainWindow = New MainWindow
        'window.Show()
        
        'If LoggingConsole's ConsoleView isn't embedded via binding, this could be done this way:
        'window.LoggingConsolePanel.Content = Rstyx.LoggingConsole.LogBox.Instance.Console.ConsoleView
        Rstyx.LoggingConsole.LogBox.Instance.DisplayName = "LoggingConsole - WPF Browser App Demo"
    End Sub
    
    'Private Sub Application_Exit(sender As Object, e As System.Windows.ExitEventArgs) Handles Me.Exit
    '    MySettings.Default.Save()
    'End Sub
    
End Class

' for jEdit:  :collapseFolds=4::tabSize=4:

