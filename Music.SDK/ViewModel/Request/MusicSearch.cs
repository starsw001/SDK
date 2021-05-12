using Music.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Request
{
    public class MusicSearch
    {
        public int Page { get; set; } = 1;
        public string KeyWord { get; set; }
    }
}
