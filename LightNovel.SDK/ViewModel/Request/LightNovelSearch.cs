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
        /// <summary>
        /// 搜索类型
        /// </summary>
        public LightNovelSearchEnum SearchType { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
