namespace RL_Decrypt_Extract
{
    partial class Form1
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
            this.btnRun = new System.Windows.Forms.Button();
            this.btnDeleteEncrypted = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(27, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(101, 60);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnDeleteEncrypted
            // 
            this.btnDeleteEncrypted.Location = new System.Drawing.Point(27, 78);
            this.btnDeleteEncrypted.Name = "btnDeleteEncrypted";
            this.btnDeleteEncrypted.Size = new System.Drawing.Size(101, 23);
            this.btnDeleteEncrypted.TabIndex = 1;
            this.btnDeleteEncrypted.Text = "Delete Encrypted";
            this.btnDeleteEncrypted.UseVisualStyleBackColor = true;
            this.btnDeleteEncrypted.Click += new System.EventHandler(this.btnDeleteEncrypted_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 113);
            this.Controls.Add(this.btnDeleteEncrypted);
            this.Controls.Add(this.btnRun);
            this.Name = "Form1";
            this.Text = "RL Decrypter + Extractor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnDeleteEncrypted;
    }
}

