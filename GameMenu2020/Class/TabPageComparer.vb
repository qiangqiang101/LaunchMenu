Public Class TabPageComparer
    Implements IComparer(Of TabPage)

    Public Function Compare(ByVal x As TabPage, ByVal y As TabPage) As Integer Implements IComparer(Of TabPage).Compare
        Return String.Compare(x.Text, y.Text)
    End Function
End Class