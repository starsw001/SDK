using Novel.SDK.ViewModel.Enums;
using Novel.SDK.ViewModel.Request;
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
        /// 初始化
        /// </summary>
        public NovelInit Init { get; }
        /// <summary>
        /// 搜索
        /// </summary>
        public NovelSearch Search { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public NovelCategory Category { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public NovelDetail Detail { get; set; }
        /// <summary>
        /// 预览
        /// </summary>
        public NovelView View { get; set; }
    }
}
