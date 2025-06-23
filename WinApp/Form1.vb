Imports Glossario

Public Class Form1
    Private ReadOnly config As Config = New Config("")

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'stFirstName = InputBox("Please enter your name")
        MessageBox.Show(config.Read("CustomInformation", "name"))
    End Sub

    'Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
    '    UpdateList()
    'End Sub

    'Public Sub UpdateList()
    'Public Sub UpdateList()
    '    lvMain.Items.Clear()

    '    Dim items As Dictionary(Of String, String) = config.ReadAll("CustomInformation")

    '    If items.Count > 0 Then
    '        For Each item As KeyValuePair(Of String, String) In items
    '            AddToList(item.Key, item.Value)
    '        Next
    '    End If
    'End Sub

    'Public Sub AddToList(ByVal key As String, ByVal value As String)
    '    Dim item As New ListViewItem(key)

    '    MsgBox(key & value)

    '    item.SubItems.Add(value)

    '    lvMain.Items.Add(item)
    'End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sqlCon As MssqlManager = New MssqlManager("Server=(localdb)\Northwind;Database=Northwind;User=gino;Password=1234")

        sqlCon.HasConnection()
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        fDbConnect.Visible = True
    End Sub
End Class
