using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTookeen.Client.Model.Dto
{
    public class MemberLogin
    {

        /// <summary>
        /// 会员名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 会员密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int Cap { get; set; }

    }
}
