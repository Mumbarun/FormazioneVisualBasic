Imports System.Collections.Specialized.BitVector32
Imports System.Data.SqlClient
Imports Glossario
Imports Microsoft.SqlServer
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Form1
    Private ReadOnly config As Config = New Config("")
    Private availableSections As String() = generateSections()
    Private tables As DataTable() = generateTables()

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        generateData()
    End Sub

    Public Sub generateData()
        'generateMssqlManagers()

        generateTabs()
    End Sub

    Private Sub generateTabs()
        tcMain.TabPages.Clear()

        For Each section As String In availableSections
            tcMain.TabPages.Add(New TabPage(section))
        Next

        For i As Integer = 0 To (availableSections.Length - 1)
            generateTab(i)
        Next
    End Sub

    Private Sub generateTab(ByVal i As Integer)
        'Dim section As String = availableSections(i)
        Dim section As String = tables(i).TableName
        Dim connection As SqlConnection = tables(i).Rows(0)("MssqlManager").connection
        Dim row As DataRow = tables(i).Rows(0)

        Dim page As TabPage = New TabPage(section)

        'Adding a new DataGridVIew component to the tab
        Dim dgv As DataGridView = New DataGridView()

        dgv.Location = New Point(0, 0)
        dgv.Size = New Drawing.Size(200, 70)
        dgv.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top

        'Adding the connect button component to the tab
        Dim btnUpdate = New Button()

        btnUpdate.Text = "Aggiorna"
        btnUpdate.Location = New Point(0, 75)
        btnUpdate.Size = New Drawing.Size(76, 23)
        btnUpdate.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        AddHandler btnUpdate.Click, AddressOf updateTab

        'Adding the connect button component to the tab
        Dim btnExecute = New Button()

        btnExecute.Text = "Esegui"
        btnExecute.Location = New Point(77, 75)
        btnExecute.Size = New Drawing.Size(76, 23)
        btnExecute.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        AddHandler btnExecute.Click, AddressOf executeCommand

        'Adding the query string text box
        Dim tbQuery As TextBox = New TextBox()

        tbQuery.Location = New Point(22, 75)
        tbQuery.Size = New Drawing.Size(175, 20)
        tbQuery.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        AddHandler tbQuery.TextChanged, AddressOf updateQuery

        'Applying components to the tab
        'Dim sqlData As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM Categories", connection)
        Dim sqlData As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM " + row("Table"), connection)
        Dim dt As DataTable = New DataTable()
        If Not String.IsNullOrWhiteSpace(row("Table")) Then
            sqlData.Fill(dt)
        End If
        dgv.DataSource = dt

        page.Controls.Add(dgv)
        page.Controls.Add(btnUpdate)
        page.Controls.Add(btnExecute)
        page.Controls.Add(tbQuery)

        tcMain.TabPages.RemoveAt(i)
        tcMain.TabPages.Insert(i, page)
    End Sub

    'Private Sub generateMssqlManagers()
    '    sqlConnections.Clear()

    '    For Each section As String In availableSections
    '        sqlConnections.Add(generateMssqlManager(section))
    '    Next
    'End Sub

    Private Function generateMssqlManager(ByVal section As String) As MssqlManager
        Dim Server As String = Split(config.Read(section, "Server"), "=", 2)(1)
        Dim Database As String = Split(config.Read(section, "Database"), "=", 2)(1)
        Dim User As String = Split(config.Read(section, "User"), "=", 2)(1)
        Dim Password As String = Split(config.Read(section, "Password"), "=", 2)(1)

        Return New MssqlManager("Server=" + Server + ";Database=" + Database + ";User=" + User + ";Password=" + Password)
    End Function

    Private Function generateSections() As String()
        Dim cache As List(Of String) = New List(Of String)

        For Each section As String In config.getSections()
            If section.Substring(0, 19) = "databaseConnection_" Then
                cache.Add(section)
            End If
        Next

        Return cache.ToArray()
    End Function

    Private Function generateTables() As DataTable()
        Dim tables As List(Of DataTable) = New List(Of DataTable)

        For Each section As String In availableSections
            Dim table As DataTable = New DataTable(section)

            'Creating table colums
            Dim cID As DataColumn = New DataColumn("ID")
            cID.DataType = System.Type.GetType("System.Int32")

            Dim cTable As DataColumn = New DataColumn("Table")
            cTable.DataType = System.Type.GetType("System.String")

            Dim cMssqlManager As DataColumn = New DataColumn("MssqlManager")
            cMssqlManager.DataType = GetType(MssqlManager)

            table.Columns.Add(cID)
            table.Columns.Add(cTable)
            table.Columns.Add(cMssqlManager)

            'Creating table rows
            Dim row As DataRow = table.NewRow()

            row.Item("ID") = 0
            row.Item("Table") = ""
            row.Item("MssqlManager") = generateMssqlManager(section)

            table.Rows.Add(row)

            tables.Add(table)
        Next

        Return tables.ToArray()
    End Function

    Private Sub updateTab()
        generateTab(tcMain.SelectedIndex)
    End Sub

    Private Sub executeCommand(sender As Object, e As EventArgs)
        Dim command As String = InputBox("Inserire comando", "Inserire comando", "Inserire comando")

        MsgBox("Esecuzione del comando => " + command)

        tables(tcMain.SelectedIndex).Rows(0)("MssqlManager").executeCommand(command)
    End Sub

    Private Sub updateQuery(sender As Object, e As EventArgs)
        Dim tbQuery As TextBox = DirectCast(sender, TextBox)

        tables(tcMain.SelectedIndex).Rows(0)("Table") = tbQuery.Text
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        fDbConnect.Visible = True
    End Sub
End Class
