
Imports System
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Collections.ObjectModel

''' <summary> The Console ViewModel: It encapsulates all Console related stuff. </summary>
 ''' <remarks> 
 ''' <para> 
 ''' An instance of this class is deliverd by the LogBox.<see cref="LogBox.Console"/> property. 
 ''' There is no need to manually instantiate this Class. 
 ''' But You may want to deal with it's properties. 
 ''' Many properties are two-way-bound to the UI: They reflect the current UI state, 
 ''' and changing the property via code causes the UI to synchronize with it. 
 ''' </para>
 ''' <para> 
 ''' Some of these properties are <b>application settings</b> which means that they
 ''' are restored when LogBox is initialized and saved when LogBox is terminated.
 ''' </para> 
 ''' <para> 
 ''' Implementation details of <b>application settings</b>: <br />
 ''' These are properties that have a corresponding user setting with the same name 
 ''' that are read and written directly without caching in a local variable. 
 ''' <see cref="LoggingConsole.ViewModelBase"/> is listening for changing of user settings and fires <see cref="LoggingConsole.ViewModelBase.OnPropertyChanged"/> 
 ''' for the property name, so the world can take notice of changing of these properties here. 
 ''' </para> 
 ''' </remarks>
Public NotInheritable Class Console
    Inherits ViewModelBase
    
    #Region "Private Fields"
        
        Private ReadOnly _LogBox            As LogBox
        Private _MessagesViews              As ObservableCollection(Of MessagesView)
        Private _ConsoleView                As ConsoleView
        
        Private _ActiveView                 As LogLevelEnum = LogLevelEnum.Info
        Private IsActiveViewInitialized     As Boolean = False
        Private _ActivateErrorViewOnError   As Boolean = True
        
        Private ReadOnly InternalLogger     As Logger = LogBox.GetLogger("LogBox.Console")
        
        Private Shared ReadOnly SyncHandle  As New Object()
        
    #End Region
    
    #Region "Constuctors and Finalizers"
        
        ''' <summary> Creates a new instance of the Console ViewModel. </summary>
         ''' <param name="parentLogBox"> The LogBox instance which holds this Console. </param>
         ''' <exception cref="System.ArgumentNullException"> <paramref name="parentLogBox"/> is <see langword="null"/>. </exception>
        Friend Sub New(parentLogBox As LogBox)
            
            If (parentLogBox Is Nothing) Then Throw New ArgumentNullException("parentLogBox")
            
            _LogBox = parentLogBox
            MyBase.DisplayName = _LogBox.DisplayName
            
            'Activate error view if there are already errors in the MessageStore.
            If (Me.ActivateErrorViewOnError and Me.LogBox.MessageStore.HighestLevelInLog = LogLevelEnum.Error) Then Me.ActiveView = LogLevelEnum.Error
            
            'Listen for logging of new error messages
            AddHandler Me.LogBox.MessageStore.ErrorLogged, AddressOf OnNewErrorLogged
        End Sub
        
        '' <summary> Won't be called as long as there are active event handlers! </summary>
        'Protected Overrides Sub Finalize()
        '    MyBase.Finalize()
        '    'If (_ConsoleView IsNot Nothing) Then RemoveHandler _ConsoleView.MessagesTabControl.SelectionChanged, AddressOf OnMessagesTabChanged
        '    'If (_LogBox IsNot Nothing) Then RemoveHandler _LogBox.MessageStore.ErrorLogged, AddressOf OnNewErrorLogged
        'End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Returns the LogBox instance, which has created this Console instance and which this Console is connected to. </summary>
         ''' <returns> LogBox </returns>
        Public ReadOnly Property LogBox() As LogBox
            Get
                Return _LogBox
            End Get
        End Property
        
        ''' <summary> Returns or sets the ConsoleView UserControl instance which is or should be connected to this Console </summary>
         ''' <value>   ConsoleView UserControl </value>
         ''' <returns> ConsoleView UserControl </returns>
         ''' <remarks> 
         ''' <para>
         ''' Get: The ConsoleView will be instantiated if it isn't yet. It's DataContext will be set to the connected LogBox instance. 
         ''' </para>
         ''' <para>
         ''' Set: The given ConsoleView will be connected to this Console instance. It's DataContext will be set to the connected LogBox instance. 
         ''' </para>
         ''' </remarks>
        Public Property ConsoleView() As ConsoleView
            Get
                If (_ConsoleView Is Nothing) Then
                    _ConsoleView = New ConsoleView(ConnectToConsole:=False)
                    _ConsoleView.DataContext = Me.LogBox
                   
                   'Workaround: Detect of chosing another Tab of MessagesTabControl
                   AddHandler _ConsoleView.MessagesTabControl.SelectionChanged, AddressOf OnMessagesTabChanged
                   
                   InternalLogger.LogDebug(String.Format("ConsoleView[Get]: {0}", My.Resources.Resources.Console_GetConsoleView_Created))
                End If
                Return _ConsoleView
            End Get
            Set(value As ConsoleView)
                If (Not value.equals(Nothing)) then
                    If (Not value.equals(_ConsoleView)) then
                        
                        ' Clean up.
                        If (_ConsoleView IsNot Nothing) Then
                            RemoveHandler _ConsoleView.MessagesTabControl.SelectionChanged, AddressOf OnMessagesTabChanged
                            _ConsoleView.DataContext = Nothing
                        End If
                        
                        _ConsoleView = value
                        _ConsoleView.DataContext = Me.LogBox
                        
                        'Workaround: Detect of chosing another Tab of MessagesTabControl
                        AddHandler _ConsoleView.MessagesTabControl.SelectionChanged, AddressOf OnMessagesTabChanged
                        
                        InternalLogger.LogDebug(String.Format("ConsoleView[Set]: {0}", My.Resources.Resources.Console_SetConsoleView_Connected))
                    End if
                End if
            End Set
        End Property
        
        ''' <summary> Checks if ConsoleView exists. </summary>
         ''' <returns> Array of System.Windows.Controls.Dock Enum members </returns>
         ''' <remarks> This is for internal use only. </remarks>
        Public ReadOnly Property ConsoleViewExists() As Boolean
            Get
                Return (_ConsoleView IsNot Nothing)
            End Get
        End Property
        
        ''' <summary> Returns the Collection of MessagesView UserControl instances which is connected to this Console </summary>
         ''' <returns> Collection of MessagesView UserControls </returns>
         ''' <remarks> 
         ''' The Collection and the MessagesViews will be initialized if they aren't yet. <br />
         ''' There is one MessagesView for each LogLevel (resp. LogLevelEnum member), which displays a filtered Log.
         ''' </remarks>
        Public ReadOnly Property MessagesViews() As ObservableCollection(Of MessagesView)
            Get
                If (_MessagesViews Is Nothing) Then
                    _MessagesViews = New ObservableCollection(Of MessagesView)
                    _MessagesViews.Add(New MessagesView(LogLevelEnum.Debug))
                    _MessagesViews.Add(New MessagesView(LogLevelEnum.Info))
                    _MessagesViews.Add(New MessagesView(LogLevelEnum.Warning))
                    _MessagesViews.Add(New MessagesView(LogLevelEnum.Error))
                End If
                Return _MessagesViews
            End Get
        End Property
        
        ''' <summary> Returns an array of Dock Enum members to fill the TabStripPlacement options combo box </summary>
         ''' <returns> Array of System.Windows.Controls.Dock Enum members </returns>
         ''' <remarks> This is for internal use only. </remarks>
        Public Shared ReadOnly Property TabStripPlacementOptions() As System.Windows.Controls.Dock()
            Get
                Return {Dock.Top, Dock.Left, Dock.Right, Dock.Bottom}
            End Get
        End Property
        
        ''' <summary> Returns an array of Dock Enum members to fill the ExpanderHeaderPlacement options combo box </summary>
         ''' <returns> Array of System.Windows.Controls.Dock Enum members </returns>
         ''' <remarks> This is for internal use only. </remarks>
        Public Shared ReadOnly Property ExpanderHeaderPlacementOptions() As System.Windows.Controls.Dock()
            Get
                Return {Dock.Left, Dock.Right}
            End Get
        End Property
        
        ''' <summary> This Value determines the View which is currently active or should be set active. </summary>
         ''' <value>   LogLevelEnum </value>
         ''' <returns> LogLevelEnum </returns>
         ''' <remarks> This is used as the index of TabControlItems Collection as well as the index of the MessagesViews Collection </remarks>
        Public Property ActiveView() As LogLevelEnum
            Get
                IsActiveViewInitialized = true
                Return _ActiveView
            End Get
            Set(value As LogLevelEnum)
                If (Not (value = _ActiveView)) then
                    _ActiveView = value
                    Me.OnPropertyChanged("ActiveView")
                End if
            End Set
        End Property
        
        ''' <summary> Should the Error View automatically be activated when an error message is logged? </summary>
         ''' <value> Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> 
         ''' This is meant for use in application code only. <br />
         ''' The Default is "true" and causes error messages to be shown immediately inside the ConsoleView. 
         ''' It does NOT cause a hidden window to be shown!
         ''' </remarks>
        Public Property ActivateErrorViewOnError() As Boolean
            Get
                Return _ActivateErrorViewOnError
            End Get
            Set(value As Boolean)
                If (Not (value = _ActivateErrorViewOnError)) then
                    _ActivateErrorViewOnError = value
                    Me.OnPropertyChanged("ActivateErrorViewOnError")
                End if
            End Set
        End Property

    #End Region
    
    #Region "Settings"
        
        ''' <summary> Automatically set the width of the Log View columns to fit the largest item? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property AutoSizeColumns() As Boolean
            Get
                Return My.Settings.AutoSizeColumns
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.AutoSizeColumns) Then
                    My.Settings.AutoSizeColumns = value
                End If
            End Set
        End Property
        
        ''' <summary> Show the "Line No" column in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ShowColumnLineNo() As Boolean
            Get
                Return My.Settings.ShowColumnLineNo
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ShowColumnLineNo) Then
                    My.Settings.ShowColumnLineNo = value
                End If
            End Set
        End Property
        
        ''' <summary> Show the "Date" column in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ShowColumnDate() As Boolean
            Get
                Return My.Settings.ShowColumnDate
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ShowColumnDate) Then
                    My.Settings.ShowColumnDate = value
                End If
            End Set
        End Property
        
        ''' <summary> Show the "Time" column in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ShowColumnTime() As Boolean
            Get
                Return My.Settings.ShowColumnTime
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ShowColumnTime) Then
                    My.Settings.ShowColumnTime = value
                End If
            End Set
        End Property
        
        ''' <summary> Show the "Log Level" (resp. "Type") column in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ShowColumnLevel() As Boolean
            Get
                Return My.Settings.ShowColumnLevel
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ShowColumnLevel) Then
                    My.Settings.ShowColumnLevel = value
                End If
            End Set
        End Property
        
        ''' <summary> Show the "Source" column in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ShowColumnSource() As Boolean
            Get
                Return My.Settings.ShowColumnSource
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ShowColumnSource) Then
                    My.Settings.ShowColumnSource = value
                End If
            End Set
        End Property
        
        ''' <summary> Use different background colors in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property UseBackgroundColors() As Boolean
            Get
                Return My.Settings.UseBackgroundColors
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.UseBackgroundColors) Then
                    My.Settings.UseBackgroundColors = value
                End If
            End Set
        End Property
        
        ''' <summary> Use different foreground colors in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property UseForegroundColors() As Boolean
            Get
                Return My.Settings.UseForegroundColors
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.UseForegroundColors) Then
                    My.Settings.UseForegroundColors = value
                End If
            End Set
        End Property
        
        ''' <summary> Use dark color schema in the Log View? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property UseDarkColorSchema() As Boolean
            Get
                Return My.Settings.UseDarkColorSchema
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.UseDarkColorSchema) Then
                    My.Settings.UseDarkColorSchema = value
                End If
            End Set
        End Property
        
        ''' <summary> Use Courier New instead of the inherited font.  </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property UseOwnFontFamily() As Boolean
            Get
                Return My.Settings.UseOwnFontFamily
            End Get
            Set(value As Boolean)
                if (value XOR My.Settings.UseOwnFontFamily) then
                    My.Settings.UseOwnFontFamily = value
                    
                    ' Since this setting could change the font size to use, force it to update.
                    Me.OnPropertyChanged("FontSize")
                end if
            End Set
        End Property
        
        ''' <summary> Use own font size instead of the inherited.  </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property UseOwnFontSize() As Boolean
            Get
                Return My.Settings.UseOwnFontSize
            End Get
            Set(value As Boolean)
                if (value XOR My.Settings.UseOwnFontSize) then
                    My.Settings.UseOwnFontSize = value
                    
                    ' Since this setting could change the font size to use, force it to update.
                    Me.OnPropertyChanged("FontSize")
                end if
            End Set
        End Property
        
        ''' <summary> Font size of ConsoleView. </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> 
         ''' <para>
         ''' Get: If <see cref="LoggingConsole.Console.UseOwnFontSize" /> is enabled, 
         ''' the "FontSize" application setting is returned. Otherwise the inherited value is
         ''' retrieved. But if this fails, the "FontSize" application setting is returned, too.
         ''' This means, that in a Windows Forms applications inheritance of FontSize isn't possible.
         ''' </para>
         ''' Set: The "FontSize" application setting is set to this value. 
         ''' If the new value is applied to the UI depends on factors described for "Get" (see above).
         ''' <para>
         ''' This is an "application setting". 
         ''' </para>
         ''' </remarks>
        Public Property FontSize() As Double
            Get
                SyncLock (SyncHandle)
                    Dim size As Double
                    
                    If (Me.UseOwnFontSize) then
                        size = My.Settings.FontSize
                    Else
                        size = GetInheritedFontSize(_ConsoleView)
                    End if
                        
                    If ((size < 6) or (size > 20)) then
                        InternalLogger.LogDebug("Fontsize[Get]: " & String.Format(My.Resources.Resources.Console_GetFontsize_ValueChanged, size, 10))
                        size = 11
                    End If
                    
                    If ((Not Me.UseOwnFontSize) AndAlso Me.UseOwnFontFamily) then
                        size *= 1.1
                    End if
                    
                    Return size
                End SyncLock
            End Get
            Set(value As Double)
                if (not (value = My.Settings.FontSize)) then
                    My.Settings.FontSize = value
                end if
            End Set
        End Property
        
        ''' <summary> Show the action buttons in the header of the options pane expander? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ActionButtonsAlwaysVisible() As Boolean
            Get
                Return My.Settings.ActionButtonsAlwaysVisible
            End Get
            Set(value As Boolean)
                If (value XOR My.Settings.ActionButtonsAlwaysVisible) Then
                    My.Settings.ActionButtonsAlwaysVisible = value
                End If
            End Set
        End Property
        
        ''' <summary> Where to place the tab strip? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property TabStripPlacement() As System.Windows.Controls.Dock
            Get
                Return My.Settings.TabStripPlacement
            End Get
            Set(value As System.Windows.Controls.Dock)
                If (Not (value = My.Settings.TabStripPlacement)) Then
                    My.Settings.TabStripPlacement = value
                End If 
            End Set
        End Property
        
        ''' <summary> Where to place the header of the options pane expander? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> This is an "application setting". </remarks>
        Public Property ExpanderHeaderPlacement() As System.Windows.Controls.Dock
            Get
                Return My.Settings.ExpanderHeaderPlacement
            End Get
            Set(value As System.Windows.Controls.Dock)
                If (Not (value = My.Settings.ExpanderHeaderPlacement)) Then
                    My.Settings.ExpanderHeaderPlacement = value
                End If 
            End Set
        End Property
        
    #End Region
    
    #Region "Event Handlers"
        
        ''' <summary> This method is called after an error is logged. </summary>
         ''' <param name="sender"> LogBox.Logger </param>
         ''' <param name="e"> Empty </param>
         ''' <remarks> If the "ActivateErrorViewOnError" property is "true", then this method activates the error view. </remarks>
        Private Sub OnNewErrorLogged(sender As System.Object, e As System.EventArgs)
            If (Me.ActivateErrorViewOnError) Then Me.ActiveView = LogLevelEnum.Error
        End Sub
        
        ''' <summary> Detects changing to another Messages Tab by the user via UI and synchronizes the <see cref="Console.ActiveView"/> property. </summary>
         ''' <param name="sender"> Me.ConsoleView.MessagesTabControl </param>
         ''' <param name="e">      SelectionChangedEventArgs </param>
         ''' <remarks> This is needed because Binding/SourceUpdate of Me.ActiveView isn't working (for what reason ever). </remarks>
        Private Sub OnMessagesTabChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs)
            Try
                If (sender.Equals(e.OriginalSource)) then
                    If (IsActiveViewInitialized) then
                        Me.ActiveView = _ConsoleView.MessagesTabControl.SelectedIndex
                    End if
                End If
            Catch ex As System.Exception
                InternalLogger.LogError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
    #End Region
    
    #Region "UpdateSourceExceptionFilter"
        
        ''' <summary> Binding support: Returns an UpdateSourceExceptionFilterCallback for a TextBox. </summary>
         ''' <returns> UpdateSourceExceptionFilterCallback </returns>
         ''' <remarks>
         ''' This property can be specified as "UpdateSourceExceptionFilter" for two-way-bound TextBoxes,
         ''' which causes the TextBox to be updated whith the (old) value of the bound property, if it were invalid before.
         ''' </remarks>
        Public Shared ReadOnly Property TextBoxExceptionFilter() As UpdateSourceExceptionFilterCallback
            Get
                Return New UpdateSourceExceptionFilterCallback(AddressOf UpdateSourceException_SilentlyKeepOldValue)
            End Get
        End Property
        
        ''' <summary>
        ''' This is the delegate method for "TextBoxExceptionFilter" property (see there).
        ''' It updates the (invalid) target with the "old" Source value.
        ''' </summary>
         ''' <returns> <see langword="null"/>. </returns>
        Private Shared Function UpdateSourceException_SilentlyKeepOldValue(bindingExpression As Object, exception As Exception) As Object
            CType(bindingExpression, BindingExpression).UpdateTarget()
            Return Nothing
        End Function
        
    #End Region
    
    #Region "Private Members"
        
        ''' <summary> Determines the value of FontSize, that would be evaluated by inheritance. </summary>
         ''' <param name="StartObject"> The object of interest </param>
         ''' <returns> FontSize or "Nothing" </returns>
         ''' <remarks>
         ''' <para>
         ''' 1. Searches for the next parent in the logical tree that has a "FontSize" property
         ''' and returns the value of "FontSize". 
         ''' Actually there are only two types of FrameworkElement checked: 
         ''' System.Windows.Controls.Control and System.Windows.Controls.Page
         ''' </para>
         ''' <para>
         ''' 2. If no such WPF parent is found and we are in a Windows Forms Application'
         ''' then the main form's font size is returned.
         ''' </para>
         ''' <para>
         ''' 3. If point 2) failed, then System.Windows.SystemFonts.MessageFontSize is returned.
         ''' </para>
         ''' </remarks>
        Private Function GetInheritedFontSize(StartObject As System.Windows.FrameworkElement) As Double
            Dim retFontSize As Nullable(Of Double) = Nothing
            
            If (StartObject isNot Nothing) then
                
                '1. Parent WPF control (search recursive upwards)
                Dim ParentObject  As System.Windows.FrameworkElement = StartObject.Parent
                If (ParentObject isNot Nothing) then
                    'Get FontSize if ParentObject is a descendant of one of two FrameworkElement types, that have a FontSize.
                    If (GetType(System.Windows.Controls.Control).IsInstanceOfType(ParentObject)) then
                        retFontSize = DirectCast(ParentObject, System.Windows.Controls.Control).FontSize
                    ElseIf (GetType(System.Windows.Controls.Page).IsInstanceOfType(ParentObject)) then
                        retFontSize = DirectCast(ParentObject, System.Windows.Controls.Page).FontSize
                    End If
                    
                    'Check next parent
                    If (retFontSize is Nothing) then
                        retFontSize = GetInheritedFontSize(ParentObject)
                    End if
                End if
                
                '2. Windows Forms Application' main form.
                If (retFontSize is Nothing) Then
                    If (System.Windows.Forms.Application.OpenForms.Count > 0) Then
                        'There is at least one Windows Form, hence it should be a Windows Forms Application
                        'and the first form may be the main form.
                        'Conversion factor: see http://msdn.microsoft.com/en-us/library/ms751565.aspx.
                        retFontSize = System.Windows.Forms.Application.OpenForms(0).Font.SizeInPoints * 96 / 72
                    End If
                End If
                
                '3. System default MessageFontSize
                If (retFontSize is Nothing) Then
                    retFontSize = System.Windows.SystemFonts.MessageFontSize
                End If
                
            End if
            
            Return retFontSize
        End function
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
