namespace CoreBank.Common.AddIn.Forms
{
    partial class Status
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbDetails = new System.Windows.Forms.TextBox();
            this.tbShortMessage = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // tbDetails
            // 
            this.tbDetails.Location = new System.Drawing.Point(27, 81);
            this.tbDetails.Multiline = true;
            this.tbDetails.Name = "tbDetails";
            this.tbDetails.Size = new System.Drawing.Size(465, 112);
            this.tbDetails.TabIndex = 2;
            // 
            // tbShortMessage
            // 
            this.tbShortMessage.Location = new System.Drawing.Point(108, 20);
            this.tbShortMessage.Name = "tbShortMessage";
            this.tbShortMessage.Size = new System.Drawing.Size(384, 20);
            this.tbShortMessage.TabIndex = 3;
            this.tbShortMessage.TextChanged += new System.EventHandler(this.tbShortMessage_TextChanged);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(27, 20);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(62, 20);
            this.tbResult.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(417, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // bw
            // 
            this.bw.WorkerReportsProgress = true;
            this.bw.WorkerSupportsCancellation = true;
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // pb
            // 
            this.pb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pb.Location = new System.Drawing.Point(27, 55);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(465, 10);
            this.pb.TabIndex = 6;
            // 
            // Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 251);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbShortMessage);
            this.Controls.Add(this.tbDetails);
            this.Controls.Add(this.label1);
            this.Name = "Status";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
      

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDetails;
        private System.Windows.Forms.TextBox tbShortMessage;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnCancel;
        public System.ComponentModel.BackgroundWorker bw;
        public System.Windows.Forms.ProgressBar pb;
    }
}