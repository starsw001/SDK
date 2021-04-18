using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelSingleCategory
    {
        public int Page { get; set; }
        public List<NovelSingleCategories> NovelSingles { get; set; }
    }
    public class NovelSingleCategories
    {
        public string DetailAddress { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string UpdateDate { get; set; }
    }
}
