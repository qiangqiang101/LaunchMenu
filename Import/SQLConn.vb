Public Class sqlConn

#Region "Class Members"
    Friend WithEvents OLEConn As New System.Data.OleDb.OleDbConnection()
    Friend WithEvents OLEComm As New System.Data.OleDb.OleDbCommand()

    Private sqlString As String
    Private err As System.Exception

    Public Shared dataReturned As New ArrayList()
#End Region

#Region "class properties"

    Public Property xOLE() As String
        Get
            xOLE = "Provider=Microsoft.Jet.OLEDB.4.0;Data source="
        End Get
        Set(ByVal Value As String)
            Value = xOLE
        End Set
    End Property

#End Region

#Region "class methods"

    Sub New()
    End Sub

    Function connectMe(ByVal sqlString As String, ByVal db As String) As Boolean
        Try
            OLEConn.ConnectionString = xOLE & db
            OLEConn.Open()
            OLEComm.CommandText = sqlString
            Return True
        Catch err As System.Exception
            MsgBox(err.Message)
            Return False
        End Try
    End Function

    Function getData() As ArrayList
        Try

            OLEComm.Connection = OLEConn

            getData = New ArrayList()

            Dim d As OleDb.OleDbDataReader = OLEComm.ExecuteReader()
            Do While d.Read
                getData.Add(d("Game_Text".ToString))
            Loop

            'Returns array collection
            dataReturned = getData

            Try
                OLEConn.Close()
            Catch err As System.Exception
                MsgBox(err.Message)
            End Try
        Catch err As System.Exception
            MsgBox(err.Message)
        End Try
    End Function

    Function getUser() As ArrayList
        Try

            OLEComm.Connection = OLEConn

            getUser = New ArrayList()

            Dim d As OleDb.OleDbDataReader = OLEComm.ExecuteReader()
            Do While d.Read
                getData.Add(d("UsrName".ToString))
            Loop

            'Returns array collection
            dataReturned = getData()

            Try
                OLEConn.Close()
            Catch err As System.Exception
                MsgBox(err.Message)
            End Try
        Catch err As System.Exception
            MsgBox(err.Message)
        End Try
    End Function
#End Region
End Class
