Imports System.Xml.Serialization
Imports System.IO

Public Structure Trial

    Public ReadOnly Property Instance As Trial
        Get
            Return ReadfromFile()
        End Get
    End Property

    <XmlIgnore>
    Public Property TrialFileName() As String

    Public StartDate As String

    Public Sub New(fn As String)
        TrialFileName = fn
    End Sub

    Public Sub New(fn As String, sd As String)
        TrialFileName = fn
        StartDate = sd
    End Sub

    Public Sub Save()
        Dim ser = New XmlSerializer(GetType(Trial))
        Dim writer As TextWriter = New StreamWriter(TrialFileName)
        ser.Serialize(writer, Me)
        writer.Close()
    End Sub

    Public Function ReadFromFile() As Trial
        If Not File.Exists(TrialFileName) Then
            Return New Trial(TrialFileName, StartDate)
        End If

        Try
            Dim ser = New XmlSerializer(GetType(Trial))
            Dim reader As TextReader = New StreamReader(TrialFileName)
            Dim instance = CType(ser.Deserialize(reader), Trial)
            reader.Close()
            Return instance
        Catch ex As Exception
            Return New Trial(TrialFileName, StartDate)
        End Try
    End Function

End Structure
