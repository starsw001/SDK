using JavaScriptEngineSwitcher.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Video.SDK
{
    public class Test
    {
        public static void Tests()
        {
            WebClient client = new WebClient();
            var html = client.DownloadString("https://www.iqiyi.com/v_vk4619hw5s.html?vfm=2008_aldbd&fv=p_02_01");

            var tvid = Regex.Match(html, "tvid=(.*?)&aid").Groups[1].Value;
            var vid = Regex.Match(html, "\"vid\",\"(.*?)\",").Groups[1].Value;
            var ts = SyncStatic.ConvertDateTime(DateTime.Now).AsLong() * 1000;
            var temp = Map(tvid, vid, ts);
            var param = "/dash?" + string.Join("&", temp.OrderBy(x => x.Key, StringComparer.Ordinal).Select(x => $"{x.Key}={x.Value}"));

            var md5 = ReadJs("cmd5x").CallFunction<string>("getCmd5x", param);
            temp.Add("vf", md5);
            temp["bop"] = HttpUtility.UrlDecode(temp["bop"]);
            temp["prio"] = HttpUtility.UrlDecode(temp["prio"]);

          var data =  IHttpMultiClient.HttpMulti.Headers("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.87 Safari/537.36")
                .AddNode("https://cache.video.iqiyi.com/dash", temp.ToList(),RequestType.GET).Build().RunString().FirstOrDefault();
            Console.WriteLine(data);
        }
        public static Dictionary<string, string> Map(string tvid, string vid, long ts)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var key1 = ReadJs("iqiyi").CallFunction<string>("auth", "");
            var authkey = ReadJs("iqiyi").CallFunction<string>("auth", key1 + ts.AsString() + tvid);
            result.Add("tvid", tvid);
            result.Add("bid", "300");
            result.Add("vid", vid);
            result.Add("src", "01080031010000000000");
            result.Add("vt", "0");
            result.Add("rs", "1");
            result.Add("uid", "");
            result.Add("ori", "pcw");
            result.Add("ps", "1");
            result.Add("k_uid", "1bf80ab6e72de7ab4a42f4db91bd530b");
            result.Add("pt", "0");
            result.Add("d", "0");
            result.Add("s", "");
            result.Add("lid", "");
            result.Add("cf", "");
            result.Add("ct", "");
            result.Add("authKey", authkey);
            result.Add("k_tag", "1");
            result.Add("ost", "undefined");
            result.Add("ppt", "undefined");
            result.Add("dfp", "a16da00a581aa149139fe169e3914993e4ff9cb705a50e3a41fc7927f988f2cb3e");
            result.Add("locale", "zh_cn");
            result.Add("prio", HttpUtility.UrlEncode("{\"ff\",\"f4v\",\"code\",2}"));
            result.Add("pck", "");
            result.Add("k_err_retries", "0");
            result.Add("up", "");
            result.Add("qd_v", "2");
            result.Add("tm", ts.AsString());
            result.Add("qdy", "a");
            result.Add("qds", "0");
            result.Add("k_ft1", "706436220846084");
            result.Add("k_ft4", "36283952406532");
            result.Add("k_ft5", "1");
            result.Add("bop", HttpUtility.UrlEncode("{\"version\",\"10.0\",\"dfp\",\"a16da00a581aa149139fe169e3914993e4ff9cb705a50e3a41fc7927f988f2cb3e\"}"));
            result.Add("ut", "0");

            return result;
        }

        public static V8JsEngine ReadJs(string name)
        {
            V8JsEngine engine = new V8JsEngine();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "JS", $"{name}.js");
            engine.ExecuteFile(path);
            return engine;
        }
    }
}
