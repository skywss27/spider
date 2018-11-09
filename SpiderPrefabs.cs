using System.Collections.Generic;
using System.Linq;

namespace EasySpider
{
    public static class SpiderPrefabs
    {
        public static Spider icbcSpider = new Spider("http://www.icbc.com.cn/ICBC/%E5%AE%A2%E6%88%B7%E6%9C%8D%E5%8A%A1/%E7%83%AD%E7%82%B9%E9%97%AE%E7%AD%94/default.htm")
        {
            ThreadsNum = 16,
            Downloader = new HTMLDownloader
            {
                TimeOut = 10000,
            },
            UrlsMng = new UrlsManager
            {
                CrawDepth = 8,
                Bloom = new BloomFilter(100000, 4),
            },
            Parser = new HTMLParser
            {
                URLRegexFilter = new[] { @"http://www.icbc.com.cn/icbc/%E5%AE%A2%E6%88%B7%E6%9C%8D%E5%8A%A1/%E7%83%AD%E7%82%B9%E9%97%AE%E7%AD%94/([a-zA-Z0-9\-\?\,\'\\\+&amp;%\$#_]*)(\/)([a-zA-Z0-9\-\?\,\'\\\+&amp;%\$#_]*)\.htm" },
                EscapeWords = new[] { "注册", "网银指南", "呵呵" },
                URLSdantarlize = u =>
                {
                    if (!u.Contains("www.icbc.com.cn"))
                        u = "http://www.icbc.com.cn" + u;
                    return u;
                },
                ContentSelector = hd =>
                {
                    var quesitionNode = hd.SelectSingleNode("//*[@class=\"style123\"]");
                    var questiondetailNode = hd.SelectSingleNode("//*[@id=\"FreeDefinePlaceholderControl1\"]/table[4]/tbody/tr/td[3]/table[2]/tbody/tr[4]/td");
                    if (quesitionNode == null || questiondetailNode == null)
                        return null;
                    var answer = questiondetailNode.InnerText.Split(new char[] { '\r','\n'});
                    List<object> answerWrap = new List<object>();
                    if (answer != null)
                        answer.ToList().ForEach(a =>
                        {
                            string str = a;
                            if (str.Contains("本页面内容供您参考") || string.IsNullOrEmpty(str)) return;
                            answerWrap.Add(str.Trim());
                        });
                    return new { Question = quesitionNode.InnerText, QuesDetail = string.Join("\r\n", answerWrap)  };
                }
            },
            DataHdler = new DataHandler
            {
                URLRegexFilters = new[] { @"http://www.icbc.com.cn/icbc/%E5%AE%A2%E6%88%B7%E6%9C%8D%E5%8A%A1/%E7%83%AD%E7%82%B9%E9%97%AE%E7%AD%94/([a-zA-Z0-9\-\?\,\'\\\+&amp;%\$#_]*)(\/)([a-zA-Z0-9\-\?\,\'\\\+&amp;%\$#_]*)\.htm" },
            }
        };
    }
}

