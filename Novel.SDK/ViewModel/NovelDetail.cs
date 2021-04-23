using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelDetail
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 封面地址
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 书籍介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 书籍状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 书籍类型
        /// </summary>
        public string BookType { get; set; }
        /// <summary>
        /// 书籍短连接
        /// </summary>
        public string ShortURL { get; set; }
        /// <summary>
        /// 详细章节
        /// </summary>
        public List<NovelDetails> Details { get; set; }
    }
    public class NovelDetails
    {
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }
        /// <summary>
        /// 章节地址
        /// </summary>
        public string ChapterURL { get; set; }
    }
}
