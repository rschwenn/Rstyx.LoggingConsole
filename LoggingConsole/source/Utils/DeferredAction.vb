
Imports System
Imports System.Threading
Imports System.Windows.Threading

''' <summary>  A DeferTimer that performs an Action on a certain thread when time elapses. Rescheduling is supported. </summary>
 ''' <remarks> 
 ''' <para>
 ''' By default that works on the current thread. By specifying a Dispatcher another thread can be determined.
 ''' </para>
 ''' <para>
 ''' Origin: <a href="http://www.codeproject.com/KB/WPF/SnappyFiltering.aspx" target="_blank"> "Deferring ListCollectionView filter updates for a responsive UI" by Matt T Hayes </a> 
 ''' </para>
 ''' <para>
 ''' Changes: Support for use of any thread.
 ''' </para>
 ''' <para>
 ''' Usage example:
 ''' </para>
 ''' <para>
 ''' Current thread: <c>Dim DeferredScrollAction As DeferredAction = New DeferredAction(AddressOf scrollToEndOfLog)</c>
 ''' </para>
 ''' <para>
 ''' Specified thread (WPF Application's UI tread in this case): <c>Dim DeferredScrollAction As DeferredAction = New DeferredAction(AddressOf scrollToEndOfLog, System.Windows.Application.Current.Dispatcher)</c>
 ''' </para>
 ''' <para>
 ''' Schedule the Action: <c>DeferredScrollAction.Defer(TimeSpan.FromMilliseconds(100))</c>
 ''' </para>
 ''' </remarks>
Public Class DeferredAction
    Implements IDisposable
    
    Private DeferTimer As Timer
    
    #Region "Constuctor"
        
        ''' <summary> Creates a new DeferredAction running in current thread. </summary>
         ''' <param name="Action"> The Action that is intended to be invoked deferred. </param>
         ''' <exception cref="ArgumentNullException"> <paramref name="Action"/> is <see langword="null"/>. </exception>
        Public Sub New(Action As Action)
            Me.New(Action, Dispatcher.CurrentDispatcher)
        End Sub
        
        ''' <summary> Creates a new DeferredAction running in a given thread. </summary>
         ''' <param name="Action">     The Action that is intended to be invoked deferred. </param>
         ''' <param name="Dispatcher"> The Dispatcher that will invoke the Action (when time has come). </param>
         ''' <exception cref="ArgumentNullException"> <paramref name="Action"/> is <see langword="null"/>. </exception>
         ''' <exception cref="ArgumentNullException"> <paramref name="Dispatcher"/> is <see langword="null"/>. </exception>
        Public Sub New(Action As Action, Dispatcher As Dispatcher)
            
            If (Action Is Nothing) Then Throw New ArgumentNullException("action")
            If (Dispatcher Is Nothing) Then Throw New ArgumentNullException("dispatcher")
            
            Me.DeferTimer = New Timer(New TimerCallback(Sub() Dispatcher.Invoke(Action) ))
        End Sub
        
    #End Region
    
    #Region "Public Methods"
        
        ''' <summary>
        ''' Schedules the Action for performing once after the specified Delay.
        ''' Repeated calls will reschedule the Action, if it has not already been performed.
        ''' </summary>
         ''' <param name="Delay"> The amount of time to wait before performing the Action. </param>
         ''' <exception cref="ObjectDisposedException">     This <see cref="DeferredAction"/> (resp. it's DeferTimer) has been already disposed of. </exception>
         ''' <exception cref="ArgumentOutOfRangeException"> <paramref name="Delay"/> is less than -1. </exception>
         ''' <exception cref="NotSupportedException">       <paramref name="Delay"/> is greater than 4294967294 milliseconds. </exception>
        Public Sub Defer(Delay As TimeSpan)
            Me.DeferTimer.Change(Delay, TimeSpan.FromMilliseconds(-1))
        End Sub
        
        ''' <summary>
        ''' Schedules the Action for performing once after the specified Delay.
        ''' Repeated calls will reschedule the Action, if it has not already been performed.
        ''' </summary>
         ''' <param name="Delay"> The amount of time (in milliseconds) to wait before performing the Action. </param>
         ''' <exception cref="ObjectDisposedException">     This <see cref="DeferredAction"/> (resp. it's DeferTimer) has been already disposed of. </exception>
         ''' <exception cref="ArgumentOutOfRangeException"> <paramref name="Delay"/> is less than -1. </exception>
         ''' <exception cref="NotSupportedException">       <paramref name="Delay"/> is greater than 4294967294 milliseconds. </exception>
        Public Sub Defer(Delay As Double)
            Me.DeferTimer.Change(TimeSpan.FromMilliseconds(Delay), TimeSpan.FromMilliseconds(-1))
        End Sub
        
        ''' <summary> Aborts an active schedule, if there is one. </summary>
         ''' <exception cref="ObjectDisposedException"> This <see cref="DeferredAction"/> (resp. it's DeferTimer) has been already disposed of. </exception>
        Public Sub Abort()
            Me.DeferTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1))
        End Sub
        
    #End Region
    
    #Region "IDisposable Members"
        
        Public Sub Dispose() Implements IDisposable.Dispose
            If (Me.DeferTimer IsNot Nothing) Then
                Me.DeferTimer.Dispose()
                Me.DeferTimer = Nothing
            End If
        End Sub
        
    #End Region

End Class

' for jEdit:  :collapseFolds=2::tabSize=4::indentSize=4:
