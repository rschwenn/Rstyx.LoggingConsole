
Imports System
Imports System.Windows.Controls

Partial Public Class MessagesView
    Inherits System.Windows.Controls.UserControl
    
    #Region "Private Fields"
        
        Private Shared InternalLogger  As Logger = LogBox.getLogger("LogBox.MessagesView")
        
        Private _LogLevel              As LogLevelEnum = LogLevelEnum.Info
        
        Private DeferredScrollAction   As DeferredAction
        Private ReadOnly ScrollDelay   As TimeSpan = TimeSpan.FromMilliseconds(100)
        
        Private DeferredAdjustAction   As DeferredAction
        Private ReadOnly AdjustDelay   As TimeSpan = TimeSpan.FromMilliseconds(100)
        
        'Private LastSelectedIndex      As Integer = 0
        Private CollectionHasChanged   As Boolean = True
        Private CollectionChangedHandlerAdded   As Boolean = False
        
    #End Region
    
    #Region "Initializing and Finalizing"
        
        ''' <summary> Creates a new instance. </summary>
         ''' <param name="LogLevelToShow"> The <see cref="LoggingConsole.LogLevelEnum"/> which this MessagesView is intended to show. </param>
        Friend Sub New(byVal LogLevelToShow As LogLevelEnum)
            Me.New()
            _LogLevel = LogLevelToShow
        End Sub
        
        ''' <summary> Creates a new instance. </summary>
        Friend Sub New()
            InitializeComponent()
        End Sub
        
        ''' <summary> Register event handlers. </summary>
        Private Sub OnUserControlLoaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
            Try
                'Listen for changes of Messages ObservableCollection (adding/deleting log messages)
                If (Not CollectionChangedHandlerAdded) Then
                    AddHandler CType(CType(Me.MessagesListView.ItemsSource, System.Windows.Data.CollectionView).SourceCollection, System.Collections.ObjectModel.ObservableCollection(Of LogEntry)).CollectionChanged, AddressOf OnCollectionChanged
                    CollectionChangedHandlerAdded = True
                End If
                
                DeferredScrollAction = New DeferredAction(AddressOf scrollToEndOfLog, Me.Dispatcher)
                DeferredAdjustAction = New DeferredAction(AddressOf adjustGridViewColumnWidths, Me.Dispatcher)
                
                If (CollectionHasChanged) Then
                    scrollToEndOfLog()
                Else
                    ' Select the last selected line. *** doesn't work because LastSelectedIndex is always = -1.
                    'If (MessagesListView.Items.Count >= LastSelectedIndex) Then
                    '    MessagesListView.SelectedIndex = LastSelectedIndex
                    'End If
                End If
                
                'Bind the appropriate Messages collection to this View
                'CType(Me.FindResource("MessagesListViewSource"), System.Windows.Data.CollectionViewSource).Source = CType(Me.DataContext, LogBox).MessageStore.Messages(Me.LogLevel)
                
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Replacement for Dispose(). </summary>
        Private Sub OnUserControlUnLoaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
            Try
                'If (Not CollectionChangedHandlerAdded) Then
                '    RemoveHandler CType(CType(Me.MessagesListView.ItemsSource, System.Windows.Data.CollectionView).SourceCollection, System.Collections.ObjectModel.ObservableCollection(Of LogEntry)).CollectionChanged, AddressOf OnCollectionChanged
                '    CollectionChangedHandlerAdded = False
                'End If
                
                DeferredScrollAction.Dispose()
                DeferredAdjustAction.Dispose()
                DeferredScrollAction = Nothing
                DeferredAdjustAction = Nothing
                
                'LastSelectedIndex = MessagesListView.SelectedIndex
                
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
    #End Region
    
    #Region "Properties"
        
        ''' <summary> Returns the <see cref="LoggingConsole.LogLevelEnum"/> which this MessagesView is intended to show. </summary>
         ''' <returns> the <see cref="LoggingConsole.LogLevelEnum"/> </returns>
         ''' <remarks> This is for internal use. </remarks>
        Public ReadOnly Property LogLevel() As LogLevelEnum
            Get
                LogLevel = _LogLevel
            End Get
        End Property
        
    #End Region
    
    #Region "Event Handlers"
        
        ''' <summary> Schedule auto-adjustment of width of all visible GridViewColumns. </summary>
         ''' <remarks> In order to get this event raised the binding property "NotifyOnTargetUpdated=True" has to be set at least for one GridViewColumn. </remarks>
        Private Sub OnListViewUpdated(sender As System.Object, e As System.Windows.Data.DataTransferEventArgs) Handles MessagesListView.TargetUpdated
            Try
                If (DeferredAdjustAction IsNot Nothing) Then
                    DeferredAdjustAction.Defer(AdjustDelay)
                End If
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> Schedule a scroll to last log entry after a log entry has been added. </summary>
         ''' <remarks> This method is only called if the MessagesView is currently loaded (resp. visible). </remarks>
        Private Sub OnCollectionChanged(sender As Object, e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
            Try
                If (e.Action = System.Collections.Specialized.NotifyCollectionChangedAction.Add) then
                    CollectionHasChanged = True
                    If (DeferredScrollAction IsNot Nothing) Then
                        DeferredScrollAction.Defer(ScrollDelay)
                    End If
                End If
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        ''' <summary> The visibility of a GridViewColumnHeader has been updated by the binding engine. </summary>
         ''' <remarks> This is registered via XAML! But the event only raises when the Binding property "NotifyOnTargetUpdated=True" is set for the "visibility" property. </remarks>
        Private Sub OnGridViewColumnHeaderVisibilityUpdated(sender As Object, e As System.Windows.Data.DataTransferEventArgs)
            Try
                If (e.Property.Name = "Visibility") Then
                    Dim header As System.Windows.Controls.GridViewColumnHeader = TryCast(sender, System.Windows.Controls.GridViewColumnHeader)
                    If (header IsNot Nothing) Then syncColumnVisibilityWithHeader(header)
                End If
            Catch ex As System.Exception
                InternalLogger.logError(ex, String.Format(My.Resources.Resources.Global_UnexpectedErrorIn, System.Reflection.MethodBase.GetCurrentMethod().Name))
            End Try
        End Sub
        
        'Private Sub LogLevelFilter(sender As System.Object, e As System.Windows.Data.FilterEventArgs)
         '   'Filter out LogEntrys of Level that should not be shown.
         '   'Connected via XAML: "Filter" in CollectionViewSource 
         '   dim oLogEntry as LogEntry = CType(e.Item, LogEntry)
         '   e.Accepted = (not (oLogEntry.Level < _LogLevel))
        'End Sub
        
    #End Region
    
    #Region "Private methods"
        
        ''' <summary> Applies the visibility of a given GridViewColumnHeader to it's GridViewColumn. </summary>
         ''' <remarks> 
         ''' <para>
         ''' The visibility of column headers is bound to console properties.
         ''' </para>
         ''' <para>
         ''' When a column is made visible, it's done by setting it's width to the last value.
         ''' </para>
         ''' </remarks>
        Private Sub syncColumnVisibilityWithHeader(Header As System.Windows.Controls.GridViewColumnHeader)
            If ((Not Header is Nothing) AndAlso (Not Header.Column is Nothing)) Then
                If (Header.Visibility = System.Windows.Visibility.Collapsed) Then
                    ' Hide the column (remember current width).
                    Header.Tag = Header.Column.Width
                    Header.Column.Width = 0.0
                Else
                    ' Show the column (set width to the last value if applicable).
                    ' ClearValue(GridViewColumn.WidthProperty) is here identical mit Width="Auto" in XAML 
                    ' header.Column.ClearValue(GridViewColumn.WidthProperty)
                    Dim width As Double = Double.NaN
                    Try
                        width = CDbl(Header.Tag)
                        If (width < 3) Then width = Double.NaN
                    Catch ex As Exception
                    End Try
                    Header.Column.Width = width
                End if
            End if
        End Sub
        
        ''' <summary> Adjust the width of all visible GridViewColumns to fit the largest visible (!) item (if Console.autoSizeColumns = True). </summary>
        Private Sub adjustGridViewColumnWidths()
            Try
                ' Me.DataContext is Null when this is called for the leaved tab...
                If (Not Me.DataContext Is Nothing) Then
                    If (CType(Me.DataContext, LogBox).Console.autoSizeColumns) then
                        For Each column As System.Windows.Controls.GridViewColumn In Me.MessagesGridView.Columns
                            If (column.ActualWidth > 0) then
                                column.Width = column.ActualWidth + 1
                                column.Width = Double.NaN
                            End if
                        Next
                    End If
                End If
            Catch ex As System.Exception
            End Try
        End Sub
        
        ''' <summary> Scroll to make the last message visible. </summary>
        Private Sub scrollToEndOfLog()
            Try
                Dim LastIndex As Integer = MessagesListView.Items.Count - 1
                If (LastIndex > 0) then
                    MessagesListView.SelectedIndex = LastIndex
                    MessagesListView.ScrollIntoView(MessagesListView.Items(LastIndex))
                    CollectionHasChanged = False
                End If
            Catch ex As System.Exception
            End Try
        End Sub
        
    #End Region
    
End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
