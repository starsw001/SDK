using Anime.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel.Request
{
    public class AnimeCategory
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 字母分类
        /// </summary>
        public AnimeLetterEnum AnimeLetterType { get; set; }
    }
}
