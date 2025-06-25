Imports System.Data.SqlClient
Imports Glossario

Public Class Form1
    Private ReadOnly config As Config = New Config("")
    Private sqlConnections As List(Of MssqlManager)

    Public Sub connectToDb(ByVal connection As String)
        Dim cacheConnection As MssqlManager = New MssqlManager(connection)

        If cacheConnection.HasConnection() Then
            sqlConnections.Add(cacheConnection)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tcMain.TabPages.Clear()

        For Each section In config.getSections()
            'Dim connection As SqlConnection = sqlConnections(0).connection
            Dim connection As SqlConnection = New MssqlManager("Server=(localdb)\Northwind;Database=Northwind;User=gino;Password=1234").connection

            Dim page As TabPage = New TabPage(section)
            Dim dgv As DataGridView = New DataGridView()

            dgv.Location = New Point(6, 6)
            dgv.Size = New Drawing.Size(756, 353)
            dgv.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top

            Dim sqlData As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM Categories", connection)
            Dim dt As DataTable = New DataTable()
            sqlData.Fill(dt)
            dgv.DataSource = dt

            page.Controls.Add(dgv)

            tcMain.TabPages.Add(page)
        Next
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        fDbConnect.Visible = True
    End Sub

    Private Sub tcMain_TabIndexChanged(sender As Object, e As EventArgs) Handles tcMain.TabIndexChanged
        MsgBox("cambiato")
    End Sub
End Class
