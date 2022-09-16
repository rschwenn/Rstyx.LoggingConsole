
Imports System
Imports System.Windows.Input
Imports System.Windows.Media.Imaging


'Namespace UI.ViewModel
    
    ''' <summary> A Command for use in UI elements. </summary>
     ''' <remarks>
     ''' <para>
     ''' Josh Smith: "A command whose sole purpose is to relay its functionality to other objects
     ''' by invoking delegates. The default return value for the CanExecute method is 'true'".
     ''' </para>
     ''' <para>
     ''' Real changes only are added properties for convienient use in conjunction with UI elements, 
     ''' which I guess destroys the clean MVVM pattern - apologies for that!
     ''' </para>
     ''' </remarks>
    Public NotInheritable Class RelayCommand
        Implements ICommand
        
        #Region "Private Fields"
            
            Private ReadOnly _CanExecuteMethod  As Predicate(Of Object) = Nothing
            Private ReadOnly _ExecuteMethod     As Action(Of Object) = Nothing
            
            Private _Icon                       As BitmapSource = Nothing
            Private _IconBrush                  As System.Windows.Media.DrawingBrush = Nothing
            Private _IconRectangle              As System.Windows.Shapes.Rectangle = Nothing
            Private _Caption                    As String = String.Empty
            Private _Description                As String = String.Empty
            Private _CaptionResourceName        As String = String.Empty
            Private _DescriptionResourceName    As String = String.Empty
            
        #End Region
        
        #Region "Constructors"
            
            ''' <summary> Creates a new command that can always execute. </summary>
             ''' <param name="ExecuteMethod"> The execution logic. </param>
             ''' <exception cref="ArgumentNullException"> <paramref name="ExecuteMethod"/> is <see langword="null"/>. </exception>
            Public Sub New(ByVal ExecuteMethod As Action(Of Object))
                Me.New(ExecuteMethod, Nothing)
            End Sub
            
            ''' <summary> Creates a new command. </summary>
             ''' <param name="ExecuteMethod"> The execution logic. </param>
             ''' <param name="CanExecuteMethod"> The execution status logic. </param>
             ''' <exception cref="ArgumentNullException"> <paramref name="ExecuteMethod"/> is <see langword="null"/>. </exception>
            Public Sub New(ByVal ExecuteMethod As Action(Of Object), ByVal CanExecuteMethod As Predicate(Of Object))
                
                If (ExecuteMethod Is Nothing) Then Throw New ArgumentNullException("ExecuteMethod", "Delegate commands can not be null")
                
                _ExecuteMethod = ExecuteMethod
                _CanExecuteMethod = CanExecuteMethod
            End Sub
            
        #End Region
        
        #Region "Properties" 
            
            ''' <summary> A bitmap Icon for the command, i.e. to display on a button. </summary>
            Public Property Icon() As BitmapSource
                Get
                    Icon = _Icon
                End Get
                Set(value As BitmapSource)
                    _Icon = value
                End Set
            End Property
            
            ''' <summary> A vector brush for drawing an Icon for the command, i.e. to display on a button. </summary>
            Public Property IconBrush() As System.Windows.Media.DrawingBrush
                Get
                    IconBrush = _IconBrush
                End Get
                Set(value As System.Windows.Media.DrawingBrush)
                    _IconBrush = value
                End Set
            End Property
            
            ''' <summary> A vector Icon for the command, i.e. to display on a button. </summary>
            ''' <remarks> Deprecated: The rectangle isn't a freezable and can be used only once! </remarks>
            Public Property IconRectangle() As System.Windows.Shapes.Rectangle
                Get
                    IconRectangle = _IconRectangle
                End Get
                Set(value As System.Windows.Shapes.Rectangle)
                    _IconRectangle = value
                End Set
            End Property
            
            ''' <summary> A Caption for the command, i.e. to display on a button. </summary>
             ''' <remarks> If <see cref="CaptionResourceName" /> is not empty, then the Caption is read from resources. </remarks>
            Public Property Caption() As String
                Get
                    if (not String.IsNullOrEmpty(_CaptionResourceName)) then
                        Caption = My.Resources.Resources.ResourceManager.GetString(_CaptionResourceName, My.Resources.Resources.Culture)
                    else
                        Caption = _Caption
                    end if
                End Get
                Set(value As String)
                    _Caption = value
                End Set
            End Property
            
            ''' <summary> A Name of a String Resource to retrieve the Caption. </summary>
            Public Property CaptionResourceName() As String
                Get
                    CaptionResourceName = _CaptionResourceName
                End Get
                Set(value As String)
                    _CaptionResourceName = value
                End Set
            End Property
            
            ''' <summary> A Description for the command, i.e. to display as tooltip. </summary>
             ''' <remarks> If <see cref="DescriptionResourceName" /> is not empty, then the Description is read from resources. </remarks>
            Public Property Description() As String
                Get
                    if (not String.IsNullOrEmpty(_DescriptionResourceName)) then
                        Description = My.Resources.Resources.ResourceManager.GetString(_DescriptionResourceName, My.Resources.Resources.Culture)
                    else
                        Description = _Description
                    end if
                End Get
                Set(value As String)
                    _Description = value
                End Set
            End Property
            
            ''' <summary> A Name of a String Resource to retrieve the Description. </summary>
            Public Property DescriptionResourceName() As String
                Get
                    DescriptionResourceName = _DescriptionResourceName
                End Get
                Set(value As String)
                    _DescriptionResourceName = value
                End Set
            End Property
            
        #End Region
        
        #Region "Events"
            
            ''' <summary>
            ''' see <a href="http://msdn.microsoft.com/de-de/library/system.windows.input.icommand.aspx" target="_blank"> System.Windows.Input.ICommand </a>
            ''' </summary>
            Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
                
                AddHandler(ByVal value As EventHandler)
                    If (_CanExecuteMethod IsNot Nothing) Then
                        AddHandler CommandManager.RequerySuggested, value
                    End If
                End AddHandler
                
                RemoveHandler(ByVal value As EventHandler)
                    If (_CanExecuteMethod IsNot Nothing) Then
                        RemoveHandler CommandManager.RequerySuggested, value
                    End If
                End RemoveHandler
                
                RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
                    If (_CanExecuteMethod IsNot Nothing) Then
                        CommandManager.InvalidateRequerySuggested()
                    End If
                End RaiseEvent
            End Event
            
        #End Region
        
        #Region "Methods"
            
            ''' <summary>
            ''' See <a href="http://msdn.microsoft.com/de-de/library/system.windows.input.icommand.aspx" target="_blank"> System.Windows.Input.ICommand </a>
            ''' </summary>
             ''' <param name="parameter"> data for the command </param>
             ''' <returns> Boolean </returns>
            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
                
                If (_CanExecuteMethod Is Nothing) Then
                    Return True
                Else
                    Return _CanExecuteMethod(parameter)
                End If
                
            End Function
            
            ''' <summary>
            ''' See <a href="http://msdn.microsoft.com/de-de/library/system.windows.input.icommand.aspx" target="_blank"> System.Windows.Input.ICommand </a>
            ''' </summary>
             ''' <param name="parameter"> data for the command </param>
            Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
                
                If (_ExecuteMethod IsNot Nothing) Then
                    _ExecuteMethod(parameter)
                End If
                
            End Sub
            
        #End Region
    
    End Class
    
'End Namespace

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
