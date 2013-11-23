<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LoggingConsolePanel = New System.Windows.Forms.Integration.ElementHost()
        Me.ConsoleView1 = New Rstyx.LoggingConsole.ConsoleView()
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.SuspendLayout
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(21, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(129, 47)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Built-in Logging"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(173, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(180, 47)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Logging from Log4Net"
        Me.Button2.UseVisualStyleBackColor = true
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(378, 19)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(177, 47)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "Show built-in Window"
        Me.Button3.UseVisualStyleBackColor = true
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(580, 19)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(163, 47)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Embed ConsoleView"
        Me.Button4.UseVisualStyleBackColor = true
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.LoggingConsolePanel)
        Me.SplitContainer1.Panel2MinSize = 50
        Me.SplitContainer1.Size = New System.Drawing.Size(859, 402)
        Me.SplitContainer1.SplitterDistance = 86
        Me.SplitContainer1.TabIndex = 1
        '
        'LoggingConsolePanel
        '
        Me.LoggingConsolePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoggingConsolePanel.Location = New System.Drawing.Point(0, 0)
        Me.LoggingConsolePanel.Name = "LoggingConsolePanel"
        Me.LoggingConsolePanel.Size = New System.Drawing.Size(859, 312)
        Me.LoggingConsolePanel.TabIndex = 0
        Me.LoggingConsolePanel.Text = "ElementHost1"
        Me.LoggingConsolePanel.Child = Me.ConsoleView1
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 402)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Name = "MainForm"
        Me.Text = "LoggingConsole WinForm Demo"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents LoggingConsolePanel As System.Windows.Forms.Integration.ElementHost
    Friend ConsoleView1 As Rstyx.LoggingConsole.ConsoleView

End Class
