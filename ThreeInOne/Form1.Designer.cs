namespace SetupSTEL
{
    partial class frmMain
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
            this.btnRefreshPort = new System.Windows.Forms.Button();
            this.cmb3in1 = new System.Windows.Forms.ComboBox();
            this.lbl3in1 = new System.Windows.Forms.Label();
            this.btnTest3in1MCR = new System.Windows.Forms.Button();
            this.btnTest3in1PIN = new System.Windows.Forms.Button();
            this.btnTest3in1ID = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefreshPort
            // 
            this.btnRefreshPort.Location = new System.Drawing.Point(18, 12);
            this.btnRefreshPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefreshPort.Name = "btnRefreshPort";
            this.btnRefreshPort.Size = new System.Drawing.Size(112, 35);
            this.btnRefreshPort.TabIndex = 2;
            this.btnRefreshPort.Text = "&Auto Config";
            this.btnRefreshPort.UseVisualStyleBackColor = true;
            this.btnRefreshPort.Click += new System.EventHandler(this.btnRefreshPort_Click);
            // 
            // cmb3in1
            // 
            this.cmb3in1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb3in1.FormattingEnabled = true;
            this.cmb3in1.Location = new System.Drawing.Point(458, 112);
            this.cmb3in1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb3in1.Name = "cmb3in1";
            this.cmb3in1.Size = new System.Drawing.Size(97, 28);
            this.cmb3in1.TabIndex = 7;
            this.cmb3in1.SelectedIndexChanged += new System.EventHandler(this.cmb3in1_SelectedIndexChanged);
            // 
            // lbl3in1
            // 
            this.lbl3in1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl3in1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbl3in1.Location = new System.Drawing.Point(19, 52);
            this.lbl3in1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl3in1.Name = "lbl3in1";
            this.lbl3in1.Size = new System.Drawing.Size(830, 149);
            this.lbl3in1.TabIndex = 6;
            this.lbl3in1.Text = "Feitian PinPad 3 in 1";
            this.lbl3in1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl3in1.Click += new System.EventHandler(this.lbl3in1_Click);
            // 
            // btnTest3in1MCR
            // 
            this.btnTest3in1MCR.Enabled = false;
            this.btnTest3in1MCR.Location = new System.Drawing.Point(596, 63);
            this.btnTest3in1MCR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTest3in1MCR.Name = "btnTest3in1MCR";
            this.btnTest3in1MCR.Size = new System.Drawing.Size(225, 35);
            this.btnTest3in1MCR.TabIndex = 9;
            this.btnTest3in1MCR.Text = "MCR";
            this.btnTest3in1MCR.UseVisualStyleBackColor = true;
            this.btnTest3in1MCR.Click += new System.EventHandler(this.btnTest3in1MCR_Click);
            // 
            // btnTest3in1PIN
            // 
            this.btnTest3in1PIN.Enabled = false;
            this.btnTest3in1PIN.Location = new System.Drawing.Point(596, 108);
            this.btnTest3in1PIN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTest3in1PIN.Name = "btnTest3in1PIN";
            this.btnTest3in1PIN.Size = new System.Drawing.Size(225, 35);
            this.btnTest3in1PIN.TabIndex = 10;
            this.btnTest3in1PIN.Text = "PIN Pad";
            this.btnTest3in1PIN.UseVisualStyleBackColor = true;
            this.btnTest3in1PIN.Click += new System.EventHandler(this.btnTest3in1PIN_Click);
            // 
            // btnTest3in1ID
            // 
            this.btnTest3in1ID.Enabled = false;
            this.btnTest3in1ID.Location = new System.Drawing.Point(596, 153);
            this.btnTest3in1ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTest3in1ID.Name = "btnTest3in1ID";
            this.btnTest3in1ID.Size = new System.Drawing.Size(225, 35);
            this.btnTest3in1ID.TabIndex = 11;
            this.btnTest3in1ID.Text = "ID Card";
            this.btnTest3in1ID.UseVisualStyleBackColor = true;
            this.btnTest3in1ID.Click += new System.EventHandler(this.btnTest3in1ID_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Location = new System.Drawing.Point(18, 229);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(831, 528);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(14, 27);
            this.lblStatus.Multiline = true;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblStatus.Size = new System.Drawing.Size(804, 477);
            this.lblStatus.TabIndex = 23;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 929);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTest3in1ID);
            this.Controls.Add(this.btnTest3in1PIN);
            this.Controls.Add(this.btnTest3in1MCR);
            this.Controls.Add(this.cmb3in1);
            this.Controls.Add(this.lbl3in1);
            this.Controls.Add(this.btnRefreshPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(889, 985);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3in1 Demo Program";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefreshPort;
        private System.Windows.Forms.ComboBox cmb3in1;
        private System.Windows.Forms.Label lbl3in1;
        private System.Windows.Forms.Button btnTest3in1MCR;
        private System.Windows.Forms.Button btnTest3in1PIN;
        private System.Windows.Forms.Button btnTest3in1ID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox lblStatus;
    }
}

