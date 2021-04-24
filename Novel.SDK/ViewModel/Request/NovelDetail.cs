using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel.Request
{
    public class NovelDetail
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 详情地址
        /// </summary>
        public string NovelDetailAddress { get; set; }
    }
}
