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
            //if (SpecAn is SpecAn sa) sa.Dispose();
            if (SigGen1 is SigGen sg) sg.Dispose();

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
            this.BtnTestESGError = new System.Windows.Forms.Button();
            this.BtnTestU200X = new System.Windows.Forms.Button();
            this.BtnCalibrateU200X = new System.Windows.Forms.Button();
            this.BtnCalibrateSigGen1 = new System.Windows.Forms.Button();
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
            // BtnTestESGError
            // 
            this.BtnTestESGError.Location = new System.Drawing.Point(190, 48);
            this.BtnTestESGError.Name = "BtnTestESGError";
            this.BtnTestESGError.Size = new System.Drawing.Size(116, 23);
            this.BtnTestESGError.TabIndex = 3;
            this.BtnTestESGError.Text = "Test ESG Error";
            this.BtnTestESGError.UseVisualStyleBackColor = true;
            this.BtnTestESGError.Click += new System.EventHandler(this.BtnTestESGError_Click);
            // 
            // BtnTestU200X
            // 
            this.BtnTestU200X.Location = new System.Drawing.Point(345, 48);
            this.BtnTestU200X.Name = "BtnTestU200X";
            this.BtnTestU200X.Size = new System.Drawing.Size(116, 23);
            this.BtnTestU200X.TabIndex = 4;
            this.BtnTestU200X.Text = "Test U200X";
            this.BtnTestU200X.UseVisualStyleBackColor = true;
            this.BtnTestU200X.Click += new System.EventHandler(this.BtnTestU200X_Click);
            // 
            // BtnCalibrateU200X
            // 
            this.BtnCalibrateU200X.Location = new System.Drawing.Point(491, 48);
            this.BtnCalibrateU200X.Name = "BtnCalibrateU200X";
            this.BtnCalibrateU200X.Size = new System.Drawing.Size(116, 23);
            this.BtnCalibrateU200X.TabIndex = 5;
            this.BtnCalibrateU200X.Text = "Calibrate U200X";
            this.BtnCalibrateU200X.UseVisualStyleBackColor = true;
            this.BtnCalibrateU200X.Click += new System.EventHandler(this.BtnCalibrateU200X_Click);
            // 
            // BtnCalibrateSigGen1
            // 
            this.BtnCalibrateSigGen1.Location = new System.Drawing.Point(190, 91);
            this.BtnCalibrateSigGen1.Name = "BtnCalibrateSigGen1";
            this.BtnCalibrateSigGen1.Size = new System.Drawing.Size(116, 23);
            this.BtnCalibrateSigGen1.TabIndex = 6;
            this.BtnCalibrateSigGen1.Text = "Calibrate ESG";
            this.BtnCalibrateSigGen1.UseVisualStyleBackColor = true;
            this.BtnCalibrateSigGen1.Click += new System.EventHandler(this.BtnCalibrateSigGen1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnCalibrateSigGen1);
            this.Controls.Add(this.BtnCalibrateU200X);
            this.Controls.Add(this.BtnTestU200X);
            this.Controls.Add(this.BtnTestESGError);
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
        private System.Windows.Forms.Button BtnTestESGError;
        private System.Windows.Forms.Button BtnTestU200X;
        private System.Windows.Forms.Button BtnCalibrateU200X;
        private System.Windows.Forms.Button BtnCalibrateSigGen1;
    }
}

