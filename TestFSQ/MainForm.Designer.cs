namespace TestFSQ
{
    partial class MainForm
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
            this.BtnTestFindResources = new System.Windows.Forms.Button();
            this.BtnTestFSQ = new System.Windows.Forms.Button();
            this.BtnAutoFindFSQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnTestFindResources
            // 
            this.BtnTestFindResources.Location = new System.Drawing.Point(38, 49);
            this.BtnTestFindResources.Name = "BtnTestFindResources";
            this.BtnTestFindResources.Size = new System.Drawing.Size(106, 23);
            this.BtnTestFindResources.TabIndex = 0;
            this.BtnTestFindResources.Text = "Find Resources";
            this.BtnTestFindResources.UseVisualStyleBackColor = true;
            this.BtnTestFindResources.Click += new System.EventHandler(this.BtnTestFindResources_Click);
            // 
            // BtnTestFSQ
            // 
            this.BtnTestFSQ.Location = new System.Drawing.Point(38, 91);
            this.BtnTestFSQ.Name = "BtnTestFSQ";
            this.BtnTestFSQ.Size = new System.Drawing.Size(106, 23);
            this.BtnTestFSQ.TabIndex = 1;
            this.BtnTestFSQ.Text = "Test FSQ";
            this.BtnTestFSQ.UseVisualStyleBackColor = true;
            this.BtnTestFSQ.Click += new System.EventHandler(this.BtnTestFSQ_Click);
            // 
            // BtnAutoFindFSQ
            // 
            this.BtnAutoFindFSQ.Location = new System.Drawing.Point(38, 144);
            this.BtnAutoFindFSQ.Name = "BtnAutoFindFSQ";
            this.BtnAutoFindFSQ.Size = new System.Drawing.Size(106, 23);
            this.BtnAutoFindFSQ.TabIndex = 2;
            this.BtnAutoFindFSQ.Text = "Auto Find FSQ";
            this.BtnAutoFindFSQ.UseVisualStyleBackColor = true;
            this.BtnAutoFindFSQ.Click += new System.EventHandler(this.BtnAutoFindFSQ_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnAutoFindFSQ);
            this.Controls.Add(this.BtnTestFSQ);
            this.Controls.Add(this.BtnTestFindResources);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnTestFindResources;
        private System.Windows.Forms.Button BtnTestFSQ;
        private System.Windows.Forms.Button BtnAutoFindFSQ;
    }
}

