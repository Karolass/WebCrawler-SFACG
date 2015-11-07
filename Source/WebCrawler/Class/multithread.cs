using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;


namespace WebCrawler
{
    class multithread
    {
        public int index = 0;
        public string title_UI = "";
        public string title = "";
        public string chapter = "";
        public string url= "";
        public int progress = 0;
        public int status = 0;
        public string task = "";
        public string failed = "";
        public bool exe_flg = false;

        public void thread_exe()
        {
            exe_flg = true;
            status = 1;

            getHTMLbyWebRequest();
        }


        #region -----main

        private void getHTMLbyWebRequest()
        {
        RETRY:
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

                    List<string> HomePages = new List<string>();
                    List<string> Link = new List<string>();
                    List<string> Page = new List<string>();
                    FindJSlink(responseFromServer, ref HomePages);

                    if (HomePages.Count > 0)
                    {
                        FindTitle(responseFromServer);

                        title = RemoveIllegalPathCharacters(title);
                        title_UI = title + " - " + chapter;
                        //chapter = RemoveString(chapter);

                        for (int i = 0; i < HomePages.Count; i++)
                        {

                        RETRY2:
                            try
                            {
                                request = (HttpWebRequest)WebRequest.Create(HomePages[i]);
                                request.Timeout = 10000;
                                response = (HttpWebResponse)request.GetResponse();
                                if (response.StatusDescription.ToUpper() == "OK")
                                {
                                    dataStream = response.GetResponseStream();
                                    reader = new StreamReader(dataStream, encoding);
                                    responseFromServer = reader.ReadToEnd();

                                    FindLink(responseFromServer, ref Link, ref Page);
                                }
                            }
                            catch (Exception ex)
                            {
                                goto RETRY2;
                            }
                        }

                        //Request子連結
                        if (Link.Count > 0)
                        {
                            for (int j = 0; j < Link.Count; j++)
                            {
                                int to_count = 0;
                            RETRY3:
                                if (to_count >= 3)
                                {
                                    //更新失敗頁數
                                    if (!string.IsNullOrEmpty(failed))
                                        failed = string.Format("{0}, {1}", failed, j + 1);
                                    else
                                        failed = string.Format("{0}", j + 1);
                                    continue;
                                }
                                else
                                {
                                    task = string.Format("{0} / {1}", j + 1, Link.Count);
                                }
                                try
                                {
                                    if (SaveImg(Link[j], title, j + 1))
                                        throw new Exception();
                                    Thread.Sleep(1500);
                                    to_count = 0;
                                    //request = WebRequest.Create(Link[j]);
                                    //response = (HttpWebResponse)request.GetResponse();
                                    //if (response.StatusDescription.ToUpper() == "OK")
                                    //{
                                    //    dataStream = response.GetResponseStream();
                                    //    reader = new StreamReader(dataStream, encoding);
                                    //    responseFromServer = reader.ReadToEnd();

                                    //    string img_link = GetImagePath(responseFromServer); //解析img連結

                                    //    int page = Convert.ToInt16(Page[j]);

                                    //    if (SaveImg(img_link, title, page))
                                    //        throw new Exception();

                                    //    Thread.Sleep(500);
                                    //    to_count = 0;
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    to_count++;
                                    goto RETRY3;
                                }
                            }
                        }
                    }
                    else
                    {
                        status = 3;   //該網頁發生問題
                    }

                    if (status != 3)
                        status = 2;

                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                goto RETRY;
            }
        }

