namespace prolab24
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
            this.components = new System.ComponentModel.Container();
            this.skorr = new System.Windows.Forms.Label();
            this.zamanlayicii = new System.Windows.Forms.Timer(this.components);
            this.karakter = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.karakter)).BeginInit();
            this.SuspendLayout();
            // 
            // skorr
            // 
            this.skorr.AutoSize = true;
            this.skorr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skorr.ForeColor = System.Drawing.Color.Black;
            this.skorr.Location = new System.Drawing.Point(13, 9);
            this.skorr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.skorr.Name = "skorr";
            this.skorr.Size = new System.Drawing.Size(94, 25);
            this.skorr.TabIndex = 0;
            this.skorr.Text = "Score: 0";
            // 
            // zamanlayicii
            // 
            this.zamanlayicii.Interval = 20;
            this.zamanlayicii.Tick += new System.EventHandler(this.Oyunn);
            // 
            // karakter
            // 
            this.karakter.Location = new System.Drawing.Point(41, 57);
            this.karakter.Margin = new System.Windows.Forms.Padding(4);
            this.karakter.Name = "karakter";
            this.karakter.Size = new System.Drawing.Size(73, 68);
            this.karakter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.karakter.TabIndex = 3;
            this.karakter.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(887, 495);
            this.Controls.Add(this.karakter);
            this.Controls.Add(this.skorr);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Prolab";
            ((System.ComponentModel.ISupportInitialize)(this.karakter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label skorr;
        private System.Windows.Forms.PictureBox karakter;
        private System.Windows.Forms.Timer zamanlayicii;
    }
}

