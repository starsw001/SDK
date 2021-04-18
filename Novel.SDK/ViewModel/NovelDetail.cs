using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelDetail
    {
        public int TotalPage { get; set; }
        public string Cover { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string BookType { get; set; }
        public string ShortURL { get; set; }
        public List<NovelDetails> Details { get; set; }
    }
    public class NovelDetails
    {
        public string ChapterName { get; set; }
        public string ChapterURL { get; set; }
    }
}
