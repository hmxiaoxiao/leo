using System;
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;
using Leo.DB;

namespace Leo.Reg
{
    /// <summary>
    /// 国资委的分析
    /// </summary>
    public class Sasac
    {

        // 声明委托
        public delegate void FindedLinkEventHandle(Object sender, LinkEventArgs e);
        public event FindedLinkEventHandle FindedLink;      // 声明事件


        //  委托参数
        public class LinkEventArgs : EventArgs
        {
            public readonly string url;
            public readonly string title;
            public readonly string date;
            public readonly int parent_id;
            public LinkEventArgs(string url, string title, string date, int parent_id)
            {
                this.url = url;
                this.title = title;
                this.date = date;
                this.parent_id = parent_id;
            }
        }

        // 事件实现
        protected virtual void OnFinded(LinkEventArgs e)
        {
            if (FindedLink != null)
            {
                FindedLink(this, e);
            }
        }


        private const string WEB_ROOT = "http://www.sasac.gov.cn";        // 网站的首页

        /// <summary>
        /// 根据传入的网页地址，返回下一页的地址
        /// </summary>
        /// <param name="current_url">当前的网页地址</param>
        /// <returns></returns>
        public string getNextPage(string current_url)
        {
            // 如果网址为空，则直接返回
            if (string.IsNullOrEmpty(current_url))
                return "";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc;

            // 取得下页
            const string xpath_firstpage = "/html[1]/body[1]/table[2]/tr[1]/td[2]/table[4]/tr[1]/td[1]/table[1]/tr[1]/td[3]/table[2]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]/tr[1]/td[1]/a[1]";
            const string xpath = "/html[1]/body[1]/table[2]/tr[1]/td[2]/table[4]/tr[1]/td[1]/table[1]/tr[1]/td[3]/table[2]/tr[2]/td[1]/table[1]/tr[1]/td[2]/table[1]/tr[1]/td[1]/a[1]";

            string url = current_url;
            string next_page_url = "";


            // 如果没有域名，则加上域名
            if (url.Substring(0, 1) == "/")
                url = WEB_ROOT + url;

            try
            {
                //如果出现乱码，调整编码集为gb2312或者是utf-8
                //HtmlWeb.DefaultEncoding = System.Text.Encoding.GetEncoding(strEncode);
                doc = web.Load(url);
                HtmlNodeCollection firstpage = doc.DocumentNode.SelectNodes(xpath_firstpage);
                HtmlNodeCollection page = doc.DocumentNode.SelectNodes(xpath);


                if (firstpage != null && page == null && firstpage[0].InnerText == "下一页")
                        next_page_url =  firstpage[0].Attributes["href"].Value;
                
                if (firstpage != null && page != null)
                {
                    if (firstpage[0].InnerText == "下一页")
                        next_page_url = firstpage[0].Attributes["href"].Value;
                    else if (page[0].InnerText == "下一页")
                        next_page_url = page[0].Attributes["href"].Value;
                }

                return next_page_url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }


        /// <summary>
        /// 更新所有的页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePages(int id = 0)
        {
            
            List<Nodes> list;
            if(id > 0)
                list = Nodes.Select("id = " + id);
            else
                list = Nodes.Select();
            foreach (Nodes node in list)
            {
                getAllLinksOnPage(list[0].URL, list[0].ID);
            }
            return true;
        }


        /// <summary>
        /// 取得页面上所有的联接
        /// </summary>
        /// <param name="page_url"></param>
        /// <returns></returns>
        public bool getAllLinksOnPage(string page_url, int parent_id, bool SearchAll = false)
        {
            if (string.IsNullOrEmpty(page_url))
                return false;

            // 如果没有域名，则加上域名
            if (page_url.Substring(0, 1) == "/")
                page_url = WEB_ROOT + page_url;

            // 表格的XPath
            const string xpath = "/html[1]/body[1]/table[2]/tr[1]/td[2]/table[4]/tr[1]/td[1]/table[1]/tr[1]/td[3]/table[2]/tr[1]/td[1]/table[1]/tr";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(page_url);
            HtmlNodeCollection lists = doc.DocumentNode.SelectNodes(xpath);

            bool finded = true;
            foreach (HtmlNode hn in lists)
            {
                if (hn.SelectNodes("td[@class='black14']") != null)
                {
                    // 补全地址！
                    string url = hn.SelectNodes("td/a")[0].Attributes["href"].Value;
                    if (url.Substring(0, 1) == "/")
                        url = WEB_ROOT + url;

                    // 判断地址是否已经存在，如果不存在，那么至少要检查下一页的数据
                    if (Leo.DB.Contents.Select(string.Format("url = '{0}'", url)).Count == 0)
                        finded = false;
                    LinkEventArgs e = new LinkEventArgs(url,
                                                        hn.SelectNodes("td/a")[0].Attributes["title"].Value,
                                                        hn.SelectNodes("td")[1].InnerText,
                                                        parent_id);


                    OnFinded(e);
                    GetLinkContent(e.url);

                }
            }

            string next_page;

            // 是否要循环
            if (SearchAll)
            {
                next_page = this.getNextPage(page_url);
                if(!string.IsNullOrEmpty(next_page))
                    getAllLinksOnPage(next_page, parent_id, SearchAll);
            }
            else if (!finded)
            {
                next_page = this.getNextPage(page_url);
                if (!string.IsNullOrEmpty(next_page))
                    getAllLinksOnPage(next_page, parent_id, SearchAll);
            }
            return true;
        }


        /// <summary>
        /// 取得页面的内容
        /// </summary>
        /// <param name="page_url"></param>
        /// <returns></returns>
        public static string GetLinkContent(string page_url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(page_url);
            HtmlNodeCollection firstpage = doc.DocumentNode.SelectNodes("/html[1]/body[1]/table[2]/tr[1]/td[2]/table[11]/tr[1]/td[1]");
            if (firstpage != null && firstpage.Count >= 1)
            {
                return firstpage[0].InnerHtml;
            }
            return "";
        }
    }
}
