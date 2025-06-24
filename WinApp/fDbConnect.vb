Imports Glossario

Public Class fDbConnect
    Private ReadOnly config As Config = New Config("")
    Private sections As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private selectedSection As String = "databaseConnection_1"

    Private Server As String
    Private Database As String
    Private User As String
    Private Password As String

    Private Sub loadSections()
        Dim cachesections As List(Of String) = config.getSections()
        sections.Clear()

        'Load from config file
        For Each section As String In cachesections
            Dim cache(2) As String
            cache = Split(section, "_", 2)

            If cache.Count > 1 And cache(0) = "databaseConnection" Then
                sections.Add(cache(1), cache(0))
            End If
        Next
    End Sub

    Private Sub renderSections()
        lvMain.Items.Clear()

        For Each section As KeyValuePair(Of String, String) In sections
            Dim item As ListViewItem = New ListViewItem(section.Key)
            item.SubItems.Add(section.Value)

            lvMain.Items.Add(item)
        Next
    End Sub

    Private Sub loadData(ByVal section As String)
        Server = Split(config.Read(section, "Server"), "=", 2)(1)
        Database = Split(config.Read(section, "Database"), "=", 2)(1)
        User = Split(config.Read(section, "User"), "=", 2)(1)
        Password = Split(config.Read(section, "Password"), "=", 2)(1)

        If String.IsNullOrWhiteSpace(Server) Then
            config.Write(section, "Server", "")
        Else
            tbServer.Text = Server
        End If

        If String.IsNullOrWhiteSpace(Database) Then
            config.Write(section, "Database", "")
        Else
            tbDatabase.Text = Database
        End If

        If String.IsNullOrWhiteSpace(User) Then
            config.Write(section, "User", "")
        Else
            tbUser.Text = User
        End If

        If String.IsNullOrWhiteSpace(Password) Then
            config.Write(section, "Password", "")
        Else
            tbPassword.Text = Password
        End If
    End Sub

    Private Sub fDbConnect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadSections()
        renderSections()

        loadData(selectedSection)
    End Sub

    Private Sub tbServer_TextChanged(sender As Object, e As EventArgs) Handles tbServer.TextChanged
        config.Write(selectedSection, "Server", tbServer.Text)
    End Sub

    Private Sub tbDatabase_TextChanged(sender As Object, e As EventArgs) Handles tbDatabase.TextChanged
        config.Write(selectedSection, "Database", tbDatabase.Text)
    End Sub

    Private Sub tbUser_TextChanged(sender As Object, e As EventArgs) Handles tbUser.TextChanged
        config.Write(selectedSection, "User", tbUser.Text)
    End Sub

    Private Sub tbPassword_TextChanged(sender As Object, e As EventArgs) Handles tbPassword.TextChanged
        config.Write(selectedSection, "Password", tbPassword.Text)
    End Sub

    Private Sub lvMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMain.SelectedIndexChanged
        If lvMain.SelectedItems.Count > 0 Then
            selectedSection = "databaseConnection_" + lvMain.SelectedItems(0).Text
            MsgBox(selectedSection)
            loadData(selectedSection)
        End If
    End Sub

    Private Sub btnNewConnection_Click(sender As Object, e As EventArgs) Handles btnNewConnection.Click
        'loadSections()
        'Dim length As Integer = 0
        'length = sections.Count + 1

        'config.Write("databaseConnection_" + length.ToString(), "Server", "")
        'config.Write("databaseConnection_" + length.ToString(), "Database", "")
        'config.Write("databaseConnection_" + length.ToString(), "User", "")
        'config.Write("databaseConnection_" + length.ToString(), "Password", "")

        'loadSections()

        loadSections()
        Dim length As Integer = 0
        length = sections.Count + 1

        selectedSection = "databaseConnection_" + length.ToString()

        tbServer.Text = ""
        tbDatabase.Text = ""
        tbUser.Text = ""
        tbPassword.Text = ""

        loadSections()
        renderSections()
    End Sub
End Class