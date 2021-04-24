using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel.Response
{
    public class NovelSingleCategoryResult
    {
        public int TotalPage { get; set; }
        public List<NovelSingleCategoryResults> NovelSingles { get; set; }
    }
    public class NovelSingleCategoryResults
    {
        public string DetailAddress { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string UpdateDate { get; set; }
    }
}
