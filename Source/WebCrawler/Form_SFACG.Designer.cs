namespace WebCrawler
{
    partial class Form_SFACG
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SFACG));
            this.textbox_HTML = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_GO = new System.Windows.Forms.Button();
            this.label_status = new System.Windows.Forms.Label();
            this.label_wcount = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Thread_count = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_chapter_T = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_chapter_F = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textbox_HTML
            // 
            this.textbox_HTML.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.textbox_HTML.Location = new System.Drawing.Point(18, 87);
            this.textbox_HTML.Name = "textbox_HTML";
            this.textbox_HTML.Size = new System.Drawing.Size(837, 29);
            this.textbox_HTML.TabIndex = 0;
            this.textbox_HTML.Text = "http://comic.sfacg.com/HTML/SJZL/";
            this.textbox_HTML.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textbox_HTML_MouseClick);
            this.textbox_HTML.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_HTML_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "先打網址: \r\n(必須是每本的頁目錄)";
            // 
            // button_GO
            // 
            this.button_GO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_GO.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.button_GO.Location = new System.Drawing.Point(861, 76);
            this.button_GO.Name = "button_GO";
            this.button_GO.Size = new System.Drawing.Size(119, 48);
            this.button_GO.TabIndex = 2;
            this.button_GO.Text = "GOGOGO";
            this.button_GO.UseVisualStyleBackColor = true;
            this.button_GO.Click += new System.EventHandler(this.button_GO_Click);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.BackColor = System.Drawing.Color.Silver;
            this.label_status.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_status.Location = new System.Drawing.Point(261, 20);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(113, 40);
            this.label_status.TabIndex = 3;
            this.label_status.Text = "閒置中";
            this.label_status.Visible = false;
            // 
            // label_wcount
            // 
            this.label_wcount.AutoSize = true;
            this.label_wcount.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_wcount.Location = new System.Drawing.Point(380, 23);
            this.label_wcount.Name = "label_wcount";
            this.label_wcount.Size = new System.Drawing.Size(125, 24);
            this.label_wcount.TabIndex = 4;
            this.label_wcount.Text = "工作執行量: 0";
            this.label_wcount.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(776, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "同時執行數設定:";
            this.label2.Visible = false;
            // 
            // textBox_Thread_count
            // 
            this.textBox_Thread_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Thread_count.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.textBox_Thread_count.Location = new System.Drawing.Point(929, 12);
            this.textBox_Thread_count.MaxLength = 2;
            this.textBox_Thread_count.Name = "textBox_Thread_count";
            this.textBox_Thread_count.Size = new System.Drawing.Size(51, 29);
            this.textBox_Thread_count.TabIndex = 6;
            this.textBox_Thread_count.Text = "2";
            this.textBox_Thread_count.Visible = false;
            this.textBox_Thread_count.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Thread_count_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_chapter_T);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox_chapter_F);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox_Thread_count);
            this.panel1.Controls.Add(this.textbox_HTML);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_GO);
            this.panel1.Controls.Add(this.label_wcount);
            this.panel1.Controls.Add(this.label_status);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 201);
            this.panel1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(298, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(337, 48);
            this.label5.TabIndex = 11;
            this.label5.Text = "EX: 正篇100集、外傳2集、番外篇3集\r\n如果只抓外傳跟番外篇，請打101~105";
            // 
            // textBox_chapter_T
            // 
            this.textBox_chapter_T.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.textBox_chapter_T.Location = new System.Drawing.Point(209, 131);
            this.textBox_chapter_T.MaxLength = 3;
            this.textBox_chapter_T.Name = "textBox_chapter_T";
            this.textBox_chapter_T.Size = new System.Drawing.Size(63, 29);
            this.textBox_chapter_T.TabIndex = 10;
            this.textBox_chapter_T.Text = "999";
            this.textBox_chapter_T.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Thread_count_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(179, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "~";
            // 
            // textBox_chapter_F
            // 
            this.textBox_chapter_F.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.textBox_chapter_F.Location = new System.Drawing.Point(110, 131);
            this.textBox_chapter_F.MaxLength = 3;
            this.textBox_chapter_F.Name = "textBox_chapter_F";
            this.textBox_chapter_F.Size = new System.Drawing.Size(63, 29);
            this.textBox_chapter_F.TabIndex = 8;
            this.textBox_chapter_F.Text = "1";
            this.textBox_chapter_F.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Thread_count_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(14, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "集數起迄:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 201);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1008, 528);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // Form_SFACG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form_SFACG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C#漫畫爬蟲v1.1 for SFACG";
            this.Deactivate += new System.EventHandler(this.Form_SFACG_Deactivate);
            this.Load += new System.EventHandler(this.Form_SFACG_Load);
            this.SizeChanged += new System.EventHandler(this.Form_SFACG_SizeChanged);
            this.Activated += new System.EventHandler(this.Form_SFACG_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SFACG_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_HTML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_GO;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_wcount;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Thread_count;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_chapter_F;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_chapter_T;
        private System.Windows.Forms.Label label5;
    }
}

