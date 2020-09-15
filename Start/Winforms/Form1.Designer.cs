namespace Winforms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.loadingGIF = new System.Windows.Forms.PictureBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.pgCards = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.loadingGIF)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(117, 46);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 19);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // loadingGIF
            // 
            this.loadingGIF.Image = ((System.Drawing.Image)(resources.GetObject("loadingGIF.Image")));
            this.loadingGIF.Location = new System.Drawing.Point(117, 160);
            this.loadingGIF.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loadingGIF.Name = "loadingGIF";
            this.loadingGIF.Size = new System.Drawing.Size(212, 162);
            this.loadingGIF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingGIF.TabIndex = 1;
            this.loadingGIF.TabStop = false;
            this.loadingGIF.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(117, 102);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(76, 20);
            this.txtName.TabIndex = 2;
            // 
            // pgCards
            // 
            this.pgCards.Location = new System.Drawing.Point(117, 371);
            this.pgCards.Name = "pgCards";
            this.pgCards.Size = new System.Drawing.Size(212, 23);
            this.pgCards.TabIndex = 3;
            this.pgCards.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 528);
            this.Controls.Add(this.pgCards);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.loadingGIF);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.loadingGIF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox loadingGIF;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ProgressBar pgCards;
    }
}

