using Music.SDK.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK
{
    public interface IMusic
    {
        MusicResponseOutput MusicSearchItem(MusicRequestInput Input);
        MusicResponseOutput MusicSearchSheet(MusicRequestInput Input);
    }
}
