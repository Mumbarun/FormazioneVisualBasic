<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fCreate
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
        Me.pMain = New System.Windows.Forms.Panel()
        Me.vsbMain = New System.Windows.Forms.VScrollBar()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(396, 415)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Crea"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'pMain
        '
        Me.pMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pMain.Location = New System.Drawing.Point(12, 12)
        Me.pMain.Name = "pMain"
        Me.pMain.Size = New System.Drawing.Size(440, 397)
        Me.pMain.TabIndex = 2
        '
        'vsbMain
        '
        Me.vsbMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vsbMain.Location = New System.Drawing.Point(455, 12)
        Me.vsbMain.Name = "vsbMain"
        Me.vsbMain.Size = New System.Drawing.Size(17, 397)
        Me.vsbMain.TabIndex = 3
        '
        'fCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 450)
        Me.Controls.Add(Me.vsbMain)
        Me.Controls.Add(Me.pMain)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "fCreate"
        Me.Text = "fCreate"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents pMain As Panel
    Friend WithEvents vsbMain As VScrollBar
End Class
