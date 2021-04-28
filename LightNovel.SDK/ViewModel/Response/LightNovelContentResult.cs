using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel.Response
{
    public class LightNovelContentResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Image { get; set; }
    }
}
