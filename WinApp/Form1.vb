Imports System.Data.SqlClient
Imports Glossario

Public Class Form1
    Private ReadOnly config As Config = New Config()
    Private availableSections As String() = generateSections()
    Private tables As DataTable() = generateTables()

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Public Sub loadData()
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

        dgv.Name = "dgvMain"
        dgv.Location = New Point(0, 0)
        dgv.Size = New Drawing.Size(200, 70)
        dgv.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Top

        'Adding a new ToolStripMenu
        Dim cms As ContextMenuStrip = New ContextMenuStrip
        cms.Name = "cms" + section

        'Adding Edit button to the ToolStripMenu
        Dim tsmiEdit As ToolStripMenuItem = New ToolStripMenuItem("Modifica")
        tsmiEdit.Name = "tsmiEdit" + section
        AddHandler tsmiEdit.Click, AddressOf editItem

        'Adding Delete button to the ToolStripMenu
        Dim tsmiDelete As ToolStripMenuItem = New ToolStripMenuItem("Elimina")
        tsmiDelete.Name = "tsmiDelete" + section
        AddHandler tsmiDelete.Click, AddressOf deleteItem

        cms.Items.Add(tsmiEdit)
        cms.Items.Add(tsmiDelete)

        dgv.ContextMenuStrip = cms

        'Setting the DataGridView in read only
        dgv.MultiSelect = False
        dgv.ReadOnly = True

        'Adding the mouse down manager function for auto-select the DataGridVIew row
        AddHandler dgv.MouseDown, AddressOf dgvMouseDown

        'Adding the new element button component to the tab
        Dim btnExecute = New Button()

        btnExecute.Text = "Nuovo"
        btnExecute.Location = New Point(0, 75)
        btnExecute.Size = New Drawing.Size(76, 23)
        btnExecute.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        AddHandler btnExecute.Click, AddressOf newElement

        'Adding the update button component to the tab
        Dim btnUpdate = New Button()

        btnUpdate.Text = "Aggiorna"
        btnUpdate.Location = New Point(77, 75)
        btnUpdate.Size = New Drawing.Size(76, 23)
        btnUpdate.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        btnUpdate.Visible = False

        AddHandler btnUpdate.Click, AddressOf updateTab

        'Adding the query string combobox
        Dim cbQuery As ComboBox = New ComboBox()

        cbQuery.Items.Clear()
        For Each table As String In tables(i).Rows(0)("MssqlManager").tables
            cbQuery.Items.Add(table)
        Next

        cbQuery.Text = "Seleziona tabella"
        cbQuery.Location = New Point(22, 75)
        cbQuery.Size = New Drawing.Size(175, 20)
        cbQuery.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        cbQuery.DropDownStyle = ComboBoxStyle.DropDownList

        If Not String.IsNullOrWhiteSpace(tables(i).Rows(0)("Table")) Then
            cbQuery.SelectedItem = tables(i).Rows(0)("Table")
        End If

        AddHandler cbQuery.SelectedIndexChanged, AddressOf updateQuery

        'Applying components to the tab
        Dim sqlData As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM " + row("Table"), connection)
        Dim dt As DataTable = New DataTable()
        If Not String.IsNullOrWhiteSpace(row("Table")) Then
            sqlData.Fill(dt)
        End If
        dgv.DataSource = dt

        page.Controls.Add(dgv)
        page.Controls.Add(btnUpdate)
        page.Controls.Add(btnExecute)
        'page.Controls.Add(tbQuery)
        page.Controls.Add(cbQuery)

        tcMain.TabPages.RemoveAt(i)
        tcMain.TabPages.Insert(i, page)
    End Sub

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

    Private Sub newElement(sender As Object, e As EventArgs)
        updateTab()

        Dim createForm As fCreate = New fCreate(tables(tcMain.SelectedIndex).Rows(0)("MssqlManager"), tables(tcMain.SelectedIndex).Rows(0)("Table"))

        createForm.ShowDialog()
        updateTab()

        'If createForm.ShowDialog() = DialogResult.OK Then
        '    MsgBox("form creato")
        'Else
        '    MsgBox("form annullato")
        'End If
    End Sub

    Private Sub updateQuery(sender As Object, e As EventArgs)
        Dim cbQuery As ComboBox = DirectCast(sender, ComboBox)

        tables(tcMain.SelectedIndex).Rows(0)("Table") = cbQuery.SelectedItem

        updateTab()
    End Sub

    Private Sub editItem(sender As Object, e As EventArgs)
        Dim dgv As DataGridView = Me.Controls.Find("dgvMain", True).FirstOrDefault()

        If dgv.SelectedRows.Count > 0 Then
            Dim rSelected As DataGridViewRow = dgv.SelectedRows(0)

            Dim query As KeyValuePair(Of String, Object) = New KeyValuePair(Of String, Object)(dgv.Columns.Item(0).HeaderText, rSelected.Cells.Item(0).Value)

            Dim createForm As fCreate = New fCreate(tables(tcMain.SelectedIndex).Rows(0)("MssqlManager"), tables(tcMain.SelectedIndex).Rows(0)("Table"), query)

            createForm.ShowDialog()
            updateTab()
        Else
            MsgBox("Nessuna riga evidenziata")
        End If
    End Sub

    Private Sub deleteItem(sender As Object, e As EventArgs)
        Dim dgv As DataGridView = Me.Controls.Find("dgvMain", True).FirstOrDefault()

        If dgv.SelectedRows.Count > 0 Then
            Dim rSelected As DataGridViewRow = dgv.SelectedRows(0)

            Dim key As String = dgv.Columns.Item(0).HeaderText
            Dim value As Object = rSelected.Cells.Item(0).Value

            If tables(tcMain.SelectedIndex).Rows(0)("MssqlManager").deleteOneElement(tables(tcMain.SelectedIndex).Rows(0)("table"), key, value) Then
                updateTab()
            End If
        Else
            MsgBox("Nessuna riga evidenziata")
        End If
    End Sub

    Private Sub dgvMouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            Dim dgv As DataGridView = DirectCast(sender, DataGridView)
            Dim hitTest As DataGridView.HitTestInfo = dgv.HitTest(e.X, e.Y)

            If hitTest.Type = DataGridViewHitTestType.Cell OrElse hitTest.Type = DataGridViewHitTestType.RowHeader Then
                dgv.ClearSelection() ' Deseleziona eventuali righe precedentemente selezionate
                dgv.Rows(hitTest.RowIndex).Selected = True
            End If
        End If
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        fDbConnect.Visible = True
    End Sub
End Class
