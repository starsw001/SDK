using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelResponseOutput
    {
        public List<NovelRecommend> IndexRecommends { get; set; }
        public List<NovelCategory> IndexCategories { get; set; }
        public NovelSingleCategory SingleCategories { get; set; }
        public List<NovelSearch> SearchResults { get; set; }
        public NovelDetail Details { get; set; }
        public NovelContent Contents  { get; set; }
    }
}
