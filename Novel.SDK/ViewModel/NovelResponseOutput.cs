using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelResponseOutput
    {
        public List<NovelHot> IndexHots { get; set; }
        public List<NovelRecommend> IndexRecommends { get; set; }
        public List<NovelCategory> IndexCategories { get; set; }
    }
}
