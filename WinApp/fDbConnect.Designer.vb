<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fDbConnect
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbServer = New System.Windows.Forms.TextBox()
        Me.tbDatabase = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbPassword = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lvMain = New System.Windows.Forms.ListView()
        Me.id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.connection = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnNewConnection = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(712, 415)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Salva"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(491, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Server"
        '
        'tbServer
        '
        Me.tbServer.Location = New System.Drawing.Point(588, 12)
        Me.tbServer.Name = "tbServer"
        Me.tbServer.Size = New System.Drawing.Size(200, 20)
        Me.tbServer.TabIndex = 6
        '
        'tbDatabase
        '
        Me.tbDatabase.Location = New System.Drawing.Point(588, 38)
        Me.tbDatabase.Name = "tbDatabase"
        Me.tbDatabase.Size = New System.Drawing.Size(200, 20)
        Me.tbDatabase.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(491, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Database"
        '
        'tbUser
        '
        Me.tbUser.Location = New System.Drawing.Point(588, 64)
        Me.tbUser.Name = "tbUser"
        Me.tbUser.Size = New System.Drawing.Size(200, 20)
        Me.tbUser.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(491, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "User"
        '
        'tbPassword
        '
        Me.tbPassword.Location = New System.Drawing.Point(588, 90)
        Me.tbPassword.Name = "tbPassword"
        Me.tbPassword.Size = New System.Drawing.Size(200, 20)
        Me.tbPassword.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(491, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Password"
        '
        'lvMain
        '
        Me.lvMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.id, Me.connection})
        Me.lvMain.HideSelection = False
        Me.lvMain.Location = New System.Drawing.Point(12, 12)
        Me.lvMain.MultiSelect = False
        Me.lvMain.Name = "lvMain"
        Me.lvMain.Size = New System.Drawing.Size(473, 426)
        Me.lvMain.TabIndex = 13
        Me.lvMain.UseCompatibleStateImageBehavior = False
        Me.lvMain.View = System.Windows.Forms.View.Details
        '
        'id
        '
        Me.id.Text = "ID"
        '
        'connection
        '
        Me.connection.Text = "Connessione"
        Me.connection.Width = 400
        '
        'btnNewConnection
        '
        Me.btnNewConnection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewConnection.Location = New System.Drawing.Point(575, 415)
        Me.btnNewConnection.Name = "btnNewConnection"
        Me.btnNewConnection.Size = New System.Drawing.Size(131, 23)
        Me.btnNewConnection.TabIndex = 14
        Me.btnNewConnection.Text = "Nuova connessione"
        Me.btnNewConnection.UseVisualStyleBackColor = True
        '
        'fDbConnect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnNewConnection)
        Me.Controls.Add(Me.lvMain)
        Me.Controls.Add(Me.tbPassword)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbUser)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbDatabase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbServer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "fDbConnect"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tbServer As TextBox
    Friend WithEvents tbDatabase As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbUser As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents tbPassword As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lvMain As ListView
    Friend WithEvents id As ColumnHeader
    Friend WithEvents connection As ColumnHeader
    Friend WithEvents btnNewConnection As Button
End Class
