﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Dieser Code wurde von einem Tool generiert.
'     Laufzeitversion:4.0.30319.42000
'
'     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
'     der Code erneut generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    '-Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    'Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    'mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    '''<summary>
    '''  Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Public Class Resources
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Rstyx.LoggingConsole.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        '''  Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Copyright: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property About_Copyright() As String
            Get
                Return ResourceManager.GetString("About_Copyright", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die License: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property About_License() As String
            Get
                Return ResourceManager.GetString("About_License", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die a .NET Logging Component ähnelt.
        '''</summary>
        Public Shared ReadOnly Property About_ProgDescription() As String
            Get
                Return ResourceManager.GetString("About_ProgDescription", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Logging Console ähnelt.
        '''</summary>
        Public Shared ReadOnly Property About_ProgTitle() As String
            Get
                Return ResourceManager.GetString("About_ProgTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Version: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property About_Version() As String
            Get
                Return ResourceManager.GetString("About_Version", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die New ConsoleView created. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Console_GetConsoleView_Created() As String
            Get
                Return ResourceManager.GetString("Console_GetConsoleView_Created", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Change dangerous value ({0:F1}) to {1:F1}. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Console_GetFontsize_ValueChanged() As String
            Get
                Return ResourceManager.GetString("Console_GetFontsize_ValueChanged", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Existing ConsoleView connected. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Console_SetConsoleView_Connected() As String
            Get
                Return ResourceManager.GetString("Console_SetConsoleView_Connected", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Background Colors ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_BackgroundColors() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_BackgroundColors", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Use different background colors ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_BackgroundColors_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_BackgroundColors_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Dark Color Schema ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_DarkColorSchema() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_DarkColorSchema", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Use dark background colors. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_DarkColorSchema_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_DarkColorSchema_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Text Colors ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_ForegroundColors() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_ForegroundColors", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Use different text colors ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_ForegroundColors_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_ForegroundColors_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Language: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_Language() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_Language", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Position of Options: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OptionpanePosition() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OptionpanePosition", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  White Option Labels ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OptionsForegroundWhite() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OptionsForegroundWhite", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die White Labels in Settings Area? ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OptionsForegroundWhite_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OptionsForegroundWhite_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Fixed Width Font ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OwnFontFamily() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OwnFontFamily", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die If disabled, the Font Family of the main window or system is used, otherwise a non-proportional font ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OwnFontFamily_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OwnFontFamily_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Font Size: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OwnFontSize() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OwnFontSize", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die If disabled, the Font Size of the main window or system is used. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_OwnFontSize_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_OwnFontSize_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Position of Tabs: ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_TabPosition() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_TabPosition", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Toolbar ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_Toolbar() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_Toolbar", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Show tasks toolbar ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Appearence_Toolbar_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Appearence_Toolbar_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Auto Column Width ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Behavior_AutoColWidth() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Behavior_AutoColWidth", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Set Column Width automatically to match the longest visible item ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Behavior_AutoColWidth_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Behavior_AutoColWidth_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Error Popup ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Behavior_ErrorPopup() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Behavior_ErrorPopup", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Should the Console popup when new Errors are logged? ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Behavior_ErrorPopup_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Behavior_ErrorPopup_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Appearence General ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_Appearence_General() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_Appearence_General", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Appearence Text Area ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_Appearence_Textarea() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_Appearence_Textarea", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Behavior ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_Behavior() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_Behavior", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Column Display ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_ColumnDisplay() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_ColumnDisplay", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Max. Log Length ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_LogLength() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_LogLength", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Log: Options and Tasks ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_OptionPane() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_OptionPane", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Tasks ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Header_Tasks() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Header_Tasks", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Lines ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_LogLength_Lines() As String
            Get
                Return ResourceManager.GetString("ConsoleView_LogLength_Lines", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die 0 = unlimited ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_LogLength_ToolTip() As String
            Get
                Return ResourceManager.GetString("ConsoleView_LogLength_ToolTip", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Date ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Tasks_Date() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Tasks_Date", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Level ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Tasks_Level() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Tasks_Level", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Line No ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Tasks_LineNo() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Tasks_LineNo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Source ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Tasks_Source() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Tasks_Source", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die  Time ähnelt.
        '''</summary>
        Public Shared ReadOnly Property ConsoleView_Tasks_Time() As String
            Get
                Return ResourceManager.GetString("ConsoleView_Tasks_Time", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Bottom ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_Dock_Bottom() As String
            Get
                Return ResourceManager.GetString("Enum_Dock_Bottom", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Left ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_Dock_Left() As String
            Get
                Return ResourceManager.GetString("Enum_Dock_Left", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Right ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_Dock_Right() As String
            Get
                Return ResourceManager.GetString("Enum_Dock_Right", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Top ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_Dock_Top() As String
            Get
                Return ResourceManager.GetString("Enum_Dock_Top", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Debug ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_LogLevel_Debug() As String
            Get
                Return ResourceManager.GetString("Enum_LogLevel_Debug", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Error ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_LogLevel_Error() As String
            Get
                Return ResourceManager.GetString("Enum_LogLevel_Error", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Info ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_LogLevel_Info() As String
            Get
                Return ResourceManager.GetString("Enum_LogLevel_Info", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Warning ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Enum_LogLevel_Warning() As String
            Get
                Return ResourceManager.GetString("Enum_LogLevel_Warning", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Error while handling an event ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Global_ErrorInEventHandler() As String
            Get
                Return ResourceManager.GetString("Global_ErrorInEventHandler", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Unexpected error (maybe while handling an event) ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Global_ErrorMaybeInEventHandler() As String
            Get
                Return ResourceManager.GetString("Global_ErrorMaybeInEventHandler", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Unexpected error. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Global_UnexpectedError() As String
            Get
                Return ResourceManager.GetString("Global_UnexpectedError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Unexpected error in {0}(). ähnelt.
        '''</summary>
        Public Shared ReadOnly Property Global_UnexpectedErrorIn() As String
            Get
                Return ResourceManager.GetString("Global_UnexpectedErrorIn", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die About LoggingConsole: Popup dialog is not allowed inside browser! ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_AboutBox_NotAllowedInBrowser() As String
            Get
                Return ResourceManager.GetString("LogBox_AboutBox_NotAllowedInBrowser", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Clear Log ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ClearLogCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_ClearLogCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Clear the whole Log ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ClearLogCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_ClearLogCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Logging Console ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_DefaultDisplayName() As String
            Get
                Return ResourceManager.GetString("LogBox_DefaultDisplayName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die The Console View is already embedded in another Window. =&gt; Display in floating Window not possible! ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_EmbedConsoleViewFailed() As String
            Get
                Return ResourceManager.GetString("LogBox_EmbedConsoleViewFailed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Close ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_HideFloatingConsoleViewCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_HideFloatingConsoleViewCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Close floating Console Window ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_HideFloatingConsoleViewCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_HideFloatingConsoleViewCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Log initialized. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_LogInitialized() As String
            Get
                Return ResourceManager.GetString("LogBox_LogInitialized", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Reset Options ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ResetSettingsCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_ResetSettingsCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Reset User Settings to Defaults. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ResetSettingsCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_ResetSettingsCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die =&gt; Failed creating File &apos;{0}&apos;. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLog_FinalErrorMessage() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLog_FinalErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die {0} Log saved in File &apos;{1}&apos; ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLog_FinalSuccessMessage() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLog_FinalSuccessMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Save Log ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Save the current Log View into a text File ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die SaveAs dialog aborted ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogDialog_Aborted() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogDialog_Aborted", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die .log ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogDialog_DefaultExt() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogDialog_DefaultExt", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die SaveAs dialog returned filename &apos;{0}&apos; ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogDialog_FilenameReturned() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogDialog_FilenameReturned", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Log Files ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogDialog_Logfiles() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogDialog_Logfiles", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die {0} Log save as ... ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_SaveLogDialog_Title() As String
            Get
                Return ResourceManager.GetString("LogBox_SaveLogDialog_Title", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die About ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ShowAboutBoxCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_ShowAboutBoxCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Info about Logging Console ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ShowAboutBoxCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_ShowAboutBoxCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Show Log ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ShowFloatingConsoleViewCommand_Caption() As String
            Get
                Return ResourceManager.GetString("LogBox_ShowFloatingConsoleViewCommand_Caption", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Show the Log in floating Window ähnelt.
        '''</summary>
        Public Shared ReadOnly Property LogBox_ShowFloatingConsoleViewCommand_Description() As String
            Get
                Return ResourceManager.GetString("LogBox_ShowFloatingConsoleViewCommand_Description", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die MessageStore initialized. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessageStore_Initialized() As String
            Get
                Return ResourceManager.GetString("MessageStore_Initialized", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die *** Log limited: Old length = {0:0} lines, New length = {1:0} lines. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessageStore_LimitLogResult() As String
            Get
                Return ResourceManager.GetString("MessageStore_LimitLogResult", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die All Log Entries removed. ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessageStore_LogCleared() As String
            Get
                Return ResourceManager.GetString("MessageStore_LogCleared", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Date ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_Date() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_Date", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Level ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_Level() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_Level", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Message ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_Message() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_Message", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Line No ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_SerialNo() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_SerialNo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Source ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_Source() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_Source", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die Time ähnelt.
        '''</summary>
        Public Shared ReadOnly Property MessagesView_gvch_Time() As String
            Get
                Return ResourceManager.GetString("MessagesView_gvch_Time", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
