Imports Glossario

Public Class fCreate
    Private mssqlManager As MssqlManager
    Private table As String
    Private columns As MssqlManager.ColumnInfo()
    Private tMain As DataTable
    Private id As KeyValuePair(Of String, Object)

    Public Sub New(ByVal mssqlManagerInput As MssqlManager, ByVal tableInput As String)
        InitializeComponent()

        'Colleague the scroller to the pannel
        renderScroll()

        mssqlManager = mssqlManagerInput
        table = tableInput

        loadData()

        render()
    End Sub

    Public Sub New(ByVal mssqlManagerInput As MssqlManager, ByVal tableInput As String, ByVal input As KeyValuePair(Of String, Object))
        InitializeComponent()

        'Colleague the scroller to the pannel
        renderScroll()

        mssqlManager = mssqlManagerInput
        table = tableInput

        columns = mssqlManager.reorderTableColumns(mssqlManager.getTableColumns(table)).ToArray()
        tMain = generateTable()

        render()

        'Additional part for the "input" value
        id = input
        loadRow(input)
        btnSave.Text = "Modifica"
    End Sub

    Private Sub loadData()
        columns = mssqlManager.reorderTableColumns(mssqlManager.getTableColumns(table)).ToArray()

        tMain = generateTable()
    End Sub

    Private Sub render()
        Dim height As Integer = 10

        pMain.Controls.Clear()

        'Generate elements
        For Each item As DataRow In tMain.Rows
            Dim info As MssqlManager.ColumnInfo = item.Item("info")
            Dim id As String = item.Item("id").ToString()
            Dim value As Object = item.Item("value")

            If info.type = "String" Or info.type = "Id" Then
                Dim tb As TextBox = New TextBox()

                'tb.Name = "tb_" + info.name
                tb.Name = info.name
                tb.MaxLength = info.maxLength
                tb.Size = New Drawing.Size(270, 20)
                tb.Location = New Point(160, height)
                tb.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

                Dim label As Label = createLabel(info.name, height)
                If Not info.isNullable Then
                    label.Text = "* " + info.name
                End If

                AddHandler tb.TextChanged, AddressOf tbUpdateValues

                pMain.Controls.Add(tb)
                pMain.Controls.Add(label)
            ElseIf info.type = "Integer" Or info.type = "Decimal" Then
                Dim nud As NumericUpDown = New NumericUpDown()

                'nud.Name = "nud_" + info.name
                nud.Name = info.name
                nud.Maximum = info.maxLength
                nud.Size = New Drawing.Size(270, 20)
                nud.Location = New Point(160, height)
                nud.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

                AddHandler nud.TextChanged, AddressOf nudUpdateValues

                Dim label As Label = createLabel(info.name, height)
                If Not info.isNullable Then
                    label.Text = "* " + info.name
                End If

                pMain.Controls.Add(nud)
                pMain.Controls.Add(label)
            Else
                Dim l As Label = New Label
                l.Text = info.type
                l.Location = New Point(160, height)

                Dim l2 As Label = New Label
                l2.Text = info.maxLength
                l2.Location = New Point(300, height)

                pMain.Controls.Add(createLabel(info.name, height))
                pMain.Controls.Add(l)
                pMain.Controls.Add(l2)
            End If

            height = height + 30
        Next

        renderScroll()
    End Sub

    Private Function createLabel(ByVal name As String, ByVal height As Integer) As Label
        Dim res As Label = New Label()
        res.Name = "l" + name
        res.Text = name
        res.Size = New Drawing.Size(150, 20)
        res.Location = New Point(0, height)
        res.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        Return res
    End Function

    Private Sub renderScroll()
        vsbMain.Value = pMain.VerticalScroll.Value
        vsbMain.Minimum = pMain.VerticalScroll.Minimum
        vsbMain.Maximum = pMain.VerticalScroll.Maximum
    End Sub

    Private Function generateQuery() As String
        'Dim query As String = "SET IDENTITY_INSERT " + table + " ON; " &
        '    "SET IDENTITY_INSERT " + table + " ON; Insert into " + table + " values("
        Dim query As String = "SET IDENTITY_INSERT " + table + " ON;" & vbCrLf &
            "INSERT INTO " + table + " ("

        For i As Integer = 0 To (tMain.Rows.Count - 1)
            Dim row As DataRow = tMain.Rows(i)
            Dim info As MssqlManager.ColumnInfo = row("info")

            query = query & info.name

            If i < tMain.Rows.Count - 1 Then
                query = query & ","
            End If
        Next

        query = query & ")" & vbCrLf &
            "VALUES ("

        For i As Integer = 0 To (tMain.Rows.Count - 1)
            Dim row As DataRow = tMain.Rows(i)
            Dim info As MssqlManager.ColumnInfo = row("info")

            If row("value").ToString() = "" Then
                query = query & "NULL"
            Else
                query = query & "'" + row("value").ToString() + "'"
            End If

            If i < tMain.Rows.Count - 1 Then
                query = query & ","
            End If
        Next
        query = query & ")" & vbCrLf &
            "SET IDENTITY_INSERT " + table + " OFF;"

        Return query
    End Function

    Private Function generateTable() As DataTable
        Dim res As DataTable = New DataTable

        'Creating data columns
        Dim cId As DataColumn = New DataColumn("id")
        cId.DataType = System.Type.GetType("System.String")

        Dim cInfo As DataColumn = New DataColumn("info")
        cInfo.DataType = GetType(MssqlManager.ColumnInfo)

        Dim cValue As DataColumn = New DataColumn("value")
        cValue.DataType = System.Type.GetType("System.Object")

        res.Columns.Add(cId)
        res.Columns.Add(cInfo)
        res.Columns.Add(cValue)

        'Creating data rows
        For Each info As MssqlManager.ColumnInfo In columns
            If Not info.type = "Id" Then
                Dim row As DataRow = res.NewRow()

                row.Item("info") = info
                If info.type = "String" Then
                    row.Item("id") = info.name
                    row.Item("value") = ""
                ElseIf info.type = "Integer" Or info.type = "Decimal" Then
                    row.Item("id") = info.name
                    row.Item("value") = 0
                Else
                    row.Item("id") = info.name
                    row.Item("value") = ""
                End If

                res.Rows.Add(row)
            End If
        Next

        Return res
    End Function

    Private Sub loadRow(ByVal input As KeyValuePair(Of String, Object))
        Dim item As Dictionary(Of String, Object) = mssqlManager.findOne(table, input)

        For Each element As KeyValuePair(Of String, Object) In item
            'pMain.Controls.Find(element.Key.ToString(), True).FirstOrDefault().Text = element.Value

            Dim els As Control() = Me.Controls.Find(element.Key.ToString(), True)

            If els.Count > 0 Then
                Dim el As Control = els.FirstOrDefault()

                'MsgBox(element.Key.ToString() + " => " + element.Value.ToString())

                If TypeOf el Is TextBox Then
                    Dim tb As TextBox = CType(el, TextBox)
                    tb.Text = element.Value.ToString()
                Else
                    Dim nud As NumericUpDown = CType(el, NumericUpDown)
                    nud.Value = element.Value
                End If
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim query As String = generateQuery()
        If mssqlManager.executeCommand(query) Then
            Form1.loadData()
            Me.Close()
        Else
            MsgBox("Errore durante la creazione dell'oggetto in " + table)
        End If
    End Sub

    Private Sub vsbMain_Scroll(sender As Object, e As ScrollEventArgs) Handles vsbMain.Scroll
        pMain.VerticalScroll.Value = vsbMain.Value
    End Sub

    Private Sub pMain_SizeChanged(sender As Object, e As EventArgs) Handles pMain.SizeChanged
        'renderScroll()
    End Sub

    Private Sub tbUpdateValues(sender As Object, e As EventArgs)
        Dim tb As TextBox = DirectCast(sender, TextBox)

        Dim row As DataRow = tMain.Select("id='" + tb.Name + "'").FirstOrDefault()
        Dim index As Integer = tMain.Rows.IndexOf(row)

        tMain.Rows(index)("value") = tb.Text
    End Sub

    Private Sub nudUpdateValues(sender As Object, e As EventArgs)
        Dim nud As NumericUpDown = DirectCast(sender, NumericUpDown)

        Dim row As DataRow = tMain.Select("id='" + nud.Name + "'").FirstOrDefault()
        Dim index As Integer = tMain.Rows.IndexOf(row)

        tMain.Rows(index)("value") = nud.Value
    End Sub
End Class