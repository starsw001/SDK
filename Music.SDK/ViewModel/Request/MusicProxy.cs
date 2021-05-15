using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Request
{
    public class MusicProxy
    {
        /// <summary>
        /// 代理IP
        /// </summary>
        public string IP { get; set; } = "";
        /// <summary>
        /// 代理IP端口
        /// </summary>
        public int Port { get; set; } = -1;
        /// <summary>
        /// 凭证
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; } = "";
    }
}
