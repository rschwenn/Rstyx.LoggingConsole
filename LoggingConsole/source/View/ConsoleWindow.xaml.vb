
Imports System

Partial Class ConsoleWindow
    Inherits System.Windows.Window
    
    Private InternalLogger As Logger  = LogBox.getLogger("LogBox.ConsoleWindow")
    
    ''' <summary>
    ''' Focus the Window on Activate to get keyboard events.
    ''' </summary>
    Private Sub ConsoleWindow_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        Me.Focus()
    End Sub
    
    Private Sub Window_ESC(sender As System.Object , e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
      Try
          If (e.Key = Windows.Input.Key.Escape) then
              Me.Close()
              e.Handled = True
          End If
        Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
        End Try
    End Sub
    
    'Private Sub HideButton_Click(sender As System.Object , e As System.Windows.RoutedEventArgs) Handles HideButton.Click
    '    Me.Hide()
    'End Sub
End Class
