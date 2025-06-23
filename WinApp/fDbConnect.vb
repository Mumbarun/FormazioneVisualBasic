Imports Glossario

Public Class fDbConnect
    Private ReadOnly config As Config = New Config("")
    Private Server As String
    Private Database As String
    Private User As String
    Private Password As String

    Private Sub loadData()
        Server = config.Read("databaseConnection", "Server")
        Database = config.Read("databaseConnection", "Database")
        User = config.Read("databaseConnection", "User")
        Password = config.Read("databaseConnection", "Password")

        If String.IsNullOrWhiteSpace(Server) Then
            config.Write("databaseConnection", "Server", "")
        End If

        If String.IsNullOrWhiteSpace(Database) Then
            config.Write("databaseConnection", "Database", "")
        End If

        If String.IsNullOrWhiteSpace(User) Then
            config.Write("databaseConnection", "User", "")
        End If

        If String.IsNullOrWhiteSpace(Password) Then
            config.Write("databaseConnection", "Password", "")
        End If
    End Sub

    Private Sub tbServer_TextChanged(sender As Object, e As EventArgs) Handles tbServer.TextChanged
        config.Write("databaseConnection", "Server", tbServer.Text)
    End Sub
End Class