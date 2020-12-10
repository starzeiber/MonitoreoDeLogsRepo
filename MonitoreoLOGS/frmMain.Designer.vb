<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.myTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_NombreLog = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker_Validacion = New System.ComponentModel.BackgroundWorker()
        Me.Button_Iniciar = New System.Windows.Forms.Button()
        Me.RadioButton_Px = New System.Windows.Forms.RadioButton()
        Me.RadioButton_TPV = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Ws = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'myTimer
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Log a monitorear"
        '
        'TextBox_NombreLog
        '
        Me.TextBox_NombreLog.Location = New System.Drawing.Point(104, 16)
        Me.TextBox_NombreLog.Name = "TextBox_NombreLog"
        Me.TextBox_NombreLog.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_NombreLog.TabIndex = 1
        '
        'BackgroundWorker_Validacion
        '
        '
        'Button_Iniciar
        '
        Me.Button_Iniciar.Location = New System.Drawing.Point(431, 12)
        Me.Button_Iniciar.Name = "Button_Iniciar"
        Me.Button_Iniciar.Size = New System.Drawing.Size(75, 23)
        Me.Button_Iniciar.TabIndex = 2
        Me.Button_Iniciar.Text = "Iniciar"
        Me.Button_Iniciar.UseVisualStyleBackColor = True
        '
        'RadioButton_Px
        '
        Me.RadioButton_Px.AutoSize = True
        Me.RadioButton_Px.Checked = True
        Me.RadioButton_Px.Location = New System.Drawing.Point(229, 16)
        Me.RadioButton_Px.Name = "RadioButton_Px"
        Me.RadioButton_Px.Size = New System.Drawing.Size(39, 17)
        Me.RadioButton_Px.TabIndex = 3
        Me.RadioButton_Px.TabStop = True
        Me.RadioButton_Px.Text = "PX"
        Me.RadioButton_Px.UseVisualStyleBackColor = True
        '
        'RadioButton_TPV
        '
        Me.RadioButton_TPV.AutoSize = True
        Me.RadioButton_TPV.Location = New System.Drawing.Point(289, 16)
        Me.RadioButton_TPV.Name = "RadioButton_TPV"
        Me.RadioButton_TPV.Size = New System.Drawing.Size(46, 17)
        Me.RadioButton_TPV.TabIndex = 4
        Me.RadioButton_TPV.TabStop = True
        Me.RadioButton_TPV.Text = "TPV"
        Me.RadioButton_TPV.UseVisualStyleBackColor = True
        '
        'RadioButton_Ws
        '
        Me.RadioButton_Ws.AutoSize = True
        Me.RadioButton_Ws.Location = New System.Drawing.Point(353, 15)
        Me.RadioButton_Ws.Name = "RadioButton_Ws"
        Me.RadioButton_Ws.Size = New System.Drawing.Size(43, 17)
        Me.RadioButton_Ws.TabIndex = 5
        Me.RadioButton_Ws.TabStop = True
        Me.RadioButton_Ws.Text = "WS"
        Me.RadioButton_Ws.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 68)
        Me.Controls.Add(Me.RadioButton_Ws)
        Me.Controls.Add(Me.RadioButton_TPV)
        Me.Controls.Add(Me.RadioButton_Px)
        Me.Controls.Add(Me.Button_Iniciar)
        Me.Controls.Add(Me.TextBox_NombreLog)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMain"
        Me.Text = "Monitoreo de logs"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents myTimer As System.Windows.Forms.Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_NombreLog As TextBox
    Friend WithEvents BackgroundWorker_Validacion As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button_Iniciar As Button
    Friend WithEvents RadioButton_Px As RadioButton
    Friend WithEvents RadioButton_TPV As RadioButton
    Friend WithEvents RadioButton_Ws As RadioButton
End Class
