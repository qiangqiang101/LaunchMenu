Imports System.Runtime.InteropServices

Public Class slUnlock

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            Dim fb As frmScreenLock = ParentForm
            If txtPwd.Text = fb.Password Then
                fb.Close()
                frmMenu.WindowState = FormWindowState.Maximized
            Else
                txtPwd.Clear()
                txtPwd.Focus()
            End If
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tTaskMngr_Tick(sender As Object, e As EventArgs) Handles tTaskMngr.Tick
        For Each selProcess As Process In Process.GetProcesses
            If selProcess.ProcessName = "taskmgr" Then
                selProcess.Kill()
                Exit For
            End If
        Next
    End Sub

    Private Structure KBDLLHOOKSTRUCT
        Public key As Keys
        Public scanCode As Integer
        Public flags As Integer
        Public time As Integer
        Public extra As IntPtr
    End Structure

#Region "Security"
    'System level functions to be used for hook and unhook keyboard input
    Private Delegate Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(ByVal id As Integer, ByVal callback As LowLevelKeyboardProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function UnhookWindowsHookEx(ByVal hook As IntPtr) As Boolean
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function CallNextHookEx(ByVal hook As IntPtr, ByVal nCode As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetModuleHandle(ByVal name As String) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function GetAsyncKeyState(ByVal key As Keys) As Short
    End Function

    'Declaring Global objects
    Private ptrHook As IntPtr
    Private objKeyboardProcess As LowLevelKeyboardProc

    Public Sub New()
        Dim objCurrentModule As ProcessModule = Process.GetCurrentProcess().MainModule
        'Get Current Module
        objKeyboardProcess = New LowLevelKeyboardProc(AddressOf captureKey)
        'Assign callback function each time keyboard process
        ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0)
        'Setting Hook of Keyboard Process for current module
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Function captureKey(ByVal nCode As Integer, ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
        If nCode >= 0 Then
            Dim objKeyInfo As KBDLLHOOKSTRUCT = DirectCast(Marshal.PtrToStructure(lp, GetType(KBDLLHOOKSTRUCT)), KBDLLHOOKSTRUCT)
            If objKeyInfo.key = Keys.RWin OrElse objKeyInfo.key = Keys.LWin Then
                ' Disabling Windows keys
                Return CType(1, IntPtr)
            End If
            If objKeyInfo.key = Keys.ControlKey OrElse objKeyInfo.key = Keys.Escape Then
                ' Disabling Ctrl + Esc keys
                Return CType(1, IntPtr)
            End If
            If objKeyInfo.key = Keys.Alt OrElse objKeyInfo.key = Keys.Tab Then
                ' Disabling Alt + Tab keys
                Return CType(1, IntPtr)
            End If
            If objKeyInfo.key = Keys.Alt OrElse objKeyInfo.key = Keys.F4 Then
                ' Disabling Alt + F4 keys
                Return CType(1, IntPtr)
            End If
            If objKeyInfo.key = Keys.ControlKey OrElse objKeyInfo.key = Keys.Alt Then
                ' Disabling Ctrl + Alt keys
                Return CType(1, IntPtr)
            End If
        End If
        Return CallNextHookEx(ptrHook, nCode, wp, lp)
    End Function
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        If ptrHook <> IntPtr.Zero Then
            UnhookWindowsHookEx(ptrHook)
            ptrHook = IntPtr.Zero
        End If
        MyBase.Dispose(disposing)
    End Sub


    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            Const CS_NOCLOSE As Integer = &H200
            cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE
            Return cp
        End Get
    End Property
#End Region
End Class
