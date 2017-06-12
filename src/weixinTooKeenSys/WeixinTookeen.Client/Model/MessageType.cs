using System;
using System.Collections.Generic;
using System.Text;

namespace WeixinTookeen.Client.Model
{
    public class MessageType: BaseEntity
    {
        public string SendType { get; set; }

        public string TxtContent { get; set; }
    }
}
