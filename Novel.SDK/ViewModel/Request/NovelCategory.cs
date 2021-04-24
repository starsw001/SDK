using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel.Request
{
    public class NovelCategory
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 分类地址
        /// </summary>
        public string NovelCategoryAddress { get; set; }
    }
}
