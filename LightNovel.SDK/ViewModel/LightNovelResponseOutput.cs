using LightNovel.SDK.ViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel
{
    public class LightNovelResponseOutput
    {
        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<LightNovelCategoryResult> CategoryResults { get; set; }
    }
}
