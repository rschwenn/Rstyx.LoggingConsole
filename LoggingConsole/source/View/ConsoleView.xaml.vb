
Imports System

Partial Public Class ConsoleView
    Inherits System.Windows.Controls.UserControl
    
    Private ReadOnly InternalLogger     As Logger  = LogBox.GetLogger("LogBox.ConsoleView")
    Private ReadOnly _ConnectToConsole  As Boolean = False
    
    Public Sub New()
        Me.New(ConnectToConsole:=True)
    End Sub
    
    ''' <summary> Initializes a new instance of the <see cref="ConsoleView"/> class with the option to prevent self-connecting to <see cref="Console"/>. </summary>
    ''' <param name="ConnectToConsole"> False, to prevent self-connecting to <see cref="Console"/>. This is needed if <see cref="ConsoleView"/> is initialized by <see cref="Console"/> itself. </param>
    ''' <remarks> The public constructor whithout arguments (which is used by xaml) calls this with "ConnectToConsole:=True". </remarks>
    Friend Sub New(ConnectToConsole As Boolean)
        InitializeComponent()
        _ConnectToConsole = ConnectToConsole
    End Sub
    
    
    Private Sub ConsoleView_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        'Connect this ConsoleView to the Console View Model, if needed.
        If (_ConnectToConsole) Then
            LogBox.Instance.Console.ConsoleView = Me
        End if
    End Sub
    
    'Close the opened option pane on ESC release.
    Private Sub Expander_ESC(sender As System.Object , e As System.Windows.Input.KeyEventArgs) Handles SettingsExpander.KeyUp
        Try
            If (SettingsExpander.IsExpanded) then
                If (e.Key = Windows.Input.Key.Escape) then
                    e.Handled = True
                    SettingsExpander.IsExpanded = False
                End If
            End If
        Catch ex As System.Exception
                InternalLogger.LogError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
        End Try
    End Sub
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
