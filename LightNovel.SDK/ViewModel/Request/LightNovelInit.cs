using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel.Request
{
    public class LightNovelInit
    {
        internal  Dictionary<string, string> FieldMap = new Dictionary<string, string>
        {
            {nameof(UserName),nameof(UserName).ToLower()},
            {nameof(PassWord),nameof(PassWord).ToLower()},
            {nameof(UseCookie),nameof(UseCookie).ToLower()},
            {nameof(Action),nameof(Action).ToLower()},
            {nameof(Submit),nameof(Submit).ToLower()},
        };
        public string UserName { get; set; } = "kilydoll365";
        public string PassWord { get; set; } = "sion8550";
        public int UseCookie { get; } = 0;
        public string Action { get; } = "login";
        public string Submit { get; } = "%26%23160%3B%B5%C7%26%23160%3B%26%23160%3B%C2%BC%26%23160%3B";
    }
}
