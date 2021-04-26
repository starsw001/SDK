using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel.Response
{
    public class LightNovelSearchResults
    {
        public int TotalPage { get; set; }
        public List<LightNovelSearchResult> Result { get; set; }
    }
    public class LightNovelSearchResult
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Stutas { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailAddress { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }
    }
}
