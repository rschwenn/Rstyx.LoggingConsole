
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Globalization

'Documentation
 ''' <summary>
 ''' <para>
 ''' Static Helper class to achieve On-the-Fly language switching at runtime
 ''' and to provide list of available cultures on UI.
 ''' </para>
 ''' <para>
 ''' Wraps up WPF Binding friendly access to instances of My.Resources.Resources and itself, provides a list of available cultures
 ''' and a property for getting and setting current culture.
 ''' </para>
 ''' </summary>
 ''' <remarks>
 ''' <para>
 ''' Origin: <a href="http://www.codeproject.com/KB/WPF/WPFLocalize.aspx" target="_blank">Andrew Wood's "WPF Runtime Localization"</a>,
 ''' which heavily inspired me to chose this way for runtime localization.
 ''' </para>
 ''' <para>
 ''' Changes to both this class and the concept:
 ''' <list type="bullet">
 ''' <item><description> Ported to Visual Basic. </description></item>
 ''' <item><description> Moved creation of ObjectDataProvider's from XAML to code in order to have easy access to ODP's from code and from more than one XAML file. </description></item>
 ''' <item><description> Added <see cref="P:InstanceProvider"/> property to allow WPF two way binding to this class. </description></item>
 ''' <item><description> Moved initilization code from constructor to properties. </description></item>
 ''' <item><description> Added Culture of main assembly resources to the <see cref="P:SupportedCultures"/> List. </description></item>
 ''' <item><description> Added <see cref="P:MainAssemblyCulture"/> property for convenience. </description></item>
 ''' <item><description> Changed <c>ChangeCulture()</c> method to <see cref="P:CurrentCulture"/> property to allow binding. </description></item>
 ''' <item><description> Removed <c>GetResourceInstance()</c> method. </description></item>
 ''' </list>
 ''' </para>
 ''' </remarks>
Public NotInheritable Class CultureResources
    
    #Region "Private Fields"
        
        Private Shared _InstanceProvider       As System.Windows.Data.ObjectDataProvider = Nothing
        Private Shared _provider               As System.Windows.Data.ObjectDataProvider = Nothing
        Private Shared _MainAssemblyCulture    As CultureInfo = Nothing
        Private Shared _SupportedCultures      As List(Of CultureInfo) = Nothing
        
        Private Shared InternalLogger          As Logger = LogBox.getLogger("LogBox.CultureResources")
        
        Private Shared ReadOnly SyncHandle     As New Object
        
    #End Region
    
    #Region "Constuctor"
        
        Private Sub New()
        End Sub
        
    #End Region
    
    #Region "Shared Properties"
        
        ''' <summary> 
        ''' Returns the one and only CultureResources instance as an ObjectDataProvider for use as a Binding source. 
        ''' </summary>
         ''' <returns> <see cref="System.Windows.Data.ObjectDataProvider" /> </returns>
         ''' <remarks> 
         ''' <para>
         ''' If the ObjectDataProvider (hence the instance of this CultureResources class) doesn't exist yet, it will be created. 
         ''' </para>
         ''' <para>
         ''' The instance is only needed to allow WPF two way binding. The Form of ObjectDataProvider is chosen
         ''' to simply update bindings to all bound properties with a single call to ObjectDataProvider.refresh().
         ''' </para>
         ''' </remarks>
        Public Shared ReadOnly Property InstanceProvider() As System.Windows.Data.ObjectDataProvider
            Get
                SyncLock (SyncHandle)
                    If (_InstanceProvider Is Nothing) Then
                        _InstanceProvider = New System.Windows.Data.ObjectDataProvider
                        _InstanceProvider.ObjectInstance = New CultureResources
                        InternalLogger.logDebug("CultureResources instantiated to allow WPF two way binding.")
                    End If
                    Return _InstanceProvider
                End SyncLock
            End Get
        End Property
        
        ''' <summary>
        ''' Returns an instance of My.Resources.Resources as an ObjectDataProvider for use as a Binding source. 
        ''' </summary>
         ''' <returns> <see cref="System.Windows.Data.ObjectDataProvider" /> </returns>
         ''' <remarks> 
         ''' <para>
         ''' If the ObjectDataProvider doesn't exist yet, it will be created. 
         ''' </para>
         ''' <para>
         ''' Binding to this property instead directly to My.Resources.Resources provides the capability
         ''' to update bindings to all bound properties with a single call to ObjectDataProvider.refresh().
         ''' </para>
         ''' </remarks>
        Public Shared ReadOnly Property ResourceProvider() As System.Windows.Data.ObjectDataProvider
            Get
                SyncLock (SyncHandle)
                    If (_provider Is Nothing) Then
                        _provider = New System.Windows.Data.ObjectDataProvider
                        _provider.ObjectInstance = New My.Resources.Resources
                    End If
                    Return _provider
                End SyncLock
            End Get
        End Property
        
        ''' <summary> List of available cultures, enumerated if not exists yet </summary>
         ''' <value>   List(Of CultureInfo) </value>
         ''' <returns> List(Of CultureInfo) </returns>
         ''' <remarks> 
         ''' <para>
         ''' If this List doesn't exist yet, it will be created. 
         ''' </para>
         ''' <para>
         ''' The first Item of this List is the Culture of neutral resources of main assembly. 
         ''' If a resource for the same culture as neutral resources of main assembly is found, it isn't added twice.
         ''' </para>
         ''' </remarks>
        Public Shared ReadOnly Property SupportedCultures() As List(Of CultureInfo)
            Get
                SyncLock (SyncHandle)
                    If (_SupportedCultures Is Nothing) Then
                        Debug.WriteLine("Get Installed cultures:")
                        _SupportedCultures = New List(Of CultureInfo)
                        
                        'Add Culture of neutral resources of main assembly, if it's not the invariant culture.
                        'If (Not MainAssemblyCulture.Equals(CultureInfo.InvariantCulture)) Then
                        _SupportedCultures.Add(MainAssemblyCulture)
                        'End If
                        
                        'Add Cultures of sattelite assemblies
                        Dim tCulture             As CultureInfo = Nothing
                        Dim ThisAssembly         As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                        Dim ThisAssemblyFileName As String = ThisAssembly.Location
                        Dim ThisAssemblyDirName  As String = System.IO.Path.GetDirectoryName(ThisAssemblyFileName)
                        
                        For Each dir As String In System.IO.Directory.GetDirectories(ThisAssemblyDirName)
                            Try
                                'see if this directory corresponds to a valid culture name
                                Dim dirinfo As New System.IO.DirectoryInfo(dir)
                                tCulture = CultureInfo.GetCultureInfo(dirinfo.Name)
                                
                                'determine if a resources dll exists in this directory that matches the executable name
                                If (dirinfo.GetFiles(System.IO.Path.GetFileNameWithoutExtension(ThisAssemblyFileName) & ".resources.dll").Length > 0) Then
                                    if (not _SupportedCultures.Contains(tCulture))
                                        _SupportedCultures.Add(tCulture)
                                        Debug.WriteLine(String.Format(" Found Culture: {0} [{1}]", tCulture.DisplayName, tCulture.Name))
                                    end if
                                End If
                            Catch generatedExceptionName As ArgumentException
                                'ignore exceptions generated for any unrelated directories in the bin folder
                            End Try
                        Next
                        
                        'Log supported cultures to LoggingConsole
                        InternalLogger.logDebug("Supported languages:")
                        For Each ci as CultureInfo in _SupportedCultures
                            InternalLogger.logDebug(String.Format(" - {0} [{1}]", ci.NativeName, ci.Name))
                        Next
                    End If
                    Return _SupportedCultures
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns the Culture of neutral resources of main assembly. </summary>
         ''' <returns> System.Globalization.CultureInfo </returns>
         ''' <remarks> 
         ''' If the main assembly doesn't have a System.Resources.NeutralResourcesLanguageAttribute, 
         ''' then the CultureInfo.InvariantCulture is returned.
         ''' </remarks>
        Public Shared ReadOnly Property MainAssemblyCulture() As CultureInfo
            Get
                SyncLock (SyncHandle)
                    If (_MainAssemblyCulture Is Nothing) Then
                        Dim ThisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                        Dim Attributes As Object() = ThisAssembly.GetCustomAttributes(GetType(System.Resources.NeutralResourcesLanguageAttribute), False)
                        
                        if (Attributes.Length > 0) then
                            Dim Attr As System.Resources.NeutralResourcesLanguageAttribute = Attributes(0)
                            _MainAssemblyCulture = New CultureInfo(Attr.CultureName)
                        Else
                            _MainAssemblyCulture = CultureInfo.InvariantCulture
                        End If
                        Debug.WriteLine(String.Format(" Culture of main assembly: {0} [{1}]", _MainAssemblyCulture.DisplayName, _MainAssemblyCulture.Name))
                    End If
                    Return _MainAssemblyCulture
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Gets or sets the culture used for retrieving resources from My.Resources.Resources. </summary>
         ''' <value>   Culture to change to </value>
         ''' <returns> Culture currently used by My.Resources.Resources.Culture /> </returns>
         ''' <remarks> 
         ''' <para> This is an extended wrapper for My.Resources.Resources.Culture /> </para>
         ''' <para> 
         ''' <b>Get:</b> In every case a supported culture is returned: 
         ''' If the retrieved culture isn't actually supported, but it's parent is, then the parent is returned. 
         ''' Otherwise the first culture of <see cref="CultureResources.SupportedCultures" /> is returned,
         ''' which should be the culture of main assembly.
         ''' </para>
         ''' <para> 
         ''' <b>Set:</b> If the desired culture is not available then nothing is done. Otherwise it is set as current and 
         ''' the <see cref="CultureResources.ResourceProvider"/> as well as the <see cref="CultureResources.InstanceProvider"/>
         ''' are refreshed, which means that all dependent WPF binding targets are updated. 
         ''' These are resource strings (localized elements) and all properties of this class.
         ''' </para>
         ''' </remarks>
        Public Shared Property CurrentCulture() As CultureInfo
            Get
                SyncLock (SyncHandle)
                    Dim resCulture As CultureInfo = Nothing
                    
                    If (My.Resources.Resources.Culture isNot Nothing) then
                        resCulture = My.Resources.Resources.Culture
                        InternalLogger.logDebug(String.Format("CurrentCulture[Get]: My.Resources.Resources.Culture = [{0}]", resCulture.Name))
                    Else
                        InternalLogger.logDebug("CurrentCulture[Get]: My.Resources.Resources.Culture = null")
                        resCulture = CultureInfo.CurrentUICulture
                        InternalLogger.logDebug(String.Format("CurrentCulture[Get]: CultureInfo.CurrentUICulture = [{0}]", resCulture.Name))
                    End If
                    
                    'Try fallback to match a suported culture.
                    if (not SupportedCultures.Contains(resCulture)) then
                        InternalLogger.logDebug(String.Format("CurrentCulture[Get]: [{0}] is not supported.", resCulture.Name))
                        if (SupportedCultures.Contains(resCulture.Parent)) then
                            resCulture = resCulture.Parent
                        else
                            resCulture = _SupportedCultures(0)
                        end if
                        InternalLogger.logDebug(String.Format("CurrentCulture[Get]: Instead use [{0}].", resCulture.Name))
                    end if
                    Return resCulture
                End SyncLock
            End Get
            Set(value As CultureInfo)
                SyncLock (SyncHandle)
                    If (value isNot My.Resources.Resources.Culture) Then
                        If (not SupportedCultures.Contains(value)) Then
                            Debug.WriteLine(String.Format("Culture [{0}] not available - not changed anything.", value))
                            InternalLogger.logDebug(String.Format("Culture [{0}] not available - not changed anything.", value))
                        Else
                            Debug.WriteLine(String.Format("Switch to Culture [{0}]", value))
                            InternalLogger.logDebug(String.Format("Switch to Culture [{0}]", value))
                            
                            My.Resources.Resources.Culture = value
                            InstanceProvider.Refresh()
                            ResourceProvider.Refresh()
                            OnCultureChanged()
                        End If
                    End If
                End SyncLock
            End Set
        End Property
        
    #End Region
    
    #Region "Events"
        
        ''' <summary> Raises when CurrentCulture has changed. </summary>
        Public Shared Event CultureChanged As System.EventHandler
        
        ''' <summary> Raises the CultureChanged event. </summary>
         ''' <remarks> This event indicates that <see cref="CurrentCulture" /> has changed. </remarks>
        Private Shared Sub OnCultureChanged()
            Try
                RaiseEvent CultureChanged(InstanceProvider, New System.EventArgs)
            Catch ex As System.Exception
                InternalLogger.logError(ex, "OnCultureChanged: " & My.Resources.Resources.Global_ErrorInEventHandler)
            End Try
        End Sub
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=3::tabSize=4::indentSize=4:
