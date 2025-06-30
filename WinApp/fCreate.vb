Imports Glossario

Public Class fCreate
    Private mssqlManager As MssqlManager
    Private table As String
    Private columns As MssqlManager.ColumnInfo()

    Public Sub New(ByVal mssqlManagerInput As MssqlManager, ByVal tableInput As String)
        InitializeComponent()

        mssqlManager = mssqlManagerInput
        table = tableInput
        columns = mssqlManager.getTableColumns(table).ToArray()

        For Each info As MssqlManager.ColumnInfo In mssqlManagerInput.getTableColumns(table)
            MsgBox("Table => " + table + " con => " + info.name + " | " + info.type.ToString() + " | " + info.maxLength.ToString() + " | " + info.isNullable.ToString().ToUpper())
        Next
    End Sub

    Private Sub render()
        Dim height As Integer = 0

        For Each info As MssqlManager.ColumnInfo In columns
            If info.type = "" Then
            ElseIf info.type = "" Then
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    End Sub
End Class