namespace NurseAlert
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtAlert = new System.Windows.Forms.TextBox();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.lbl0 = new System.Windows.Forms.Label();
            this.picFrame = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(16, 339);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(964, 86);
            this.txtOutput.TabIndex = 0;
            // 
            // txtAlert
            // 
            this.txtAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 150F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlert.ForeColor = System.Drawing.Color.Black;
            this.txtAlert.Location = new System.Drawing.Point(12, 43);
            this.txtAlert.Name = "txtAlert";
            this.txtAlert.ReadOnly = true;
            this.txtAlert.Size = new System.Drawing.Size(968, 290);
            this.txtAlert.TabIndex = 1;
            this.txtAlert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAlert.WordWrap = false;
            // 
            // txtRef
            // 
            this.txtRef.Location = new System.Drawing.Point(262, 12);
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(637, 22);
            this.txtRef.TabIndex = 7;
            this.txtRef.Text = "./a.mp4";
            // 
            // btnEnter
            // 
            this.btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Location = new System.Drawing.Point(905, 8);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 29);
            this.btnEnter.TabIndex = 6;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl0.Location = new System.Drawing.Point(12, 12);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(219, 20);
            this.lbl0.TabIndex = 5;
            this.lbl0.Text = "Enter path to image or video";
            // 
            // picFrame
            // 
            this.picFrame.Location = new System.Drawing.Point(986, 12);
            this.picFrame.Name = "picFrame";
            this.picFrame.Size = new System.Drawing.Size(597, 413);
            this.picFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFrame.TabIndex = 8;
            this.picFrame.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1595, 439);
            this.Controls.Add(this.picFrame);
            this.Controls.Add(this.txtRef);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.lbl0);
            this.Controls.Add(this.txtAlert);
            this.Controls.Add(this.txtOutput);
            this.Name = "Form1";
            this.Text = "Nurse Alert";
            ((System.ComponentModel.ISupportInitialize)(this.picFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtAlert;
        private System.Windows.Forms.TextBox txtRef;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label lbl0;
        private System.Windows.Forms.PictureBox picFrame;
    }
}

