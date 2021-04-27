using LightNovel.SDK.ViewModel.Enums;
using LightNovel.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel
{
    public class LightNovelRequestInput
    {
        /// <summary>
        /// 模式
        /// </summary>
        public LightNovelEnum LightNovelType { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        public LightNovelSearch Search { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public LightNovelCategory Category { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public LightNovelDetail Detail { get; set; }
        /// <summary>
        /// 预览
        /// </summary>
        public LightNovelView View { get; set; }
        /// <summary>
        /// 内容
        /// </summary>

        public LightNovelContent Content { get; set; }
    }
}
