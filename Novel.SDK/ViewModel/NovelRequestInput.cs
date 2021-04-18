using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelRequestInput
    {
        /// <summary>
        /// 模式
        /// </summary>
        public NovelEnum NovelType { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 关键字
        /// </summary>
        public string NovelSearchKeyWord { get; set; }
        /// <summary>
        /// 分类地址
        /// </summary>
        public string NovelCategoryAddress { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string NovelDetailAddress { get; set; }
        /// <summary>
        /// 章节地址
        /// </summary>
        public string NovelViewAddress { get; set; }
    }
}
