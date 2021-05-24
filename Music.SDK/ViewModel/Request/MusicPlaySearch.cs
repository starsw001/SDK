using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Request
{
    public class MusicPlaySearch
    {
        public dynamic Dynamic  { get; set; }
        /// <summary>
        /// 酷狗专用专辑ID
        /// </summary>
        public string KuGouAlbumId { get; set; }
    }
}
