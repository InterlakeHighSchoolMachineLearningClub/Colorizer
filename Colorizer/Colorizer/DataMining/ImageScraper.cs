using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Colorizer.DataMining
{
    public static class ImageScraper
    {
        private static string search = "https://www.google.com/search?q=\"{0}\"&tbm=isch";

        public static IEnumerable<Image> RetrieveImages(params string[] keywords)
        {
            keywords.NullCheck();
            var webGet = new HtmlWeb();
            var document = webGet.Load(string.Format(ImageScraper.search, ImageScraper.ConcantenateKeywords(keywords)));
            var html = document.DocumentNode.InnerHtml;

            Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match m in linkParser.Matches(html).AsParallel())
            {
                var st = m.ToString();
                if (st.IndexOf(">") == -1 || st.IndexOf("(") == -1 || st.IndexOf("<") == -1 || st.IndexOf("\"") == -1)
                {
                    var k = WebTools.DownloadImage(m.ToString());
                    if (k != null)
                    {
                        yield return k;
                    }
                }
            }
        }
        private static string ConcantenateKeywords(string[] keywords)
        {
            StringBuilder builder = new StringBuilder(keywords.Length * 6);
            foreach (var item in keywords)
            {
                builder.Append(item);
                builder.Append("+");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
        private static IEnumerable<string> RetrieveURLs(string html)
        {
            XDocument doc = XDocument.Parse(html);
            var k = doc.Nodes();
            return null;
        }
    }
}
