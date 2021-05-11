using Music.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Request
{
    public class MusicSearch
    {
        public int Page { get; set; }
        public string KeyWord { get; set; }
        /// <summary>
        /// 查询模式
        /// </summary>
        public MusicTypeEnum MusicType { get; set; }
    }
}
