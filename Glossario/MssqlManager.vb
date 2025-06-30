Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class MssqlManager
    Public connection As SqlConnection
    Public sqlCommand As SqlCommand
    Public tables As String()

    Private _Cnn As String = ""

    Sub New(sCnn As String)
        _Cnn = sCnn
        Open()
    End Sub

    Public Sub Open()
        connection = New SqlConnection With {.ConnectionString = _Cnn}

        tables = getTables()
    End Sub

    Public Class ColumnInfo
        Public Property name As String
        Public Property type As String
        Public Property maxLength As Integer
        Public Property isNullable As Boolean
    End Class

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

    Public Function getTables() As String()
        Dim res As List(Of String) = New List(Of String)

        Try
            connection.Open()

            Dim query As String = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        res.Add(reader("TABLE_NAME").ToString())
                    End While
                End Using
            End Using

            connection.Close()

            Return res.ToArray()
        Catch ex As Exception
            MsgBox(ex.Message)

            connection.Close()

            Return res.ToArray()
        End Try
    End Function

    Public Function getTableColumns(ByVal table As String) As List(Of ColumnInfo)
        Dim res As List(Of ColumnInfo) = New List(Of ColumnInfo)

        Try
            connection.Open()

            Dim query As String = "SELECT " &
                "COLUMN_NAME, " &
                "DATA_TYPE, " &
                "CHARACTER_MAXIMUM_LENGTH, " &
                "IS_NULLABLE " &
                "FROM INFORMATION_SCHEMA.COLUMNS " &
                "WHERE TABLE_NAME = @TableName " &
                "ORDER BY ORDINAL_POSITION"

            Dim command As SqlCommand = New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@TableName", table)

            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                Dim info As ColumnInfo = New ColumnInfo()

                info.name = reader("COLUMN_NAME").ToString()
                info.type = reader("DATA_TYPE").ToString()
                If Not IsDBNull(reader("CHARACTER_MAXIMUM_LENGTH")) Then
                    info.maxLength = Convert.ToInt32(reader("CHARACTER_MAXIMUM_LENGTH"))
                Else
                    info.maxLength = -1
                End If
                info.isNullable = (reader("IS_NULLABLE").ToString().ToUpper() = "YES")

                res.Add(info)
            End While

            connection.Close()

            Return res
        Catch ex As Exception
            MsgBox(ex.Message)

            Return res
        End Try
    End Function

    Public Function executeCommand(ByVal c As String) As Boolean
        Try
            connection.Open()

            Dim command As New SqlCommand(c, connection)
            command.ExecuteNonQuery()

            connection.Close()
            Return True
        Catch ex As Exception
            MsgBox("Errore nell'esecuzione di quet'operazione nel database => " + ex.Message)
            Return False
        End Try
    End Function
End Class
