using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelHot
    {
        public string BookName { get; set; }
        public string DetailAddress { get; set; }
        public string Cover { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
    public class NovelRecommend
    {
        public string RecommendType { get; set; }
        public string DetailAddress { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
    }
    public class NovelCategory 
    { 
        public string CategoryName { get; set; }
        public string CollectAddress { get; set; }
    }
}
