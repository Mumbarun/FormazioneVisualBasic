Imports Glossario

Public Class fCreate
    Private mssqlManager As MssqlManager
    Private table As String
    Private columns As MssqlManager.ColumnInfo()
    Private values As List(Of Object) = New List(Of Object)
    Private tMain As DataTable

    Public Sub New(ByVal mssqlManagerInput As MssqlManager, ByVal tableInput As String)
        InitializeComponent()

        'Colleague the scroller to the pannel
        renderScroll()

        mssqlManager = mssqlManagerInput
        table = tableInput

        loadData()

        'For Each info As MssqlManager.ColumnInfo In columns
        '    MsgBox("Table => " + table + " con => " + info.name + " | " + info.type.ToString() + " | " + info.maxLength.ToString() + " | " + info.isNullable.ToString().ToUpper())
        'Next

        render()
    End Sub

    Private Sub loadData()
        columns = mssqlManager.reorderTableColumns(mssqlManager.getTableColumns(table)).ToArray()

        For Each info As MssqlManager.ColumnInfo In columns
            If info.type = "String" Or info.type = "Id" Then
                values.Add("")
            ElseIf info.type = "Integer" Or info.type = "Decimal" Then
                values.Add(0)
            Else
                values.Add("")
            End If
        Next

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

                tb.Name = "tb_" + info.name
                tb.MaxLength = info.maxLength
                tb.Size = New Drawing.Size(250, 20)
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

                nud.Name = "nud_" + info.name
                nud.Maximum = info.maxLength
                nud.Size = New Drawing.Size(250, 20)
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
        res.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        Return res
    End Function

    Private Sub renderScroll()
        vsbMain.Value = pMain.VerticalScroll.Value
        vsbMain.Minimum = pMain.VerticalScroll.Minimum
        vsbMain.Maximum = pMain.VerticalScroll.Maximum
    End Sub

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
            Dim row As DataRow = res.NewRow()

            row.Item("info") = info
            If info.type = "String" Or info.type = "Id" Then
                row.Item("id") = "tb_" + info.name
                row.Item("value") = ""
            ElseIf info.type = "Integer" Or info.type = "Decimal" Then
                row.Item("id") = "nud_" + info.name
                row.Item("value") = 0
            Else
                row.Item("id") = info.name
                row.Item("value") = ""
            End If

            res.Rows.Add(row)
        Next

        Return res
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Get the values in the form

        For Each v As Object In values
            MsgBox(v.ToString())
        Next

        For i As Integer = 0 To (columns.Length - 1)

        Next
    End Sub

    Private Sub vsbMain_Scroll(sender As Object, e As ScrollEventArgs) Handles vsbMain.Scroll
        pMain.VerticalScroll.Value = vsbMain.Value
    End Sub

    Private Sub pMain_SizeChanged(sender As Object, e As EventArgs) Handles pMain.SizeChanged
        'renderScroll()
    End Sub

    Private Sub tbUpdateValues(sender As Object, e As EventArgs)
        Dim tb As TextBox = DirectCast(sender, TextBox)

        MsgBox(tb.Name)
        Dim row As DataRow = tMain.Select("id='" + tb.Name + "'").FirstOrDefault()
    End Sub

    Private Sub nudUpdateValues(sender As Object, e As EventArgs)
        Dim tb As NumericUpDown = DirectCast(sender, NumericUpDown)

        MsgBox(tb.Name)
    End Sub
End Class