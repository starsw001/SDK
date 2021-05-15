using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using XExten.Advance.LinqFramework;
using System.Net;

namespace Music.SDK.Utilily.KuGouUtility
{
    internal class KuGouHelper
    {
        private const string SignHead = "NVPh5oo715z5DIWAeQlhMDsWXXQV4hwt";
        private static Dictionary<string, string> Sign = new Dictionary<string, string>()
        {
            { "callback","callback123"},
            { "clienttime",""},
            { "clientver","20000"},
            { "dfid","-"},
            { "iscorrection","1"},
            { "keyword",""},
            { "mid",""},
            { "page",""},
            { "pagesize","10"},
            { "platform","WebFilter"},
            { "privilege_filter","0"},
            { "srcappid","2919"},
            { "tag","em"},
            { "userid","-1"},
            { "uuid",""},
        };

        internal static string GetParam(string KeyWord, int Page)
        {
            var TimeSpan = (long)(DateTime.Now - TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local)).TotalMilliseconds;
            Sign["keyword"] = KeyWord;
            Sign["page"] = Page.ToString();
            Sign["clienttime"] = Sign["uuid"] = Sign["mid"] = TimeSpan.ToString();

            var PreParam = Sign.OrderBy(t=>t.Key,StringComparer.Ordinal).Select(t => $"{t.Key}={t.Value}");

            var WaitMd5 = SignHead + string.Join("", PreParam) + SignHead;
            var Signature = WaitMd5.ToMd5().ToUpper();
            return $"?{string.Join("&", PreParam)}&signature={Signature}";
        }
    }
}
