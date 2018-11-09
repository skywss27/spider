using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace EasySpider
{
	public class DataHandler
	{
		//readonly DataBase dataBase = new DataBase ("mongodb://127.0.0.1:27017", "Spider");

		public string[] URLRegexFilters{ get; set; }

		//public void CollectData (string url, int depth, string html, object content)
		//{
		//	if (URLRegexFilters != null && URLRegexFilters.All (f => !Regex.IsMatch (url, f, RegexOptions.IgnoreCase)) || content == null)
		//		return;
		//	//dataBase.Add (new URLInfo{ URL = url, Depth = depth, SlelectedContent = content });
  //          bbcDB.Model.URLInfo u = new bbcDB.Model.URLInfo();
  //          u.URL = url;
  //          u.SlelectedContent = html;
  //          ICBCDAO.Add(u);

  //      }
        public void WriteData(string url, Object question, Object answer)
        {
            if (URLRegexFilters != null && URLRegexFilters.All(f => !Regex.IsMatch(url, f, RegexOptions.IgnoreCase)) || answer == null)
                return;

            //JObject QNA = (JObject)question;
            //string qus = QNA["Question"].ToString();
            //string ans = QNA["QuesDetail"].ToString();
            //string anstemp = string.Empty;
            //foreach (var item in ans.Split(new char[] { '\r', '\n' }))
            //{
            //    if (string.IsNullOrEmpty(item) || item.Contains("本页面内容供您参考"))
            //        continue;
            //    anstemp += item.Trim() + " ";
            //}

            //JObject obj = new JObject();
            //obj.Add("Question", qus);
            //obj.Add("Answer", anstemp.Trim());
            //ICBCQNA QNA = (ICBCQNA)question;

            JObject obj = new JObject();
            obj.Add("Question", question.GetType().GetProperty("Question").GetValue(question).ToString());
            obj.Add("Answer",  question.GetType().GetProperty("QuesDetail").GetValue(question).ToString());

            string json = JsonConvert.SerializeObject(obj);
            string QnaPath = @"D:\IcbcQna.json";//文件存放路径，保证文件存在
            string urlPath = @"D:\Icbcurls.txt";
            //if (!File.Exists(QnaPath))
            //{
            //    File.Create(QnaPath);
            //    File.Create(urlPath);
            //}
               
          
            StreamWriter sw = new StreamWriter(QnaPath, true);
            sw.WriteLine(json);
            sw.Close();

            StreamWriter sw1 = new StreamWriter(urlPath, true);
            sw1.WriteLine(url);
            sw1.Close();
        }
	}
}

