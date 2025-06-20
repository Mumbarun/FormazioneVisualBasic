Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class MssqlManager
    Public connection As SqlConnection
    Public sqlCommand As SqlCommand

    Private _Cnn As String = ""

    Sub New(sCnn As String)
        _Cnn = sCnn
        Open()
    End Sub

    Public Sub Open()
        connection = New SqlConnection With {.ConnectionString = _Cnn}
    End Sub

    Public Function HasConnection() As Boolean
        Try
            connection.Open()

            connection.Close()

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)

            Return False
        End Try
    End Function
End Class
