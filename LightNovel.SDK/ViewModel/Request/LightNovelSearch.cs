using LightNovel.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LightNovel.SDK.ViewModel.Request
{
    public class LightNovelSearch
    {
        internal Dictionary<string, string> FieldMap = new Dictionary<string, string>
        {
            { nameof(ArticleSearch),nameof(ArticleSearch).ToLower()},
            { nameof(SearchKey),nameof(SearchKey).ToLower()},
            { nameof(CharSet),nameof(CharSet).ToLower()},
        };
        public LightNovelSearch() 
        { 
        
        }

        public LightNovelSearch(LightNovelSearchEnum SearchType,string KeyWord)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            SearchKey = HttpUtility.UrlEncode(KeyWord, Encoding.GetEncoding("GBK"));
            ArticleSearch = SearchType.ToString().ToLower();
        }

        /// <summary>
        /// 搜索类型
        /// </summary>
        public string ArticleSearch { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string CharSet { get; } = "gbk";
        /// <summary>
        /// 提交
        /// </summary>
        public string Submit { get; } = "%C7%E1%D0%A1%CB%B5%CB%D1%CB%F7";
    }
}
