
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Collections.ObjectModel
Imports System.Collections.Generic

''' <summary> The LogBox is the <b>Hub of LoggingConsole</b>, because it's the main view model, the hub, the dispatcher. </summary>
 ''' <remarks>
 ''' <para>
 ''' The LogBox is the one class that <b>provides access to all available components, methods and properties</b> of LoggingConsole.
 ''' </para>
 ''' <para>
 ''' <b>Instantiation:</b> The LogBox cannot be instanciated directly.
 ''' Instead, use the static <see cref="LogBox.Instance"/> method to
 ''' return <b>the one and only LogBox instance</b> which will be created if it doesn't exist yet.
 ''' </para>
 ''' <para>
 ''' A <b><see cref="LoggingConsole.Logger"/></b> is needed to log messages.
 ''' You can get one only by using the static <see cref="LogBox.getLogger"/> method.
 ''' This returns the Logger instance with the given Name which will be created if it doesn't exist yet.
 ''' All Loggers are hold by the LogBox, so you can get the same Logger by calling
 ''' <c>LogBox.getLogger("My.Name")</c> from any place.
 ''' </para>
 ''' <para>
 ''' <b>Console:</b> Almost all viewer related tasks are done with the one <see cref="LoggingConsole.Console"/>
 ''' connected to this LogBox instance. You can get it by the <see cref="LogBox.Console"/> property. 
 ''' There is one exception: To show the <b>built-in floating window with embedded ConsoleView</b> 
 ''' call <see cref="LogBox.showFloatingConsoleView"/>.
 ''' The title of this window is <see cref="LogBox.DisplayName"/>.
 ''' </para>
 ''' </remarks>
Public NotInheritable Class LogBox
    Inherits ViewModelBase
    
    #Region "Private Fields"
        
        Private Shared ReadOnly SyncHandle          As Object
        Private Shared _LogBox                      As LogBox
        Private Shared InternalLogger               As Logger
        
        Private _MessageStore                       As MessageStore = Nothing
        Private _Console                            As Console = Nothing
        Private LoggerSet                           As Dictionary(Of String, Logger)
        Private FloatingWindow                      As ConsoleWindow = Nothing
        Private OwnerWindow                         As Window = Nothing
        
        Private _isFloatingConsoleModal             As Boolean = false
        Private IsDisplayNameResource               As Boolean = false
        
        Private _showFloatingConsoleViewAction      As Action(Of Boolean) = Nothing
        Private _hideFloatingConsoleViewAction      As Action = Nothing
        
        Private _ClearLogCommand                    As RelayCommand = Nothing
        Private _SaveLogCommand                     As RelayCommand = Nothing
        Private _ResetSettingsCommand               As RelayCommand = Nothing
        Private _HideFloatingConsoleViewCommand     As RelayCommand = Nothing
        Private _ShowFloatingConsoleViewCommand     As RelayCommand = Nothing
        Private _ShowAboutBoxCommand                As RelayCommand = Nothing
        
        Private Const DefaultLoggerKey              As String = "%$D1e5f9a4u3l5t0L6o8g#g.e-r+Ke=y$%"
        
    #End Region
    
    #Region "Constuctors and Finalizers"
        
        ''' <summary> The LogBox instance will be created and initialized. </summary>
         ''' <remarks> Explicit static constructor to tell C# compiler not to mark type as beforefieldinit. </remarks>
        Shared Sub New()
            SyncHandle = New Object()
            _LogBox    = New LogBox()
            
            'Listen for some events
            AddHandler _LogBox.MessageStore.ErrorLogged, AddressOf _LogBox.OnNewErrorLogged
            AddHandler CultureResources.CultureChanged,  AddressOf _LogBox.OnCultureChanged
            
            'Log some info.
            InternalLogger = getLogger("LogBox")
            
            Dim ThisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim ThisProcess  As System.Diagnostics.Process = Process.GetCurrentProcess()
            Dim MainModule   As System.Diagnostics.ProcessModule = ThisProcess.MainModule
            
            InternalLogger.logDebug("ExecutingAssembly FullName = "  & ThisAssembly.FullName)
            InternalLogger.logDebug("ExecutingAssembly Location = "  & ThisAssembly.Location)
            
            InternalLogger.logDebug("Main Module = " & MainModule.FileName)
            InternalLogger.logDebug("Command Line = " & System.Environment.CommandLine)
            InternalLogger.logDebug("Current Directory = " & System.Environment.CurrentDirectory)
            InternalLogger.logDebug("Current Process = " & If(System.Environment.Is64BitProcess, "x64", "x32"))
            
            Try
                InternalLogger.logDebug("Operating System = " & System.Environment.OSVersion.ToString() & " (" & If(System.Environment.Is64BitOperatingSystem, "x64", "x32") & ")")
                InternalLogger.logDebug(".NET Framework = " & System.Environment.Version.ToString())
                InternalLogger.logDebug("Processor Count = " & System.Environment.ProcessorCount.ToString())
                InternalLogger.logDebug("Machine Name = " & System.Environment.MachineName)
                InternalLogger.logDebug("UserDomain = " & System.Environment.UserDomainName)
            Catch ex As Exception
            End Try
            InternalLogger.logDebug("User = " & System.Environment.UserName)
            InternalLogger.logDebug("interactive Session = " & System.Environment.UserInteractive.ToString())
            
            InternalLogger.logDebug("CultureInfo.InstalledUICulture = " & System.Globalization.CultureInfo.InstalledUICulture.Name)
            InternalLogger.logDebug("CultureInfo.CurrentCulture = "     & System.Globalization.CultureInfo.CurrentCulture.Name)
            InternalLogger.logDebug("CultureInfo.CurrentUICulture = "   & System.Globalization.CultureInfo.CurrentUICulture.Name)
            
            InternalLogger.logDebug(My.Resources.Resources.LogBox_LogInitialized)
        End Sub
        
        ''' <summary> Instantiates the one and only LogBox instance and initializes instance fields. </summary>
         ''' <remarks> 
         ''' Initially a default DisplayName is used, but this can be changed by setting the 
         ''' <see cref="Console.DisplayName"/> property.
         ''' </remarks>
        Private Sub New()
            IsDisplayNameResource = True
            MyBase.DisplayName = My.Resources.Resources.LogBox_DefaultDisplayName
            
            LoggerSet = New Dictionary(Of String, Logger)
        End Sub
        
        ''' <summary> Saves the application settings at shutdown. This is called automatically. </summary>
        Protected Overrides Sub Finalize()
            My.Settings.Save()
            MyBase.Finalize()
        End Sub
        
    #End Region
    
    #Region "Static Members"
        
        ''' <summary> Returns the one and only LogBox instance. </summary>
         ''' <returns> The <see cref="LoggingConsole.LogBox"/> instance. </returns>
        Public Shared ReadOnly Property Instance() As LogBox
            Get
                Return _LogBox
            End Get
        End Property
        
        ''' <summary> Returns the Version of LoggingConsole. </summary>
         ''' <returns> Version As String </returns>
        Public Shared ReadOnly Property Version() As String
            Get
                Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
            End Get
        End Property
        
        ''' <summary> Returns the Copyright of LoggingConsole. </summary>
         ''' <returns> Copyright String </returns>
        Public Shared ReadOnly Property Copyright() As String
            Get
                Return DirectCast(System.Reflection.Assembly.GetExecutingAssembly(). _
                    GetCustomAttributes(GetType(System.Reflection.AssemblyCopyrightAttribute), False)(0), System.Reflection.AssemblyCopyrightAttribute).Copyright
            End Get
        End Property
        
        ''' <summary> Returns the Logger instance with the empty <see cref="LoggingConsole.Logger.Name"/>. </summary>
         ''' <returns> The requested <see cref="LoggingConsole.Logger"/> instance. </returns>
         ''' <remarks> If the Logger with the empty Name doesn't exist yet, it will be created and stored in the internal set of Loggers. </remarks>
        Public Shared Function getLogger() As Logger
            SyncLock (SyncHandle)
                Return getLogger(String.Empty)
            End SyncLock
        End Function
        
        ''' <summary> Returns the Logger instance with the given <see cref="LoggingConsole.Logger.Name"/>. </summary>
         ''' <param name="LoggerName"> The Name of the requested Logger. If <see langword="null"/>, empty or white space, the empty name is used. </param>
         ''' <returns> The requested <see cref="LoggingConsole.Logger"/> instance. </returns>
         ''' <remarks>  If the Logger with the given Name doesn't exist yet, it will be created and stored in the internal set of Loggers.  </remarks>
        Public Shared Function getLogger(ByVal LoggerName As String) As Logger
            SyncLock (SyncHandle)
                ' Key for this new Logger in LoggerSet Dictionary.
                Dim LoggerKey As String
                If (String.IsNullOrWhiteSpace(LoggerName)) Then
                    LoggerKey = DefaultLoggerKey
                Else 
                    LoggerKey = LoggerName
                End If
                
                ' Create Logger if needed.
                If (Not _LogBox.LoggerSet.ContainsKey(LoggerKey)) Then
                    _LogBox.LoggerSet.Add(LoggerKey, New Logger(LoggerName, _LogBox))
                End If
                
                Return _LogBox.LoggerSet.Item(LoggerKey)
            End SyncLock
        End Function
        
    #End Region
    
    #Region "Instance Properties"
        
        ''' <summary> Returns the one <see cref="LoggingConsole.Console"/> connected to this LogBox. </summary>
         ''' <remarks> The Console will be created if it doesn't exist yet </remarks>
        Public ReadOnly Property Console() As Console
            Get
                SyncLock (SyncHandle)
                    If (_Console Is Nothing) Then
                        _Console = New Console(Me)
                    End If
                    Return _Console
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns the one <see cref="LoggingConsole.MessageStore"/> connected to this LogBox. </summary>
         ''' <remarks> The MessageStore will be created if it doesn't exist yet </remarks>
        Public ReadOnly Property MessageStore() As MessageStore
            Get
                SyncLock (SyncHandle)
                    If (_MessageStore Is Nothing) Then
                        _MessageStore = New MessageStore(Me)
                    End If
                    Return _MessageStore
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Indicates whether or not exception header lines should be logged as debug level. Defaults to "true". </summary>
         ''' <remarks>
         ''' The exception header line is the one that begins with "***Exception in ...".
         ''' If the exception message is empty, the header line will be handled as error level anyway.
         ''' </remarks>
        Public Property ExceptionHeaderAsDebug() As Boolean = True
        
        ''' <summary> Indicates whether or not the floating ConsoleWindow should be shown modal. Defaults to "false"</summary>
        Public Property IsFloatingConsoleModal() As Boolean
            Get
                IsFloatingConsoleModal = _isFloatingConsoleModal
            End Get
            Set(value As Boolean)
                _isFloatingConsoleModal = value
            End Set
        End Property
        
        ''' <summary> Gets or sets the Action to perform, when <see cref="LogBox.showFloatingConsoleView"/> is invoked. </summary>
         ''' <remarks> 
         ''' <para>
         ''' It defaults to <see cref="LogBox.showBuiltinFloatingConsoleView"/>. 
         ''' </para>
         ''' <para>
         ''' CAUTION: If this property is set to a custom Action, then the corresponding
         ''' <see cref="LoggingConsole.LogBox.HideFloatingConsoleViewAction"/> property must be set, too. 
         ''' </para>
         ''' </remarks>
        Public Property ShowFloatingConsoleViewAction() As Action(Of Boolean)
            Get
                If (_showFloatingConsoleViewAction is Nothing) Then
                    _showFloatingConsoleViewAction = AddressOf showBuiltinFloatingConsoleView
                End If
                ShowFloatingConsoleViewAction = _showFloatingConsoleViewAction
            End Get
            Set(ByVal value As Action(Of Boolean))
                _showFloatingConsoleViewAction = value
            End Set
        End Property
        
        ''' <summary> Gets or sets the Action to perform, when <see cref="LogBox.hideFloatingConsoleView"/> is invoked. </summary>
         ''' <remarks> 
         ''' <para>
         ''' It defaults to <see cref="LogBox.hideBuiltinFloatingConsoleView"/>. 
         ''' </para>
         ''' <para>
         ''' This property corresponds to the <see cref="LoggingConsole.LogBox.ShowFloatingConsoleViewAction"/> property. 
         ''' </para>
         ''' <para>
         ''' CAUTION: If this property is set to a custom Action, then the corresponding
         ''' <see cref="LoggingConsole.LogBox.ShowFloatingConsoleViewAction"/> property must be set, too. 
         ''' </para>
         ''' </remarks>
        Public Property HideFloatingConsoleViewAction() As Action
            Get
                If (_hideFloatingConsoleViewAction is Nothing) Then
                    _hideFloatingConsoleViewAction = AddressOf hideBuiltinFloatingConsoleView
                End If
                HideFloatingConsoleViewAction = _hideFloatingConsoleViewAction
            End Get
            Set(ByVal value As Action)
                _hideFloatingConsoleViewAction = value
            End Set
        End Property
        
        #Region "Commands"
            
            ''' <summary> Returns the Command which clears the whole log. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.clearLog"/> </remarks>
            Public ReadOnly Property ClearLogCommand() As RelayCommand
                Get
                    If _ClearLogCommand Is Nothing Then
                        '_ClearLogCommand = New RelayCommand(AddressOf Me.clearLog, AddressOf Me.CanClearLog)
                        _ClearLogCommand = New RelayCommand(AddressOf Me.clearLog)
                        _ClearLogCommand.CaptionResourceName = "LogBox_ClearLogCommand_Caption"
                        _ClearLogCommand.DescriptionResourceName = "LogBox_ClearLogCommand_Description"
                        _ClearLogCommand.IconBrush = UIResources.IconBrush("Handmade_delete2")
                    End If
                    
                    Return _ClearLogCommand
                End Get
            End Property
            
            ''' <summary> Returns the Command which saves the currently shown log to a file. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.saveActiveLog"/> </remarks>
            Public ReadOnly Property SaveLogCommand() As RelayCommand
                Get
                    If _SaveLogCommand Is Nothing Then
                        _SaveLogCommand = New RelayCommand(AddressOf Me.saveActiveLog)
                        _SaveLogCommand.CaptionResourceName = "LogBox_SaveLogCommand_Caption"
                        _SaveLogCommand.DescriptionResourceName = "LogBox_SaveLogCommand_Description"
                        _SaveLogCommand.IconBrush = UIResources.IconBrush("Tango_DocumentSave1")
                    End If
                    
                    Return _SaveLogCommand
                End Get
            End Property
            
            ''' <summary> Returns the Command which saves all application settings. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.resetSettings"/> </remarks>
            Public ReadOnly Property ResetSettingsCommand() As RelayCommand
                Get
                    If _ResetSettingsCommand Is Nothing Then
                        _ResetSettingsCommand = New RelayCommand(AddressOf Me.resetSettings)
                        _ResetSettingsCommand.CaptionResourceName = "LogBox_ResetSettingsCommand_Caption"
                        _ResetSettingsCommand.DescriptionResourceName = "LogBox_ResetSettingsCommand_Description"
                        _ResetSettingsCommand.IconBrush = UIResources.IconBrush("Tango_Gear1")
                    End If
                    
                    Return _ResetSettingsCommand
                End Get
            End Property
            
            ''' <summary> Returns the Command which hides the floating ConsoleView. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.hideFloatingConsoleView"/> </remarks>
            Public ReadOnly Property HideFloatingConsoleViewCommand() As RelayCommand
                Get
                    If _HideFloatingConsoleViewCommand Is Nothing Then
                        _HideFloatingConsoleViewCommand = New RelayCommand(AddressOf Me.hideFloatingConsoleView)
                        _HideFloatingConsoleViewCommand.CaptionResourceName = "LogBox_HideFloatingConsoleViewCommand_Caption"
                        _HideFloatingConsoleViewCommand.DescriptionResourceName = "LogBox_HideFloatingConsoleViewCommand_Description"
                        _HideFloatingConsoleViewCommand.IconBrush = UIResources.IconBrush("Handmade_Power4")
                    End If
                    
                    Return _HideFloatingConsoleViewCommand
                End Get
            End Property
            
            ''' <summary> Returns the Command which shows the floating ConsoleView. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.showFloatingConsoleView"/> </remarks>
            Public ReadOnly Property ShowFloatingConsoleViewCommand() As RelayCommand
                Get
                    If _ShowFloatingConsoleViewCommand Is Nothing Then
                        _ShowFloatingConsoleViewCommand = New RelayCommand(AddressOf Me.showFloatingConsoleView)
                        _ShowFloatingConsoleViewCommand.CaptionResourceName = "LogBox_ShowFloatingConsoleViewCommand_Caption"
                        _ShowFloatingConsoleViewCommand.DescriptionResourceName = "LogBox_ShowFloatingConsoleViewCommand_Description"
                        _ShowFloatingConsoleViewCommand.IconBrush = UIResources.IconBrush("SheetPenx1")
                    End If
                    
                    Return _ShowFloatingConsoleViewCommand
                End Get
            End Property
            
            ''' <summary> Returns the Command which Shows the AboutBox. </summary>
             ''' <remarks> The Command will be created if it doesn't exist yet. It utilizes <see cref="LogBox.ShowAboutBox"/> </remarks>
            Public ReadOnly Property ShowAboutBoxCommand() As RelayCommand
                Get
                    If _ShowAboutBoxCommand Is Nothing Then
                        _ShowAboutBoxCommand = New RelayCommand(AddressOf Me.ShowAboutBox)
                        _ShowAboutBoxCommand.CaptionResourceName = "LogBox_ShowAboutBoxCommand_Caption"
                        _ShowAboutBoxCommand.DescriptionResourceName = "LogBox_ShowAboutBoxCommand_Description"
                        _ShowAboutBoxCommand.IconBrush = UIResources.IconBrush("Info1_Blue")
                    End If
                    
                    Return _ShowAboutBoxCommand
                End Get
            End Property
            
        #End Region
        
    #End Region
    
    #Region "Settings"
        'These are properties that have a corresponding user setting
        'with the same name that are read and written directly.
        'We listen for changing of user settings and fire MyBase.OnPropertyChanged() for the
        'property name, so the world can take notice of changing of these properties here.
        'See MyBase.OnUserSettingsChanged().
        
        ''' <summary> Should the <see cref="LoggingConsole.ConsoleWindow"/> automatically be shown when an error message is logged? </summary>
         ''' <value>   Boolean </value>
         ''' <returns> Boolean </returns>
         ''' <remarks> 
         ''' <para> This is an "application setting". </para>
         ''' <para> If this is enabled and an error message is logged, the following happens: </para>
         ''' <para> If the <see cref="LoggingConsole.ConsoleView"/> isn't embedded into any other window yet,
         ''' then the built-in <see cref="LoggingConsole.ConsoleWindow"/> is shown. 
         ''' If the ConsoleView is already embedded into any other window, nothing happens unless 
         ''' the parent application responds to this setting in an appropriate manner. 
         ''' </para>
         ''' </remarks>
        Public Property showConsoleOnError() As Boolean
            Get
                showConsoleOnError = My.Settings.showConsoleOnError
            End Get
            Set(value As Boolean)
                My.Settings.showConsoleOnError = value
            End Set
        End Property
        
    #End Region
    
    #Region "Instance Methods"
        
        ''' <summary> Clears the whole log. </summary>
        Public Sub clearLog()
            Try
                'Clears the complete message list.
                Me.MessageStore.clearLog()
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Saves a log to a file. </summary>
         ''' <param name="LogLevel">    The detail level to save. </param>
         ''' <param name="LogFilePath"> The target file. If omitted or empty, a file dialog is shown. </param>
         ''' <returns> The file path, which the log has been written to. </returns>
         ''' <remarks> The current view settings are used to determine, which fields of each <see cref="LoggingConsole.LogEntry"/> are written! </remarks>
        Public Function saveLog(byVal LogLevel As LogLevelEnum, Optional ByVal LogFilePath As String = "") As String
            Dim oLogEntry     As LogEntry
            Dim LogLevelName  As String = WpfUtils.LogLevelConverter.Convert(LogLevel, GetType(String), Nothing, Nothing)
            
            'Get a filename from dialog
            If (LogFilePath = String.Empty) Then
                Dim Title As String = String.Format(My.Resources.Resources.LogBox_SaveLogDialog_Title, LogLevelName)
                'If (Me.LogSource <> "") Then InitialFilename = Me.LogSource & "_"
                Dim InitialFilename  As String = LogLevelName & My.Resources.Resources.LogBox_SaveLogDialog_DefaultExt
                Dim InitialDirectory As String = Microsoft.VisualBasic.FileIO.FileSystem.CurrentDirectory()
                
                Dim dialog As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog()
                dialog.InitialDirectory = InitialDirectory
                dialog.FileName = InitialFilename
                dialog.DefaultExt = My.Resources.Resources.LogBox_SaveLogDialog_DefaultExt
                dialog.Title = Title
                dialog.Filter = String.Format("{0} (*{1})| *{1}", My.Resources.Resources.LogBox_SaveLogDialog_Logfiles, My.Resources.Resources.LogBox_SaveLogDialog_DefaultExt)
                'Sample Filter: "Log files (*.log)| *.log"
                
                if (dialog.ShowDialog()) then
                    LogFilePath = dialog.FileName
                    InternalLogger.logDebug(String.Format(String.Format("Logging Console: {0}.", My.Resources.Resources.LogBox_SaveLogDialog_FilenameReturned), LogFilePath))
                else
                    InternalLogger.logDebug(String.Format("Logging Console: {0}.", My.Resources.Resources.LogBox_SaveLogDialog_Aborted))
                end if
            End If
            
            'Write log file
            If (not (LogFilePath = String.Empty)) Then
                'Options
                Dim includeLineNo   As Boolean = Me.Console.showColumnLineNo
                Dim includeDate     As Boolean = Me.Console.showColumnDate
                Dim includeTime     As Boolean = Me.Console.showColumnTime
                Dim includeLevel    As Boolean = Me.Console.showColumnLevel
                Dim includeSource   As Boolean = Me.Console.showColumnSource
                
                Dim TextLine        As String = String.Empty
                
                'Open file
                Try
                    Dim oSW as System.IO.StreamWriter = System.IO.File.CreateText(LogFilePath)
                    
                    'Process all messages
                    For i As Integer = 0 To Me.MessageStore.Messages(LogLevel).Count - 1
                        oLogEntry = Me.MessageStore.Messages.item(LogLevel).item(i)
                        'Create message
                        TextLine = String.Empty
                        if (includeLineNo) then TextLine = TextLine & String.Format("{0,10}", oLogEntry.LineNo)
                        if (includeDate)   then TextLine = TextLine & String.Format("{0,12}", oLogEntry.Date)
                        if (includeTime)   then TextLine = TextLine & String.Format("{0,10}", oLogEntry.Time)
                        if (includeLevel)  then TextLine = TextLine & String.Format("  {0,-12}", "[" & WpfUtils.LogLevelConverter.Convert(oLogEntry.Level, GetType(String), Nothing, Nothing) & "]")
                        if (includeSource) then TextLine = TextLine & String.Format("{0,-30}", oLogEntry.Source)
                        TextLine = TextLine & oLogEntry.Message
                        
                        'Write message
                        oSW.WriteLine(TextLine)
                    Next
                    
                    'Close file
                    oSW.Flush()
                    oSW.Close()
                    InternalLogger.logInfo(String.Format(My.Resources.Resources.LogBox_SaveLog_FinalSuccessMessage, LogLevelName, LogFilePath))
                    
                Catch e As Exception
                    InternalLogger.logError(String.Format(My.Resources.Resources.LogBox_SaveLog_FinalErrorMessage, LogLevelName))
                End Try
            End If
            
            Return LogFilePath
        End Function
        
        ''' <summary> Resets all application settings to it's defaults. </summary>
        Public Sub resetSettings()
            Try
                My.MySettings.Default.Reset()
                'Me.ActionButtonsAlwaysVisible = My.Settings.Properties.Item("ActionButtonsAlwaysVisible").DefaultValue
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Shows a floating window with embedded <see cref="LoggingConsole.ConsoleView"/> if possible. </summary>
         ''' <param name="suppressErrorOnFail"> If "false", then an error should be logged, if the <see cref="LoggingConsole.ConsoleView"/> is already embedded into any other window. </param>
         ''' <remarks>
         ''' <para>
         ''' There can be only one instance of <see cref="LoggingConsole.ConsoleView"/>. 
         ''' That's why this method fails when the <see cref="LoggingConsole.ConsoleView"/> is already embedded into any other window.
         ''' </para>
         ''' <para>
         ''' This method is a proxy which in turn calls a delegate that is determined by the 
         ''' <see cref="LoggingConsole.LogBox.ShowFloatingConsoleViewAction"/> property. 
         ''' This way the host application can use it's own window logic.
         ''' </para>
         ''' </remarks>
        Public Sub showFloatingConsoleView(suppressErrorOnFail As Boolean)
            Try
                Me.ShowFloatingConsoleViewAction.Invoke(suppressErrorOnFail)
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Hides the floating window if it's shown. </summary>
         ''' <remarks> 
         ''' <para>
         ''' This method is a proxy which in turn calls a delegate that is determined by the 
         ''' <see cref="LoggingConsole.LogBox.HideFloatingConsoleViewAction"/> property. 
         ''' </para>
         ''' <para>
         ''' The embedded <see cref="LoggingConsole.ConsoleView"/> 
         ''' should be removed to allow it to re-embed to another window. 
         ''' </para>
         ''' </remarks>
        Public Sub hideFloatingConsoleView()
            Try
                Me.HideFloatingConsoleViewAction.Invoke()
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Shows the "About LoggingConsole" box. </summary>
        Public Sub showAboutBox()
            Try
                ' Shows About box if possible (and logs the about information at info level).
                Dim AppType      As String = String.Empty
                Dim OwnerWindow  As System.Windows.Window = Nothing
                
                ' Get application type
                If (System.Windows.Application.Current isNot Nothing) then
                    'WPF Application (standalone or XPAB)
                    If (System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName = "PresentationHost.exe") then
                        'WPF Browser Application
                        AppType = "XPAB"
                    Else
                        'WPF standalone Application
                        AppType = "WPF"
                    End If
                ElseIf (System.Windows.Forms.Application.OpenForms.Count > 0) then
                    'Windows Forms Application" (hopefully since there is at least one Windows Form)
                    AppType = "WinForm"
                End If
                
                ' Logs the about information at info level.
                InternalLogger.logInfo(My.Resources.Resources.About_ProgTitle & " - " & My.Resources.Resources.About_ProgDescription)
                InternalLogger.logInfo(My.Resources.Resources.About_Version   & " " & LogBox.Version)
                InternalLogger.logInfo(My.Resources.Resources.About_License   & " The MIT License (MIT)")
                InternalLogger.logInfo(My.Resources.Resources.About_Copyright & " " & LogBox.Copyright)
                
                ' Show About box if possible
                If (AppType = "XPAB") then
                    InternalLogger.logWarning(My.Resources.Resources.LogBox_AboutBox_NotAllowedInBrowser)
                Else
                    'Initialize and show AboutBox
                    Dim AboutBox As New AboutBox
                    AboutBox.DataContext = Me
                    
                    If (AppType = "WPF") then
                        OwnerWindow = System.Windows.Application.Current.MainWindow
                        If (Not OwnerWindow.IsInitialized) then
                            OwnerWindow = Nothing
                        End If
                    Else 
                        AboutBox.WindowStartupLocation = WindowStartupLocation.CenterScreen 
                    End If
                    
                    AboutBox.Owner = OwnerWindow
                    AboutBox.ShowDialog()
                End If
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
    #End Region
    
    #Region "ICommand Delegates"
        
        ''' <summary> Utilizes <see cref="LogBox.saveLog"/> to save the currently shown log to a file. </summary>
        Private Sub saveActiveLog()
            Try
                saveLog(Me.Console.ActiveView)
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
    #End Region
    
    #Region "Event Handlers"
        
        ''' <summary> Prevents the <see cref="LoggingConsole.ConsoleWindow"/> from closing. Instead it will be hidden to keep it alive. </summary>
         ''' <param name="sender"> <see cref="LoggingConsole.ConsoleWindow"/> </param>
         ''' <param name="e"> EventArgs </param>
        Private Sub OnFloatingWindowClosing(sender As System.Object , e As System.ComponentModel.CancelEventArgs)
            Try
                e.Cancel = True
                Me.hideFloatingConsoleView()
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> This method is called after an error is logged. </summary>
         ''' <param name="sender"> LogBox.Logger </param>
         ''' <param name="e"> Empty </param>
         ''' <remarks> If the "showConsoleOnError" property is "true", then this method activates the floating ConsoleView. </remarks>
        Private Sub OnNewErrorLogged(sender As System.Object , e As System.EventArgs)
            If (Me.showConsoleOnError) Then showFloatingConsoleView(suppressErrorOnFail:=true)
        End Sub
        
        ''' <summary> This method is called after <see cref="LoggingConsole.CultureResources.CurrentCulture" /> has changed. </summary>
         ''' <param name="sender"> <see cref="LoggingConsole.CultureResources" /> </param>
         ''' <param name="e"> Empty </param>
         ''' <remarks> 
         ''' Some ConsoleView elements are bound to LogBox properties.
         ''' In order to update these bindings with another language the datacontext of ConsoleView is reloaded.
         ''' </remarks>
        Private Sub OnCultureChanged(sender As System.Object , e As System.EventArgs)
            'If DisplayName is from resource then reset
            If (IsDisplayNameResource) Then MyBase.DisplayName = My.Resources.Resources.LogBox_DefaultDisplayName
            
            'Reset DataContext of ConsoleView, which in turn resets DataContexts in MessagesViews
            _LogBox.Console.ConsoleView.DataContext = Nothing
            _LogBox.Console.ConsoleView.DataContext = Me
            
            'Reset DataContext of FloatingWindow
            If (FloatingWindow IsNot Nothing) Then
                FloatingWindow.DataContext = Nothing
                FloatingWindow.DataContext = Me
            End If
        End Sub
        
    #End Region
    
    #Region "Private Routines"
        
        ''' <summary> Shows the built-in floating window with embedded <see cref="LoggingConsole.ConsoleView"/> if possible. </summary>
         ''' <param name="suppressErrorOnFail"> If "false", then an error is logged, if the <see cref="LoggingConsole.ConsoleView"/> is already embedded into any other window. </param>
         ''' <remarks> 
         ''' There can be only one instance of <see cref="LoggingConsole.ConsoleView"/>. 
         ''' That's why this method fails when the <see cref="LoggingConsole.ConsoleView"/> is already embedded into any other window.
         ''' </remarks>
        Private Sub showBuiltinFloatingConsoleView(suppressErrorOnFail As Boolean)
            If ((Me.Console.ConsoleView.Parent isNot Nothing) And ((FloatingWindow is Nothing) OrElse (FloatingWindow.LoggingConsolePanel.Content is Nothing))) Then
                'The ConsoleView is already embedded inside another window
                if (suppressErrorOnFail) then
                    InternalLogger.logDebug(My.Resources.Resources.LogBox_EmbedConsoleViewFailed)
                Else
                    InternalLogger.logError(My.Resources.Resources.LogBox_EmbedConsoleViewFailed)
                end if
            Else
                'Initialize FloatingWindow if needed
                If (FloatingWindow Is Nothing) Then
                    FloatingWindow = New ConsoleWindow
                    FloatingWindow.DataContext = Me
                    AddHandler FloatingWindow.Closing, AddressOf OnFloatingWindowClosing
                    
                    'Set WFP MainWindow as Owner, if possible
                    Try
                        OwnerWindow = Application.Current.MainWindow
                        If (Not OwnerWindow.IsInitialized) then
                            OwnerWindow = Nothing
                        End If
                    Catch e As Exception
                        'FloatingWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen 
                    Finally
                        FloatingWindow.Owner = OwnerWindow
                    End Try
                End If
                
                'Embed the ConsoleView
                FloatingWindow.LoggingConsolePanel.Content = Me.Console.ConsoleView
                
                If (Me.IsFloatingConsoleModal) then
                    FloatingWindow.ShowDialog()
                Else
                    FloatingWindow.Show()
                    FloatingWindow.Activate()
                End If
            End If
        End Sub
        
        ''' <summary> Hides the built-in floating window if it's shown. </summary>
         ''' <remarks> The embedded ConsoleView is removed to allow it to re-embed to another window. </remarks>
        Private Sub hideBuiltinFloatingConsoleView()
            If (FloatingWindow IsNot Nothing) Then
                FloatingWindow.Hide()
                FloatingWindow.LoggingConsolePanel.Content = Nothing
            End If
        End Sub
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
