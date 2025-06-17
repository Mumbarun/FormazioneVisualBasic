Imports Glossario

Public Class Form1
    Dim config As Config = New Config()

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'stFirstName = InputBox("Please enter your name")
        MessageBox.Show(config.Read("UserInformation", "name", "Nessun nome trovato"))
    End Sub

    Private Sub tbName_TextChanged(sender As Object, e As EventArgs) Handles tbName.TextChanged
        config.Write("UserInformation", "name", tbName.Text)
    End Sub
End Class
