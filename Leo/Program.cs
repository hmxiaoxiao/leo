using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Leo.Reg;
using HtmlAgilityPack;
using Leo.DB;
using Leo.GUI;

namespace Leo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new winMain());


            //string page = "http://www.sasac.gov.cn/n1180/n20240/n20259/index.html";
            //Console.WriteLine(page);

            //Contents c = Contents.Select()[0];
            //Contents.SavePage(c.ID, c.URL, c.NodeID, c.Title, c.CDate);

            //RunAnalyze();
            //Console.WriteLine("按回车键结束");
            //Console.ReadLine();
        }

        private static void DebugOut(object sender, Leo.Reg.Sasac.LinkEventArgs e)
        {
            Console.WriteLine(e.title);
        }

        private static void RunAnalyze()
        {
            Contents c = new Contents();
            Sasac web = new Sasac();

            web.FindedLink += c.SaveContents;
            web.FindedLink += DebugOut;

            web.UpdatePages();

            return;
        }
    }
}
