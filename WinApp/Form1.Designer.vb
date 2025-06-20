<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.lvMain = New System.Windows.Forms.ListView()
        Me.fName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(712, 415)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Salva"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'tbName
        '
        Me.tbName.Location = New System.Drawing.Point(12, 418)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(181, 20)
        Me.tbName.TabIndex = 1
        '
        'lvMain
        '
        Me.lvMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fName, Me.fValue})
        Me.lvMain.HideSelection = False
        Me.lvMain.Location = New System.Drawing.Point(12, 12)
        Me.lvMain.Name = "lvMain"
        Me.lvMain.Size = New System.Drawing.Size(776, 397)
        Me.lvMain.TabIndex = 2
        Me.lvMain.UseCompatibleStateImageBehavior = False
        Me.lvMain.View = System.Windows.Forms.View.Details
        '
        'fName
        '
        Me.fName.Text = "Nome"
        Me.fName.Width = 96
        '
        'fValue
        '
        Me.fValue.Text = "Valore"
        Me.fValue.Width = 144
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(199, 416)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "Aggiungi"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(630, 415)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(76, 23)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Aggiorna"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'dgvMain
        '
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Location = New System.Drawing.Point(9, 444)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMain.Size = New System.Drawing.Size(779, 411)
        Me.dgvMain.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 867)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lvMain)
        Me.Controls.Add(Me.tbName)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents tbName As TextBox
    Friend WithEvents lvMain As ListView
    Friend WithEvents btnAdd As Button
    Friend WithEvents fName As ColumnHeader
    Friend WithEvents fValue As ColumnHeader
    Friend WithEvents btnUpdate As Button
    Friend WithEvents dgvMain As DataGridView
End Class
