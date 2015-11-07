namespace WebCrawler
{
    partial class Fm_PopMSG
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
            this.label_MSG = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_MSG
            // 
            this.label_MSG.AutoEllipsis = true;
            this.label_MSG.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_MSG.Location = new System.Drawing.Point(1, -1);
            this.label_MSG.Name = "label_MSG";
            this.label_MSG.Size = new System.Drawing.Size(292, 115);
            this.label_MSG.TabIndex = 2;
            this.label_MSG.Text = "[訊息]";
            this.label_MSG.MouseLeave += new System.EventHandler(this.label_MSG_MouseLeave);
            this.label_MSG.Click += new System.EventHandler(this.label_MSG_Click);
            this.label_MSG.MouseEnter += new System.EventHandler(this.label_MSG_MouseEnter);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F);
            this.label2.Location = new System.Drawing.Point(190, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "0";
            this.label2.Visible = false;
            // 
            // Fm_PopMSG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 115);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_MSG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 29);
            this.Name = "Fm_PopMSG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.Fm_PopMSG_Load);
            this.MouseEnter += new System.EventHandler(this.Fm_PopMSG_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Fm_PopMSG_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_MSG;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
    }
}