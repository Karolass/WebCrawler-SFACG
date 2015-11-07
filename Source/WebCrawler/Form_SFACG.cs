using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebCrawler
{
    public partial class Form_SFACG : Form
    {
        //for SFACG
        private Thread chk_Thread;
        private object @lock = new object();
        private bool _activate = true;
        private multithread[] mts = new multithread[2];
        private List<string[]> fin_MSG = new List<string[]>();

        private DataTable _dt = new DataTable();
                
        private bool _refresh = false;
        private string _LINK = "";
        private int _work_count = 0;
        private int _exe_limit = 4;

        public Form_SFACG()
        {
            InitializeComponent();
        }

        private void Form_SFACG_Load(object sender, EventArgs e)
        {
            Form_SFACG_SizeChanged(null, null);

            _dt.Columns.Add("index", typeof(decimal));
            _dt.Columns.Add("url");
            _dt.Columns.Add("title");
            _dt.Columns.Add("task"); 
            _dt.Columns.Add("failed");
            _dt.Columns.Add("status", typeof(decimal));
            dataGridView1.DataSource = _dt;
            dataGridView1.Columns[0].HeaderText = "序"; dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].HeaderText = "網址"; dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "任務名"; dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].HeaderText = "目前進度"; dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[4].HeaderText = "失敗頁數"; dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[5].HeaderText = "狀態"; dataGridView1.Columns[5].Visible = false;

            chk_Thread = new Thread(CheckWorkAndStatus);
            chk_Thread.IsBackground = true;
            chk_Thread.Start();            
        }

        private void Form_SFACG_Activated(object sender, EventArgs e)
        {
            _activate = true;
            FlashWindow.Stop(this);
        }

        private void Form_SFACG_Deactivate(object sender, EventArgs e)
        {
            _activate = false;
        }

        private void Form_SFACG_FormClosing(object sender, FormClosingEventArgs e)
        {
            chk_Thread.Abort();   //執行緒結束 避免UI關了還殘留
        }

        #region -----Thread

        private void CheckWorkAndStatus()
        {
            while (true)
            {
                string LINK = "";
                int now_exe = 0;
                DataTable dt = new DataTable();

                lock (@lock)
                {
                    LINK = _LINK;
                    _LINK = "";

                    if (_dt.Select("status = 0").Length > 0)
                        dt = _dt.Select("status = 0").CopyToDataTable();

                }
                //防止例外
                if (string.IsNullOrEmpty(textBox_Thread_count.Text))
                    _exe_limit = 2;
                else
                    _exe_limit = Convert.ToInt16(textBox_Thread_count.Text);

                //int status_0 = (int)_dt.Compute("Count(index)", "status = 0");
                //int status_1 = (int)_dt.Compute("Count(index)", "status = 1");
                //int status_2 = (int)_dt.Compute("Count(index)", "status = 2");

                //更新
                foreach (multithread mt in mts)
                {
                    if (mt == null) continue;
                    if (mt.exe_flg)
                    {
                        if (!_dt.Rows[mt.index - 1]["title"].ToString().Equals(mt.title_UI))
                        {
                            _dt.Rows[mt.index - 1]["title"] = mt.title_UI;
                            _refresh = true;
                        }
                        if (!_dt.Rows[mt.index - 1]["task"].ToString().Equals(mt.task))
                        {
                            _dt.Rows[mt.index - 1]["task"] = mt.task;
                            _refresh = true;
                        }
                        if (!_dt.Rows[mt.index - 1]["failed"].ToString().Equals(mt.failed))
                        {
                            _dt.Rows[mt.index - 1]["failed"] = mt.failed;
                            _refresh = true;
                        }
                        if (!_dt.Rows[mt.index - 1]["status"].ToString().Equals(mt.status))
                        {
                            _dt.Rows[mt.index - 1]["status"] = mt.status;
                            _refresh = true;
                        }

                        now_exe++;
                        if (mt.status >= 2)
                        {
                            mt.exe_flg = false;
                            now_exe--;
                        }
                    }
                }

                //啟動
                bool isExecute = false;
                if (dt.Rows.Count > 0 && now_exe < mts.Length)
                {
                    for (int j = 0; j < mts.Length; j++)
                    {
                        //避免迴圈太快 導致執行緒任務重複
                        //直接全找一遍 耗點效能
                        foreach (multithread mt in mts)
                        {
                            if (mt == null) continue;
                            if (mt.url.Equals(dt.Rows[0]["url"].ToString()))
                                isExecute = true;
                        } 

                        if (mts[j] == null)
                        {
                            if (isExecute)
                                break;

                            mts[j] = new multithread();
                            Thread th = new Thread(mts[j].thread_exe);
                            th.IsBackground = true;
                            mts[j].index = Convert.ToInt16(dt.Rows[0]["index"]);
                            mts[j].url = dt.Rows[0]["url"].ToString();
                            mts[j].exe_flg = true;
                            th.Start();

                            break;
                        }
                        else
                        {           
                            if (!mts[j].exe_flg && !isExecute)
                            {
                                mts[j] = new multithread();
                                Thread th = new Thread(mts[j].thread_exe);
                                th.IsBackground = true;
                                mts[j].index = Convert.ToInt16(dt.Rows[0]["index"]);
                                mts[j].url = dt.Rows[0]["url"].ToString();
                                mts[j].exe_flg = true;
                                th.Start();

                                break;
                            }
                        }
                    }
                }

                //新增Datatable
                if (!string.IsNullOrEmpty(LINK))
                {
                    List<string> LIST = GetChapterLink(LINK);

                    if (LIST.Count > 0)
                    {
                        int chapter_F = Convert.ToInt16(textBox_chapter_F.Text.Trim());
                        int chapter_T = Convert.ToInt16(textBox_chapter_T.Text.Trim());
                        for (int i = 0; i < LIST.Count; i++)
                        {
                            if (i + 1 >= chapter_F && i + 1 <= chapter_T)
                            {
                                ////新增任務顯示
                                //int index = _dt.Rows.Count + 1;
                                //DataRow rw = _dt.NewRow();
                                //rw["index"] = index;
                                //rw["url"] = LIST[i];
                                //rw["status"] = 0;
                                //_dt.Rows.Add(rw);

                                //datagridview對跨執行緒新增資料會有hang住問題 需改成委派
                                dataGridView1.BeginInvoke(new ModifyTable(AddRow), i, LIST[i]);
                            }
                        }
                    }

                    LINK = "";
                }

                //_work_count = status_1;

                Thread.Sleep(1000);
            }
        }

        delegate void ModifyTable(int i, string url);

        private void AddRow(int i, string url)
        {
            // Do your update work here, just anything you want.
            DataRow rw = _dt.NewRow();
            rw["index"] = _dt.Rows.Count + 1; ;
            rw["url"] = url;
            rw["status"] = 0;
            _dt.Rows.Add(rw);
        }

        #endregion

        private List<string> GetChapterLink(string url)
        {
            TEST:
            try
            {
                Encoding encoding = System.Text.Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription.ToUpper() == "OK")
                {
                    switch (response.CharacterSet.ToLower())
                    {
                        case "gbk":
                            encoding = Encoding.GetEncoding("GBK");//貌似用GB2312就可以
                            break;
                        case "gb2312":
                            encoding = Encoding.GetEncoding("GB2312");
                            break;
                        case "utf-8":
                            encoding = Encoding.UTF8;
                            break;
                        case "big5":
                            encoding = Encoding.GetEncoding("Big5");
                            break;
                        case "iso-8859-1":
                            encoding = Encoding.UTF8;//ISO-8859-1的編碼用UTF-8處理，致少優酷的是這種方法沒有亂碼
                            break;
                        default:
                            encoding = Encoding.UTF8;//如果分析不出來就用的UTF-8
                            break;
                    }

                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream, encoding);
                    string responseFromServer = reader.ReadToEnd();

                    List<string> Chapter = new List<string>();
                    string pattern = @"<li><a\s*href=(""|')(?<href>/HTML/.*?)(""|')\s*target=(""|')_blank(""|')>";
                    MatchCollection mc = Regex.Matches(responseFromServer, pattern);
                    foreach (Match m in mc)
                    {
                        if (m.Success)
                        {
                            if (Chapter.Contains("http://comic.sfacg.com" + m.Groups["href"].Value))
                                continue;

                            //加入集合数组
                            Chapter.Add("http://comic.sfacg.com" + m.Groups["href"].Value);
                        }
                    }
                    return Chapter.OrderBy(list => list).ToList();
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                goto TEST;
            }

            return null;
        }

        #region -------GUI Event
        
        private void button_GO_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //lock (@lock)
            //{
            //    if (_dt.Rows.Count > 0)
            //    {
            //        if (_dt.Select(string.Format("url = '{0}'", textbox_HTML.Text.Trim())).Length > 0)
            //        {
            //            if (MessageBox.Show("該網址已存在，是否重複下載?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            //                return;
            //        }
            //    }

            //    //新增任務顯示
            //    int index = _dt.Rows.Count + 1;
            //    DataRow rw = _dt.NewRow();
            //    rw["index"] = index;
            //    rw["url"] = textbox_HTML.Text.Trim();
            //    rw["status"] = 0;
            //    _dt.Rows.Add(rw);
            //}
            if (string.IsNullOrEmpty(textBox_chapter_F.Text) || string.IsNullOrEmpty(textBox_chapter_T.Text))
            {
                MessageBox.Show("集數起迄不能為空!");
                return;
            }

            lock (@lock)
            {
                _LINK = textbox_HTML.Text.Trim();
            }

            textbox_HTML.Focus();
            textbox_HTML.SelectAll();
            Cursor.Current = Cursors.Default;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            string temp = Clipboard.GetText(TextDataFormat.Text);
            string pattern = @".*?comic.sfacg.com/HTML/[\w]+";
            MatchCollection mc = Regex.Matches(temp, pattern);
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    if (!textbox_HTML.Text.Trim().Equals(m.Groups[0].Value))
                    {
                        textbox_HTML.Text = m.Groups[0].Value;
                        //button_GO_Click(null, null);
                        Clipboard.Clear();  //執行完清除
                    }
                }
            }

            label_wcount.Text = string.Format("工作執行量: {0}", _work_count);
            if (_work_count > 0)
            {
                label_status.Text = "執行中";
                label_status.BackColor = Color.Lime;
            }
            else
            {
                label_status.Text = "閒置中";
                label_status.BackColor = Color.Silver;
            } 

            if (fin_MSG.Count > 0)
            {
                if (!_activate)
                    FlashWindow.Flash(this, 5); //閃爍通知

                Fm_PopMSG fm = new Fm_PopMSG(fin_MSG[0][0], fin_MSG[0][1], fin_MSG[0][2]);
                fm.Show();
                fin_MSG.RemoveAt(0);
            }

            //dataGridView1.Refresh();
            //BestFitCol();
            if (_refresh)
            {
                dataGridView1.Refresh();
                BestFitCol();
                _refresh = false;
            }

            timer1.Enabled = true;
        }

        private void textBox_Thread_count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void textbox_HTML_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button_GO_Click(null, null);
            }
        }

        private void textbox_HTML_MouseClick(object sender, MouseEventArgs e)
        {
            textbox_HTML.SelectAll();
        }

        private void Form_SFACG_SizeChanged(object sender, EventArgs e)
        {
            textbox_HTML.Width = button_GO.Left - 24;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dataGridView1.Rows.Count <= e.RowIndex) return;
            if (e.RowIndex < 0) return;
            if (dataGridView1.Rows[e.RowIndex].Cells["status"].Value == null) return;
            if (dataGridView1.Rows[e.RowIndex].Cells["status"].Value.ToString() == "1")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.Lime;
                //if (row.Selected)
                //{
                //    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                //    row.DefaultCellStyle.SelectionForeColor = Color.Red;
                //}
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells["status"].Value.ToString() == "2")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells["status"].Value.ToString() == "3")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.Red;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0)
                return;

            if (dataGridView1.CurrentRow.Index < 0)
                return;

            string url = dataGridView1.CurrentRow.Cells["url"].Value.ToString();

            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch { }
        }

        #endregion

        private void BestFitCol()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                int temp = 0;
                if (col.DataPropertyName != "title")
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                else
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                temp = col.Width;

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Width = temp;
            }
        }
    }
}