        private void FindJSlink(string html, ref List<string> HomePages)
        {
            //string pattern = @"<script\s*language=(""|')javascript(""|')\s*src=(""|')(?<href>/Utility/[\w]+/[\d]+.js)(""|')>";
            string pattern = @"<script\s*language=(""|')javascript(""|')\s*src=(""|')(?<href>/Utility/[/\w]+.js)(""|')>";
            MatchCollection mc = Regex.Matches(html, pattern);
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    if (HomePages.Contains(m.Groups["href"].Value))
                        continue;

                    //加入集合数组
                    HomePages.Add("http://comic.sfacg.com" + m.Groups["href"].Value);
                }
            }
        }

        private void FindLink(string html, ref List<string> Link, ref List<string> Page)
        {
            string pattern = @"(""|')(?<href>/Pic/.*?)(""|')";
            MatchCollection mc = Regex.Matches(html, pattern);
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    if (Link.Contains(m.Groups["href"].Value))
                        continue;

                    //加入集合数组
                    Link.Add("http://comic.sfacg.com" + m.Groups["href"].Value);
                }
            }
        }

        private void FindTitle(string html)
        {
            string pattern = @"<title>(?<title>[\2E80-\uFFFD_\s\w]+).*?-(?<chapter>[\2E80-\uFFFD_\w]+)-[\s\S]+</title>";
            MatchCollection mc = Regex.Matches(html, pattern);
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    title = m.Groups["title"].Value;
                    chapter = m.Groups["chapter"].Value;
                }
            }
        }

        private string[] GetAllImagePath(string html)
        {
            //var regex = new Regex("<img.*?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            var regex = new Regex("<img.*?id.*?=.*?[\"'](defualtPagePic)[\"'].*?src=[\"'](?<src>.*?)[\"'].*?>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(html);

            return (from Match match in matches select match.Groups["src"].Value).ToArray();
        }

        private string GetImagePath(string html)
        {
            var regex = new Regex("<img.*?id.*?=.*?[\"'](img)[\"'].*?src=[\"'](?<src>.*?)[\"'].*?>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(html);

            string temp = "";
            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    temp = m.Groups["src"].Value;
                }
            }

            return temp;
        }

        private bool SaveImg(string html, string title, int page)
        {
            bool failed = false;
            WebRequest request = WebRequest.Create(html);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            reader.ReadTimeout = 10000;  //加入timerout機制

            string fDir = System.IO.Path.Combine(Application.StartupPath, string.Format("{0}\\{1}\\", title.Trim(), chapter.Trim()));
            string fName = System.IO.Path.Combine(fDir, string.Format("{0:000}.jpg", page));
            if (!System.IO.Directory.Exists(fDir))
                System.IO.Directory.CreateDirectory(fDir);
            FileStream writer = new FileStream(fName, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[512];
            int c = 0; //實際讀取的位元組數
            decimal file_size = 0; //檔案大小

            try
            {
                while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                {
                    writer.Write(buff, 0, c);
                    file_size += c;
                }

                if (file_size < 1024 * 3)
                {
                    File.Delete(fName); //把403刪除
                    throw new Exception();
                }
            }
            catch
            {
                failed = true;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
                reader.Close();
                reader.Dispose();
                response.Close();

                //WebClient webClient = new WebClient();
                ////webClient.DownloadFileCompleted += Completed;
                ////webClient.DownloadProgressChanged += ProgressChanged;
                ////Dic_wc.Add(webClient, temp);
                //webClient.DownloadFile(new Uri(html), fName);
            }

            return failed;
        }

        private string ClearHtml(string text)//过滤html,js,css代码
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = Regex.Replace(text, "<head[^>]*>(?:.|[\r\n])*?</head>", "");
            text = Regex.Replace(text, "<script[^>]*>(?:.|[\r\n])*?</script>", "");
            text = Regex.Replace(text, "<style[^>]*>(?:.|[\r\n])*?</style>", "");

            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", ""); //<br> 
            text = Regex.Replace(text, "\\&[a-zA-Z]{1,10};", "");
            text = Regex.Replace(text, "<[^>]*>", "");

            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", ""); //&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty); //其它任何标记
            text = Regex.Replace(text, "[\\s]{2,}", " "); //两个或多个空格替换为一个

            text = text.Replace("'", "''");
            text = text.Replace("\r\n", "");
            text = text.Replace("  ", "");
            text = text.Replace("\t", "");
            return text.Trim();
        }
        #endregion

        private string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }

        private string RemoveString(string str)
        {
            var r = new Regex(@"[\u4E00-\u9FFF]+");
            return r.Replace(str, "");
        }
    }
}
