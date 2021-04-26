using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel.Request
{
    public class LightNovelCategory
    {
        public string CategoryAddress { get; set; }
        public int Page { get; set; } = 1;
    }
}
