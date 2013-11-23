
Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq

''' <summary>
''' Base class for all ViewModel classes in the application. It provides support for 
''' property change notifications and has a DisplayName property.  This class is abstract.
''' </summary>
 ''' <remarks>
 ''' Origin:  <a href="http://msdn.microsoft.com/magazine/dd419663.aspx" target="_blank"> Josh Smith's MvvmDemoApp </a><br />
 ''' Changes: Detect Changes of My.Settings and raises "PropertyChanged()" for a local property with same name.
 ''' </remarks>
Public MustInherit Class ViewModelBase
    Implements INotifyPropertyChanged, IDisposable
    
    #Region "Declarations"
        
        'Private Shared InternalLogger  As Logger = LogBox.getLogger("LogBox.ViewModelBase")
        
        Private privateDisplayName                 As String
        Private privateThrowOnInvalidPropertyName  As Boolean
        
    #End Region
    
    #Region "Constuctors and Finalizers"
        
        Protected Sub New()
            'Subscribe for notifications of user settings changes.
            AddHandler My.Settings.PropertyChanged, AddressOf OnUserSettingsChanged
        End Sub
        
        ''' <summary> Finalizes the object. </summary>
        Protected Overrides Sub Finalize()
            'MyBase.Finalize()
            
            ' Too late:
            'RemoveHandler My.Settings.PropertyChanged, AddressOf OnUserSettingsChanged
            
            #If DEBUG Then
                'Useful for ensuring that ViewModel objects are properly garbage collected.
                Dim msg As String = String.Format("{0} ({1}) ({2}) Finalized", Me.GetType().Name, Me.DisplayName, Me.GetHashCode())
                System.Diagnostics.Debug.WriteLine(msg)
            #End If
        End Sub
        
    #End Region
    
    #Region "DisplayName"
        
        ''' <summary> The user-friendly name of this object. </summary>
        Public Overridable Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property
        
    #End Region
    
    #Region "Debugging Aides"
        
        ''' <summary>
        ''' Warns the developer if this object does not have
        ''' a public property with the specified name. This 
        ''' method does not exist in a Release build.
        ''' </summary>
         ''' <param name="propertyName"> The property that has to be verified. </param>
        <Conditional("DEBUG"), DebuggerStepThrough()> _
        Public Sub VerifyPropertyName(ByVal propertyName As String)
            ' Verify that the property name matches a real,  
            ' public, instance property on this object.
            If ((From pi As System.Reflection.PropertyInfo In MyClass.GetType.GetProperties() Where pi.Name = propertyName.Replace("[]", String.Empty)).Count < 1) Then
                Dim msg As String = "Invalid property name: " & propertyName
                
                If Me.ThrowOnInvalidPropertyName Then
                    Throw New Exception(msg)
                Else
                    Debug.Fail(msg)
                End If
            End If
        End Sub
        
        ''' <summary>
        ''' Returns whether an exception is thrown, or if a Debug.Fail() is used
        ''' when an invalid property name is passed to the VerifyPropertyName method.
        ''' The default value is false, but subclasses used by unit tests might 
        ''' override this property's getter to return true.
        ''' </summary>
        Protected Overridable Property ThrowOnInvalidPropertyName() As Boolean
            Get
                Return privateThrowOnInvalidPropertyName
            End Get
            Set(ByVal value As Boolean)
                privateThrowOnInvalidPropertyName = value
            End Set
        End Property
        
    #End Region
    
    #Region "Event Handlers"
        
        ''' <summary>
        ''' Automatically raises "PropertyChanged()" for a local property
        ''' when a coresponding My.Settings setting (with same name) is changed.
        ''' </summary>
         ''' <remarks>
         ''' If reading this property is simlpy forwarded to read the My.Settings setting
         ''' directly, then all is automatic.
         ''' </remarks>
        Private Sub OnUserSettingsChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)
            Try
                If (Not (System.ComponentModel.TypeDescriptor.GetProperties(Me)(e.PropertyName) Is Nothing)) Then
                    Me.OnPropertyChanged(e.PropertyName)
                End If
            Catch ex As System.Exception
                'InternalLogger.logError(ex, "OnUserSettingsChanged(): " & My.Resources.Resources.Global_ErrorMaybeInEventHandler)
                System.Diagnostics.Trace.WriteLine(ex)
            End Try
        End Sub
        
    #End Region
    
    #Region "INotifyPropertyChanged Members"
        
        ''' <summary>  Raised when a property on this object has a new value. </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        
        ''' <summary> Raises this object's PropertyChanged event. </summary>
         ''' <param name="propertyName"> The property that has a new value. </param>
        Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
            Me.VerifyPropertyName(propertyName)
            
            Dim handler As PropertyChangedEventHandler = Me.PropertyChangedEvent
            If handler IsNot Nothing Then
                handler.Invoke(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub
        
    #End Region
    
    #Region "IDisposable Members"
        
        ''' <summary>
        ''' Invoked when this object is being removed from the application
        ''' and will be subject to garbage collection.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.OnDispose()
        End Sub
        
        ''' <summary>
        ''' Child classes can override this method to perform 
        ''' clean-up logic, such as removing event handlers.
        ''' </summary>
        Protected Overridable Sub OnDispose()
        End Sub
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=3::tabSize=4::indentSize=4:
