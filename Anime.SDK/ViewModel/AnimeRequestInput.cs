using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    public class AnimeRequestInput
    {
        /// <summary>
        /// 模式
        /// </summary>
        public AnimeEnum AnimeType { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 关键字
        /// </summary>
        public string AnimeSearchKeyWord { get; set; }
        /// <summary>
        /// 字母分类
        /// </summary>
        public AnimeLetterEnum AnimeLetterType { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailAddress { get; set; }
        /// <summary>
        /// 详情结果
        /// </summary>
        public AnimeDetailResult DetailResult { get; set; }
    }
}
