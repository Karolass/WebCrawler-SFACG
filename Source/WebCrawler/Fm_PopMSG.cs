using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCrawler
{
    public partial class Fm_PopMSG : Form
    {
        int WorkingAreaWidth = Screen.PrimaryScreen.WorkingArea.Width;
        int WorkingAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;
        int now_time = 0;
        int exit_time = 5000;
        bool pop_ok = false;
        bool exit_flg = false;

        string _FilePath = "", _FileName = "", _Status = "";

        public Fm_PopMSG()
        {
            InitializeComponent();
        }

        public Fm_PopMSG(string sFileName, string sFilePath, string sStatus)
        {
            InitializeComponent();

            _FileName = sFileName;
            _FilePath = sFilePath;
            _Status = sStatus;
        }

        private void Fm_PopMSG_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;

            this.Location = new Point(WorkingAreaWidth - this.Width - 10, WorkingAreaHeight);
            this.BringToFront();
            this.TopMost = true;

            if (_Status == "2")
                _Status = "\r\n下載完成!!!";
            else
                _Status = "\r\n下載失敗!!!";

            label_MSG.Text = _FileName + _Status;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            int now_Y = this.Location.Y;

            if (!pop_ok)
            {
                if (now_Y > WorkingAreaHeight - this.Height - 10)
                    this.Location = new Point(WorkingAreaWidth - this.Width - 10, now_Y - 10);
                else
                    pop_ok = true;
            }
            else
            {
                if (now_time > exit_time)
                    exit_flg = true;

                if (exit_flg)
                    this.Location = new Point(WorkingAreaWidth - this.Width - 10, now_Y + 10);

                if (now_Y > WorkingAreaHeight)
                    this.Close();

                now_time += timer1.Interval;
                label2.Text = now_time.ToString();
            }

            timer1.Enabled = true;
        }

        private void Fm_PopMSG_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Fm_PopMSG_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void label_MSG_MouseEnter(object sender, EventArgs e)
        {
            label_MSG.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            timer1.Enabled = false;
        }

        private void label_MSG_MouseLeave(object sender, EventArgs e)
        {
            label_MSG.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            timer1.Enabled = true;
        }

        private void label_MSG_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "/select, " + _FilePath);
            this.Close();
        }
    }
}
