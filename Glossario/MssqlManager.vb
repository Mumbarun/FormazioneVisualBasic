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

        'Sub New(Optional ByRef n As String = "", Optional ByRef t As String = "", Optional ByRef mL As Integer = -1, Optional iN As Boolean = False)
        '    name = n
        '    type = t
        '    maxLength = mL
        '    isNullable = iN
        'End Sub
        Sub New()
        End Sub
        Sub New(ByRef n As String, ByRef t As String, ByRef mL As Integer, isN As Boolean)
            name = n
            type = t
            maxLength = mL
            isNullable = isN
        End Sub
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

    Public Function reorderTableColumns(ByVal columns As List(Of ColumnInfo)) As List(Of ColumnInfo)
        Dim res As List(Of ColumnInfo) = New List(Of ColumnInfo)

        For Each column As ColumnInfo In columns
            Dim type As String = column.type
            Dim maxLength As Integer = column.maxLength

            'If column.type.ToUpper() = "DATE" Then
            '    type = "Date"
            'ElseIf column.type.ToUpper() = "TIME" Then
            '    type = "Time"
            'ElseIf column.type.ToUpper() = "DATETIME" Then
            '    type = "DateTime"
            'ElseIf column.type.ToUpper() = "DATETIME2" Then
            '    type = "DateTime2"
            'ElseIf column.type.ToUpper() = "SMALLDATETIME" Then
            '    type = "SmallDateTime"
            'ElseIf column.type.ToUpper() = "DATETIMEOFFSET" Then
            '    type = "DateTimeOffset"
            'ElseIf column.type.ToUpper() = "TINYINT" Then
            '    type = "Integer"
            '    maxLength = 255
            'ElseIf column.type.ToUpper() = "SMALLINT" Then
            '    type = "Integer"
            '    maxLength = 32767
            'ElseIf column.type.ToUpper() = "INT" Then
            '    type = "Integer"
            '    maxLength = 2147483647
            'ElseIf column.type.ToUpper() = "BIGINT" Then
            '    type = "Integer"
            'ElseIf column.type.ToUpper() = "IMAGE" Then
            '    type = "Image"
            'ElseIf column.type.ToUpper() = "TEXT" Or column.type.ToUpper() = "NTEXT" Then
            '    type = "String"
            'ElseIf column.type.ToUpper().Substring(0, 4) = "CHAR" Then
            '    type = "String"
            'ElseIf column.type.ToUpper().Substring(0, 5) = "FLOAT" Then
            '    type = "Integer"
            '    If column.type.Count > 7 Then
            '        maxLength = Convert.ToInt32(column.type.Substring(6, column.type.Count - 1))
            '    End If
            'ElseIf column.type.ToUpper().Substring(0, 7) = "VARCHAR" Then
            '    type = "String"
            'ElseIf column.type.ToUpper().Substring(0, 5) = "NCHAR" Or column.type.ToUpper() = "NTEXT" Then
            '    type = "Id"
            'ElseIf column.type.ToUpper().Substring(0, 8) = "NVARCHAR" Then
            '    type = "Id"
            'End If

            If column.type.ToUpper() = "DATE" Or column.type.ToUpper() = "TIME" Or column.type.ToUpper() = "DATETIME" Or column.type.ToUpper() = "DATETIME2" Or column.type.ToUpper() = "SMALLDATETIME" Or column.type.ToUpper() = "DATETIMEOFFSET" Then
                type = "Date"
            ElseIf column.type.ToUpper() = "TINYINT" Then
                type = "Decimal"
                maxLength = 255
            ElseIf column.type.ToUpper() = "SMALLINT" Then
                type = "Decimal"
                maxLength = 32767
            ElseIf column.type.ToUpper() = "INT" Then
                type = "Decimal"
                maxLength = 2147483647
            ElseIf column.type.ToUpper() = "BIGINT" Then
                type = "Decimal"
            ElseIf column.type.ToUpper() = "IMAGE" Then
                type = "String"
            ElseIf column.type.ToUpper() = "TEXT" Or column.type.ToUpper() = "NTEXT" Then
                type = "String"
            ElseIf column.type.ToUpper().Substring(0, 4) = "CHAR" Then
                type = "String"
            ElseIf column.type.ToUpper().Substring(0, 5) = "FLOAT" Then
                type = "Decimal"
                If column.type.Count > 7 Then
                    maxLength = Convert.ToInt32(column.type.Substring(6, column.type.Count - 1))
                End If
            ElseIf column.type.ToUpper().Substring(0, 7) = "VARCHAR" Then
                type = "String"
            ElseIf column.type.ToUpper().Substring(0, 5) = "NCHAR" Or column.type.ToUpper() = "NTEXT" Then
                type = "String"
            ElseIf column.type.ToUpper().Substring(0, 8) = "NVARCHAR" Then
                type = "String"
            End If

            res.Add(New ColumnInfo(column.name, type, maxLength, column.isNullable))
        Next

        Return res
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
