
Imports System
Imports System.Diagnostics
Imports System.Windows.Data
Imports System.Globalization
Imports System.Collections.Generic


''' <summary> WPF related utilities - mainly Value Converters for Binding that are delivered by static properties. </summary>
Public Class WpfUtils
    
    #Region "Declarations"
        
        Private Shared _LogLevelConverter                As LogLevelValueConverter
        Private Shared _LogLevelTabIndexConverter        As LogLevelTabIndexValueConverter
        Private Shared _LogLevelBackgroundConverter      As LogLevelBackgroundValueConverter
        Private Shared _ColumnWidthConverter             As ColumnWidthValueConverter
        Private Shared _ColumnHeaderVisibilityConverter  As ColumnHeaderVisibilityValueConverter
        Private Shared _CheckboxConverter                As CheckboxValueConverter
        Private Shared _DockEnumConverter                As DockEnumValueConverter
        Private Shared _ExpandDirectionConverter         As ExpandDirectionValueConverter
        Private Shared _MessagesListViewSourceConverter  As MessagesListViewSourceValueConverter
        Private Shared _CultureInfoConverter             As CultureInfoValueConverter
        
        Private Shared ReadOnly SyncHandle               As New Object
    #End Region
    
    #Region "Constructor"
        
        Private Sub New
            'Hides the Constructor
        End Sub
        
    #End Region
    
    #Region "Shared Properties"
        
        ''' <summary> Returns an instance of WpfUtils.LogLevelValueConverter </summary>
         ''' <returns> WpfUtils.LogLevelValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts LogLevelEnum to LogLevel Description String. </remarks>
        Public Shared ReadOnly Property LogLevelConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_LogLevelConverter Is Nothing) Then _LogLevelConverter = New LogLevelValueConverter()
                    Return _LogLevelConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.LogLevelTabIndexValueConverter </summary>
         ''' <returns> WpfUtils.LogLevelTabIndexValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts LogLevelEnum to TabIndex - and vice versa. </remarks>
        Public Shared ReadOnly Property LogLevelTabIndexConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_LogLevelTabIndexConverter Is Nothing) Then _LogLevelTabIndexConverter = New LogLevelTabIndexValueConverter()
                    Return _LogLevelTabIndexConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.LogLevelBackgroundValueConverter </summary>
         ''' <returns> WpfUtils.LogLevelBackgroundValueConverter (WPF Binding IMultiValueConverter) </returns>
         ''' <remarks> 
         ''' The returned Converter converts LogLevelEnum and Boolean property to ListView Background Brush. <br></br>
         ''' This allows the ListView to set a background color depending on LogLevel and the "useBackgroundColors" setting.
         ''' </remarks>
        Public Shared ReadOnly Property LogLevelBackgroundConverter() As IMultiValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_LogLevelBackgroundConverter Is Nothing) Then _LogLevelBackgroundConverter = New LogLevelBackgroundValueConverter()
                    Return _LogLevelBackgroundConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.ColumnWidthValueConverter </summary>
         ''' <returns> WpfUtils.ColumnWidthValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> 
         ''' The returned Converter converts between Boolean property and GridViewColumn visibility - and vice versa. <br />
         ''' In fact, the "visibility" is the GridViewColumn's width (0.0 = not visible).
         ''' </remarks>
        Public Shared ReadOnly Property ColumnWidthConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_ColumnWidthConverter Is Nothing) Then _ColumnWidthConverter = New ColumnWidthValueConverter()
                    Return _ColumnWidthConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.ColumnHeaderVisibilityValueConverter </summary>
         ''' <returns> WpfUtils.ColumnHeaderVisibilityValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts between Boolean property and GridViewColumnHeader visibility - and vice versa. </remarks>
        Public Shared ReadOnly Property ColumnHeaderVisibilityConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_ColumnHeaderVisibilityConverter Is Nothing) Then _ColumnHeaderVisibilityConverter = New ColumnHeaderVisibilityValueConverter()
                    Return _ColumnHeaderVisibilityConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.CheckboxValueConverter </summary>
         ''' <returns> WpfUtils.CheckboxValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts between Boolean property and Checkbox state - and vice versa. </remarks>
        Public Shared ReadOnly Property CheckboxConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_CheckboxConverter Is Nothing) Then _CheckboxConverter = New CheckboxValueConverter()
                    Return _CheckboxConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.DockEnumValueConverter </summary>
         ''' <returns> WpfUtils.DockEnumValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> 
         ''' The returned Converter converts between Dock Enum and it's Description String - and vice versa. <br />
         ''' Support for single variables and arrays.
         ''' </remarks>
        Public Shared ReadOnly Property DockEnumConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_DockEnumConverter Is Nothing) Then _DockEnumConverter = New DockEnumValueConverter()
                    Return _DockEnumConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.ExpandDirectionValueConverter </summary>
         ''' <returns> WpfUtils.ExpandDirectionValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts a Dock Enum to an ExpandDirection Enum. </remarks>
        Public Shared ReadOnly Property ExpandDirectionConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_ExpandDirectionConverter Is Nothing) Then _ExpandDirectionConverter = New ExpandDirectionValueConverter()
                    Return _ExpandDirectionConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.MessagesListViewSourceValueConverter </summary>
         ''' <returns> WpfUtils.MessagesListViewSourceValueConverter (WPF Binding IMultiValueConverter) </returns>
         ''' <remarks> 
         ''' The returned Converter gets a <see cref="LoggingConsole.MessageStore"/> and a <see cref="LoggingConsole.LogLevelEnum"/>. 
         ''' It returns the Messages Collection for the given LogLevel (<see cref="LoggingConsole.MessageStore.Messages"/>[LogLevel]).
         ''' </remarks>
        Public Shared ReadOnly Property MessagesListViewSourceConverter() As IMultiValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_MessagesListViewSourceConverter Is Nothing) Then _MessagesListViewSourceConverter = New MessagesListViewSourceValueConverter()
                    Return _MessagesListViewSourceConverter
                End SyncLock
            End Get
        End Property
        
        ''' <summary> Returns an instance of WpfUtils.CultureInfoValueConverter </summary>
         ''' <returns> WpfUtils.CultureInfoValueConverter (WPF Binding IValueConverter) </returns>
         ''' <remarks> The returned Converter converts a CultureInfo to it's NativeName String - and vice versa. </remarks>
        Public Shared ReadOnly Property CultureInfoConverter() As IValueConverter
            Get
                SyncLock (SyncHandle)
                    If (_CultureInfoConverter Is Nothing) Then _CultureInfoConverter = New CultureInfoValueConverter()
                    Return _CultureInfoConverter
                End SyncLock
            End Get
        End Property
        
    #End Region
    
    #Region "Shared Methods"
        
        ''' <summary> creates a BitmapImage from a file path </summary>
         ''' <param name="path">File path, i.e. for a project resource: "/ProjectName;component/Resources/save.png"</param>
         ''' <returns> The BitmapImage generated from the given file. </returns>
        Public Shared Function getImageFromPath(path As String) As System.Windows.Media.Imaging.BitmapImage
            Dim bi As New System.Windows.Media.Imaging.BitmapImage()
            SyncLock (SyncHandle)
                bi.BeginInit()
                bi.UriSource = New Uri(path, UriKind.RelativeOrAbsolute)
                bi.CacheOption = Windows.Media.Imaging.BitmapCacheOption.None
                bi.EndInit()
                Return bi
            End SyncLock
        End Function
        
    #End Region
    
    #Region "Value Converters for WPF Binding"
        
        ''' <summary> Converts LogLevelEnum to LogLevel Display String </summary>
        <ValueConversion(GetType(LogLevelEnum), GetType(String))>
        Private Class LogLevelValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            ''' <summary>
            ''' Returns the conversion dictionary.
            ''' It's newly created everytime in order to get the correct language resource strings always.
            ''' </summary>
            Private Function getConvertDictionary As Dictionary(Of LogLevelEnum, String)
                Dim ConvertDictionary As New Dictionary(Of LogLevelEnum, String)
                ConvertDictionary.add(LogLevelEnum.Debug,   My.Resources.Resources.Enum_LogLevel_Debug)
                ConvertDictionary.add(LogLevelEnum.Info,    My.Resources.Resources.Enum_LogLevel_Info)
                ConvertDictionary.add(LogLevelEnum.Warning, My.Resources.Resources.Enum_LogLevel_Warning)
                ConvertDictionary.add(LogLevelEnum.Error,   My.Resources.Resources.Enum_LogLevel_Error)
                Return ConvertDictionary
            End Function
            
            ''' <summary> Converts a LogLevelEnum value to LogLevel Display String. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The display string. On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                Try
                    Return getConvertDictionary().Item(value)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Not Implemented. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The input value itself. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                System.Diagnostics.Trace.WriteLine("LogLevelValueConverter[ConvertBack]: This is Not Implemented => return input value!")
                Return value
            End Function
        End Class
        
        ''' <summary> Converts LogLevelEnum to TabIndex - and vice versa </summary>
        <ValueConversion(GetType(LogLevelEnum), GetType(Integer))>
        Private Class LogLevelTabIndexValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                'LogLevelEnum => TabIndex
                Try
                    Return CType(value, Integer)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                Try
                    Dim LogLevel As Integer = 0
                    
                    If ((value > 0) and (value < 4)) Then
                        LogLevel = value
                    End if
                    
                    Return LogLevel
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
        End Class
        
        ''' <summary> Converts LogLevelEnum and Boolean property to ListView Background Brush. </summary>
         ''' <remarks> This allows the ListView to set a background color depending on LogLevel and the "useBackgroundColors" setting. </remarks>
        <ValueConversion(GetType(LogLevelEnum), GetType(System.Windows.Media.SolidColorBrush))>
        Private Class LogLevelBackgroundValueConverter
            Implements IMultiValueConverter
            
            Private ConvertDict        As New Dictionary(Of LogLevelEnum, System.Windows.Media.SolidColorBrush)
            Private SystemWindowColor  As New System.Windows.Media.SolidColorBrush(System.Windows.SystemColors.WindowColor)
            
            Public Sub New()
                ConvertDict.add(LogLevelEnum.Debug,   new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGray))
                'ConvertDict.add(LogLevelEnum.Info,    new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Honeydew))
                ConvertDict.add(LogLevelEnum.Info,    SystemWindowColor)
                ConvertDict.add(LogLevelEnum.Warning, new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.PeachPuff))
                ConvertDict.add(LogLevelEnum.Error,   new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Pink))
            End Sub
            
            ''' <summary> Converts a LogLevelEnum value to a SolidColorBrush. </summary>
             ''' <param name="values">     LogLevelEnum and useBackgroundColors. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The display string. On error the input value itself is returned. </returns>
            Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
                Try
                    Dim returnColor As System.Windows.Media.SolidColorBrush = SystemWindowColor
                    If ((values(1).GetType.Name = "Boolean")) Then
                        If (values(1)) Then returnColor = ConvertDict(values(0))
                    End if
                    Return returnColor
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return values
                End Try
            End Function
            
            ''' <summary> Not Implemented. </summary>
             ''' <param name="value">       Input value. </param>
             ''' <param name="targetTypes"> System.Type to convert to. </param>
             ''' <param name="parameter">   Ignored. </param>
             ''' <param name="culture">     Ignored. </param>
             ''' <returns>                  The input value itself. </returns>
            Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
                System.Diagnostics.Trace.WriteLine("LogLevelBackgroundValueConverter[ConvertBack]: This is Not Implemented => return input value!")
                Return value
            End Function
        End Class
        
        ''' <summary> Converts between Boolean property and GridViewColumn visibility (Double) - and vice versa. </summary>
         ''' <remarks> In fact, the "visibility" is the GridViewColumn's width (0.0 = not visible). </remarks>
        <ValueConversion(GetType(Boolean), GetType(Double))>
        Private Class ColumnWidthValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            ''' <summary> Converts a Boolean value to GridViewColumn visibility (Double). </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 GridViewColumn visibility (Double). On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                'Boolean => Width
                Try
                    Return IIf(cBool(value), Double.NaN, 0.0)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Converts a GridViewColumn visibility (Double) to a Boolean value. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 Boolean value. On error the input value itself is returned. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                'Width => Boolean
                Try
                    Return IIf(CDbl(value) = 0.0, False, True)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
        End Class
        
        ''' <summary> Converts between Boolean property and GridViewColumnHeader visibility - and vice versa. </summary>
        <ValueConversion(GetType(Boolean), GetType(System.Windows.Visibility))>
        Private Class ColumnHeaderVisibilityValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            ''' <summary> Converts a Boolean value to GridViewColumnHeader visibility. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 GridViewColumnHeader visibility. On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                'Boolean => System.Windows.Visibility
                Try
                    Return IIf(cBool(value), System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Converts a GridViewColumnHeader visibility to a Boolean value. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 GridViewColumnHeader visibility. On error the input value itself is returned. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                'System.Windows.Visibility => Boolean
                Try
                    Return IIf((value = System.Windows.Visibility.Visible), True, False)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
        End Class
        
        ''' <summary> Converts between Boolean property and Checkbox state - and vice versa </summary>
        <ValueConversion(GetType(Boolean), GetType(Nullable(Of Boolean)))>
        Private Class CheckboxValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            ''' <summary> Converts a Boolean value to a Nullable(Of Boolean). </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 Nullable(Of Boolean). On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                'Boolean => Checkbox state
                Try
                    Return New Nullable(Of Boolean)(CBool(value))
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Converts a Nullable(Of Boolean) value to a Boolean. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 Boolean. On error the input value itself is returned. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                Try
                    'Checkbox state => Boolean
                    'dim OnOff As Boolean
                    'GetType(value)
                    'Return value.GetValueOrDefault(true)
                    Return value
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
        End Class
        
        ''' <summary> Converts between Dock Enum and it's Display String - and vice versa. </summary>
         ''' <remarks> Support for single variables and arrays. </remarks>
        <ValueConversion(GetType(System.Windows.Controls.Dock), GetType(String))>
        Private Class DockEnumValueConverter
            Implements IValueConverter
            
            Public Sub New()
            End Sub
            
            ''' <summary>
            ''' Returns the conversion dictionary.
            ''' It's newly created everytime in order to get the correct language resource strings always.
            ''' </summary>
            Private Function getConvertDictionary As Dictionary(Of System.Windows.Controls.Dock, String)
                Dim ConvertDictionary As New Dictionary(Of System.Windows.Controls.Dock, String)
                ConvertDictionary.add(System.Windows.Controls.Dock.Top,    My.Resources.Resources.Enum_Dock_Top)
                ConvertDictionary.add(System.Windows.Controls.Dock.Left,   My.Resources.Resources.Enum_Dock_Left)
                ConvertDictionary.add(System.Windows.Controls.Dock.Right,  My.Resources.Resources.Enum_Dock_Right)
                ConvertDictionary.add(System.Windows.Controls.Dock.Bottom, My.Resources.Resources.Enum_Dock_Bottom)
                Return ConvertDictionary
            End Function
            
            ''' <summary>
            ''' Returns the back conversion dictionary.
            ''' It's newly created everytime in order to get the correct language resource strings always.
            ''' </summary>
            Private Function getConvertBackDictionary As Dictionary(Of String, System.Windows.Controls.Dock)
                Dim ConvertBackDictionary = New Dictionary(Of String, System.Windows.Controls.Dock)
                ConvertBackDictionary.add(My.Resources.Resources.Enum_Dock_Top   , System.Windows.Controls.Dock.Top)
                ConvertBackDictionary.add(My.Resources.Resources.Enum_Dock_Left  , System.Windows.Controls.Dock.Left)
                ConvertBackDictionary.add(My.Resources.Resources.Enum_Dock_Right , System.Windows.Controls.Dock.Right)
                ConvertBackDictionary.add(My.Resources.Resources.Enum_Dock_Bottom, System.Windows.Controls.Dock.Bottom)
                Return ConvertBackDictionary
            End Function
            
            ''' <summary> Converts a Dock Enum to it's Display String. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The display string. On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                Try
                    'Supports both directions and recognizes the desired direction
                    Dim ret        As Object = Nothing
                    Dim valueType  As Type
                    
                    If (value isNot Nothing) Then
                        
                        valueType = value.GetType()
                        
                        If (Left(valueType.Name, 4) = "Dock") Then
                            'Dock => String
                            If (valueType.IsArray) Then
                                'Dim ub As Long = CType(value, Array).length
                                Dim ub As Long = UBound(value)
                                Dim retArray(ub) As String
                                For i as long = 0 to ub
                                    retArray(i) = getConvertDictionary().item(value(i))
                                Next
                                ret = retArray
                            Else
                                ret = getConvertDictionary().item(value)
                            End if
                        Else
                            'String => Dock
                            If (value isNot Nothing) Then
                                If (valueType.IsArray) Then
                                    Dim ub As Long = UBound(value)
                                    Dim retArray(ub) As System.Windows.Controls.Dock
                                    For i as long = 0 to ub
                                        retArray(i) = getConvertBackDictionary().item(value(i))
                                    Next
                                    ret = retArray
                                Else
                                    ret = getConvertBackDictionary().item(value)
                                End if
                            End if
                        End if
                    End if
                    Return ret
                    
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Converts a String to a Dock Enum. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The Dock Enum string. On error the input value itself is returned. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                Return Convert(value, targetType, parameter, culture)
            End Function
        End Class
        
        ''' <summary> Converts a Dock Enum to an ExpandDirection Enum. </summary>
        <ValueConversion(GetType(System.Windows.Controls.Dock), GetType(System.Windows.Controls.ExpandDirection))>
        Private Class ExpandDirectionValueConverter
            Implements IValueConverter
            
            Private ConvertDict  As Dictionary(Of System.Windows.Controls.Dock, System.Windows.Controls.ExpandDirection)
            
            Public Sub New()
                ConvertDict = New Dictionary(Of System.Windows.Controls.Dock, System.Windows.Controls.ExpandDirection)
                ConvertDict.add(System.Windows.Controls.Dock.Left,  System.Windows.Controls.ExpandDirection.Right)
                ConvertDict.add(System.Windows.Controls.Dock.Right, System.Windows.Controls.ExpandDirection.Left)
            End Sub
            
            ''' <summary> Converts a Dock Enum to an ExpandDirection Enum. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 ExpandDirection Enum. On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                Try
                    Return ConvertDict.Item(value)
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Not Implemented. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The input value itself. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                System.Diagnostics.Trace.WriteLine("ExpandDirectionValueConverter[ConvertBack]: This is Not Implemented => return input value!")
                Return value
            End Function
        End Class
        
        ''' <summary> Returns the Messages Collection for the given LogLevel (<see cref="LoggingConsole.MessageStore.Messages"/>[LogLevel]) </summary>
         ''' <remarks> The input types are: <see cref="MessageStore"/> and <see cref="LogLevelEnum"/>. </remarks>
        <ValueConversion(GetType(MessageStore), GetType(LogLevelEnum))>
        Private Class MessagesListViewSourceValueConverter
            Implements IMultiValueConverter
            
            ''' <summary> Converts a LogLevelEnum value to a SolidColorBrush. </summary>
             ''' <param name="values">     MessageStore and LogLevelEnum. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 Messages Collection for the given LogLevel. On error the input value itself is returned. </returns>
            Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
                Try
                    'parameter 0 = MessageStore
                    'parameter 1 = LogLevelEnum
                    dim Messages As System.Collections.ObjectModel.ObservableCollection(Of LogEntry) = Nothing
                    
                    If ((values(0).GetType.Name = "MessageStore") and (values(1).GetType.Name = "LogLevelEnum")) Then
                        Messages = CType(values(0), MessageStore).Messages(values(1))
                    End if
                    
                    Return Messages
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return values
                End Try
            End Function
            
            ''' <summary> Not Implemented. </summary>
             ''' <param name="value">       Input value. </param>
             ''' <param name="targetTypes"> System.Type to convert to. </param>
             ''' <param name="parameter">   Ignored. </param>
             ''' <param name="culture">     Ignored. </param>
             ''' <returns>                  The input value itself. </returns>
            Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
                System.Diagnostics.Trace.WriteLine("MessagesListViewSourceValueConverter[ConvertBack]: This is Not Implemented => return input value!")
                Return value
            End Function
        End Class
        
        
        ''' <summary> Converts between CultureInfo and it's NativeName String - and vice versa. </summary>
         ''' <remarks> Support for single variables and List(Of CultureInfo). </remarks>
        <ValueConversion(GetType(System.Globalization.CultureInfo), GetType(String))>
        Private Class CultureInfoValueConverter
            Implements IValueConverter
            
            Private NativeName2CultureInfo  As New Dictionary(Of String, CultureInfo)
            
            Public Sub New()
            End Sub
            
            Private Function CultureInfo2Name(Culture As System.Globalization.CultureInfo) As String
                Dim NativeName  As String
                
                If (Culture.Equals(System.Globalization.CultureInfo.InvariantCulture)) Then
                    NativeName = "(default)"
                Else
                    NativeName = Culture.NativeName
                End If
                
                'Remember mapping of NativeName and CultureInfo
                if (not NativeName2CultureInfo.ContainsKey(NativeName)) Then
                    NativeName2CultureInfo.Add(NativeName, Culture)
                end if
                
                Return NativeName
            End Function
            
            ''' <summary> Converts a CultureInfo to it's NativeName String. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The CultureInfo's NativeName. On error the input value itself is returned. </returns>
            Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
                Try
                    'CultureInfo => String
                    Dim ret As Object = Nothing
                    
                    If (value isNot Nothing) Then
                        Dim valueType   As Type = value.GetType()
                        
                        If (valueType.IsGenericType) Then
                            Dim retList As New List(Of String)
                            For each ci as CultureInfo in value
                                retList.add(CultureInfo2Name(ci))
                            Next
                            Ret = retList
                        Else
                            ret = CultureInfo2Name(value)
                        End if
                    End if
                    Return ret
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
            
            ''' <summary> Converts a CultureInfo's NativeName String to the CultureInfo. </summary>
             ''' <param name="value">      Input value. </param>
             ''' <param name="targetType"> System.Type to convert to. </param>
             ''' <param name="parameter">  Ignored. </param>
             ''' <param name="culture">    Ignored. </param>
             ''' <returns>                 The CultureInfo. On error the input value itself is returned. </returns>
            Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
                Try
                    'String => CultureInfo
                    Dim ret As Object = Nothing
                    
                    If (not NativeName2CultureInfo.ContainsKey(value)) Then
                        Debug.WriteLine(String.Format("CultureInfoValueConverter[ConvertBack]: can't get CultureInfo for NativeName", value))
                    Else
                        ret = NativeName2CultureInfo(value)
                    End If
                    
                    Return ret
                Catch ex As System.Exception
                    System.Diagnostics.Trace.WriteLine(ex)
                    Return value
                End Try
            End Function
        End Class
        
    #End Region
    
End Class

'Origin: http://loosexaml.wordpress.com/2009/04/09/reference-to-self-in-xaml/
'Public Class RootExtension
 '   Inherits System.Windows.Markup.MarkupExtension
 '   
 '   Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
 '       Dim self             As Object = Nothing
 '       Dim selfType         As Type   = serviceProvider.[GetType]()
 '       Dim contextFieldInfo As System.Reflection.FieldInfo = selfType.GetField("_context", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
 '       
 '       If (contextFieldInfo IsNot Nothing) Then
 '           Dim context              As Object = contextFieldInfo.GetValue(serviceProvider)
 '           Dim contextType          As Type   = context.[GetType]()
 '           Dim rootElementFieldInfo As System.Reflection.FieldInfo = contextType.GetField("_rootElement", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
 '           
 '           If (rootElementFieldInfo IsNot Nothing) Then
 '               self = rootElementFieldInfo.GetValue(context)
 '           End If
 '       End If
 '       
 '       'This way requires .NET 4:
 '    'Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
 '    '  Return DirectCast(serviceProvider, System.Xaml.IRootObjectProvider).RootObject
 '    'End Function
 '       
 '       Return self
 '   End Function
'End Class


' for jEdit:  :collapseFolds=3::tabSize=4::indentSize=4:
