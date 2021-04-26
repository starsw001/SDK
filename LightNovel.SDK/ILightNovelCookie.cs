using LightNovel.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK
{
    public interface ILightNovelCookie
    {
        void RefreshCookie(LightNovelRefresh Input);
    }
}
