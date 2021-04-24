using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel.Request
{
    public class AnimeSearch
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 关键字
        /// </summary>
        public string AnimeSearchKeyWord { get; set; }
    }
}
