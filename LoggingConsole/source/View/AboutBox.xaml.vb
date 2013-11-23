
Partial Class AboutBox
    Inherits System.Windows.Window
    
    'Close the Window on MouseDown.
    Private Sub Window_ClickAnywhere(sender As System.Object , e As System.Windows.Input.MouseButtonEventArgs) Handles MyBase.PreviewMouseDown
        MyBase.Close()
    End Sub
    
    'Close the Window on any key release.
    Private Sub Window_AnyKey(sender As System.Object , e As System.Windows.Input.KeyEventArgs) Handles MyBase.PreviewKeyUp
        e.Handled = True
        MyBase.Close()
    End Sub
End Class
