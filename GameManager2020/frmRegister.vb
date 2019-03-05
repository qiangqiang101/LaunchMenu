Imports System.Net

Public Class frmRegister

    Private userHWID As String

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            If Not txtProductKey.Text = Nothing Then
                Dim wClient As New WebClient()
                Dim strSource As String = wClient.DownloadString($"http://zettabytetek.com/software/ELS/index.php?a=register&key={txtProductKey.Text}&hwid={userHWID}&product=LaunchMenu")
                If strSource.Contains("wrong key") Then
                    MsgBox("The product key you entered is invalid or not exists, please try again or contact our support team.", MsgBoxStyle.Exclamation, "Invalid")
                    txtProductKey.Clear()
                    txtProductKey.Focus()
                ElseIf strSource.Contains("wrong hwid") Then
                    MsgBox("Your HWID is invalid, please try again or contact our support team.", MsgBoxStyle.Exclamation, "Invalid")
                    txtProductKey.Clear()
                    txtProductKey.Focus()
                ElseIf strSource.Contains("key is already in use") Then
                    MsgBox("The product key you entered is already in use, please try again or contact our support team.", MsgBoxStyle.Exclamation, "Invalid")
                    txtProductKey.Clear()
                    txtProductKey.Focus()
                Else
                    MsgBox("Product registration was successful!, Thank you for using our product, We will be happy if you spread the word and tell your friends about this product.", MsgBoxStyle.Information, "Successful")
                    frmMain.Show()
                    Close()
                End If
            Else
                MsgBox("Please enter Product key.", MsgBoxStyle.Exclamation, "Error")
                txtProductKey.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        userHWID = $"{getHWID.ToUpper} - {SystemInformation.ComputerName}"
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Close()
    End Sub

    Private Sub lblPurchase_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblPurchase.LinkClicked
        Process.Start("http://zettabytetek.com/product-category/software")
    End Sub
End Class