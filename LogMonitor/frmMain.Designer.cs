namespace LogMonitor
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLogName = new System.Windows.Forms.TextBox();
            this.radioButtonPx = new System.Windows.Forms.RadioButton();
            this.radioButtonTpv = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonWsServicePayment = new System.Windows.Forms.RadioButton();
            this.radioButtonWsTrx = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.bkGrdWkReadLog = new System.ComponentModel.BackgroundWorker();
            this.timerEach = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre del Log:  ";
            // 
            // textBoxLogName
            // 
            this.textBoxLogName.Location = new System.Drawing.Point(164, 18);
            this.textBoxLogName.Name = "textBoxLogName";
            this.textBoxLogName.Size = new System.Drawing.Size(100, 23);
            this.textBoxLogName.TabIndex = 1;
            // 
            // radioButtonPx
            // 
            this.radioButtonPx.AutoSize = true;
            this.radioButtonPx.Location = new System.Drawing.Point(23, 23);
            this.radioButtonPx.Name = "radioButtonPx";
            this.radioButtonPx.Size = new System.Drawing.Size(38, 19);
            this.radioButtonPx.TabIndex = 2;
            this.radioButtonPx.TabStop = true;
            this.radioButtonPx.Text = "Px";
            this.radioButtonPx.UseVisualStyleBackColor = true;
            // 
            // radioButtonTpv
            // 
            this.radioButtonTpv.AutoSize = true;
            this.radioButtonTpv.Location = new System.Drawing.Point(23, 59);
            this.radioButtonTpv.Name = "radioButtonTpv";
            this.radioButtonTpv.Size = new System.Drawing.Size(43, 19);
            this.radioButtonTpv.TabIndex = 3;
            this.radioButtonTpv.TabStop = true;
            this.radioButtonTpv.Text = "Tpv";
            this.radioButtonTpv.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonWsServicePayment);
            this.groupBox1.Controls.Add(this.radioButtonWsTrx);
            this.groupBox1.Controls.Add(this.radioButtonTpv);
            this.groupBox1.Controls.Add(this.radioButtonPx);
            this.groupBox1.Location = new System.Drawing.Point(38, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 178);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo del Log";
            // 
            // radioButtonWsServicePayment
            // 
            this.radioButtonWsServicePayment.AutoSize = true;
            this.radioButtonWsServicePayment.Location = new System.Drawing.Point(23, 135);
            this.radioButtonWsServicePayment.Name = "radioButtonWsServicePayment";
            this.radioButtonWsServicePayment.Size = new System.Drawing.Size(114, 19);
            this.radioButtonWsServicePayment.TabIndex = 5;
            this.radioButtonWsServicePayment.TabStop = true;
            this.radioButtonWsServicePayment.Text = "WsPagoServicios";
            this.radioButtonWsServicePayment.UseVisualStyleBackColor = true;
            // 
            // radioButtonWsTrx
            // 
            this.radioButtonWsTrx.AutoSize = true;
            this.radioButtonWsTrx.Location = new System.Drawing.Point(23, 99);
            this.radioButtonWsTrx.Name = "radioButtonWsTrx";
            this.radioButtonWsTrx.Size = new System.Drawing.Size(56, 19);
            this.radioButtonWsTrx.TabIndex = 4;
            this.radioButtonWsTrx.TabStop = true;
            this.radioButtonWsTrx.Text = "WsTrx";
            this.radioButtonWsTrx.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(113, 265);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Iniciar";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // bkGrdWkReadLog
            // 
            this.bkGrdWkReadLog.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkGrdWkReadLog_DoWork);
            this.bkGrdWkReadLog.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BkGrdWkReadLog_RunWorkerCompleted);
            // 
            // timerEach
            // 
            this.timerEach.Tick += new System.EventHandler(this.TimerEach_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 325);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxLogName);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textBoxLogName;
        private RadioButton radioButtonPx;
        private RadioButton radioButtonTpv;
        private GroupBox groupBox1;
        private RadioButton radioButtonWsServicePayment;
        private RadioButton radioButtonWsTrx;
        private Button buttonStart;
        private System.ComponentModel.BackgroundWorker bkGrdWkReadLog;
        private System.Windows.Forms.Timer timerEach;
    }
}