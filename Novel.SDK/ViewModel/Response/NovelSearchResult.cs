using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel.Response
{
    public class NovelSearchResult
    {
        public string RecommendType { get; set; }
        public string DetailAddress { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string UpdateDate { get; set; }
    }
}
