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


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Public NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "Automatische My.Settings-Speicherfunktion"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowColumnTime() As Boolean
            Get
                Return CType(Me("ShowColumnTime"),Boolean)
            End Get
            Set
                Me("ShowColumnTime") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowColumnDate() As Boolean
            Get
                Return CType(Me("ShowColumnDate"),Boolean)
            End Get
            Set
                Me("ShowColumnDate") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowColumnLevel() As Boolean
            Get
                Return CType(Me("ShowColumnLevel"),Boolean)
            End Get
            Set
                Me("ShowColumnLevel") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowColumnSource() As Boolean
            Get
                Return CType(Me("ShowColumnSource"),Boolean)
            End Get
            Set
                Me("ShowColumnSource") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property MaxLogLength() As UInteger
            Get
                Return CType(Me("MaxLogLength"),UInteger)
            End Get
            Set
                Me("MaxLogLength") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Top")>  _
        Public Property TabStripPlacement() As Global.System.Windows.Controls.Dock
            Get
                Return CType(Me("TabStripPlacement"),Global.System.Windows.Controls.Dock)
            End Get
            Set
                Me("TabStripPlacement") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ActionButtonsAlwaysVisible() As Boolean
            Get
                Return CType(Me("ActionButtonsAlwaysVisible"),Boolean)
            End Get
            Set
                Me("ActionButtonsAlwaysVisible") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Left")>  _
        Public Property ExpanderHeaderPlacement() As Global.System.Windows.Controls.Dock
            Get
                Return CType(Me("ExpanderHeaderPlacement"),Global.System.Windows.Controls.Dock)
            End Get
            Set
                Me("ExpanderHeaderPlacement") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseBackgroundColors() As Boolean
            Get
                Return CType(Me("UseBackgroundColors"),Boolean)
            End Get
            Set
                Me("UseBackgroundColors") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("300")>  _
        Public Property BuiltinConsoleWindowHeight() As Double
            Get
                Return CType(Me("BuiltinConsoleWindowHeight"),Double)
            End Get
            Set
                Me("BuiltinConsoleWindowHeight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("500")>  _
        Public Property BuiltinConsoleWindowWidth() As Double
            Get
                Return CType(Me("BuiltinConsoleWindowWidth"),Double)
            End Get
            Set
                Me("BuiltinConsoleWindowWidth") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("150")>  _
        Public Property BuiltinConsoleWindowTop() As Double
            Get
                Return CType(Me("BuiltinConsoleWindowTop"),Double)
            End Get
            Set
                Me("BuiltinConsoleWindowTop") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("200")>  _
        Public Property BuiltinConsoleWindowLeft() As Double
            Get
                Return CType(Me("BuiltinConsoleWindowLeft"),Double)
            End Get
            Set
                Me("BuiltinConsoleWindowLeft") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property ShowColumnLineNo() As Boolean
            Get
                Return CType(Me("ShowColumnLineNo"),Boolean)
            End Get
            Set
                Me("ShowColumnLineNo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property AutoSizeColumns() As Boolean
            Get
                Return CType(Me("AutoSizeColumns"),Boolean)
            End Get
            Set
                Me("AutoSizeColumns") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShowConsoleOnError() As Boolean
            Get
                Return CType(Me("ShowConsoleOnError"),Boolean)
            End Get
            Set
                Me("ShowConsoleOnError") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UseOwnFontSize() As Boolean
            Get
                Return CType(Me("UseOwnFontSize"),Boolean)
            End Get
            Set
                Me("UseOwnFontSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("11")>  _
        Public Property FontSize() As Double
            Get
                Return CType(Me("FontSize"),Double)
            End Get
            Set
                Me("FontSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseOwnFontFamily() As Boolean
            Get
                Return CType(Me("UseOwnFontFamily"),Boolean)
            End Get
            Set
                Me("UseOwnFontFamily") = value
            End Set
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Rstyx.LoggingConsole.UIResources")>  _
        Public ReadOnly Property UIResources_LoggerName() As String
            Get
                Return CType(Me("UIResources_LoggerName"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("/Rstyx.LoggingConsole;component/source/Resources/IconResources.xaml")>  _
        Public ReadOnly Property UIResources_IconResourcesUri() As String
            Get
                Return CType(Me("UIResources_IconResourcesUri"),String)
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseForegroundColors() As Boolean
            Get
                Return CType(Me("UseForegroundColors"),Boolean)
            End Get
            Set
                Me("UseForegroundColors") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UseDarkColorSchema() As Boolean
            Get
                Return CType(Me("UseDarkColorSchema"),Boolean)
            End Get
            Set
                Me("UseDarkColorSchema") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property OptionsForegroundWhite() As Boolean
            Get
                Return CType(Me("OptionsForegroundWhite"),Boolean)
            End Get
            Set
                Me("OptionsForegroundWhite") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Rstyx.LoggingConsole.My.MySettings
            Get
                Return Global.Rstyx.LoggingConsole.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
