﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.tcMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(426, 426)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Salva"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(344, 426)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(76, 23)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Aggiorna"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.Location = New System.Drawing.Point(12, 426)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(148, 23)
        Me.btnConnect.TabIndex = 5
        Me.btnConnect.Text = "Connessioni al database"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'tcMain
        '
        Me.tcMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcMain.Controls.Add(Me.TabPage1)
        Me.tcMain.Location = New System.Drawing.Point(12, 12)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(490, 408)
        Me.tcMain.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(482, 382)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 461)
        Me.Controls.Add(Me.tcMain)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnSave)
        Me.MinimumSize = New System.Drawing.Size(500, 500)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.tcMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnConnect As Button
    Friend WithEvents tcMain As TabControl
    Friend WithEvents TabPage1 As TabPage
End Class
