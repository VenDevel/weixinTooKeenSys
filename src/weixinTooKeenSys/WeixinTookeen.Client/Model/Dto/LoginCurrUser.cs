using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTookeen.Client.Model.Dto
{
    public static class LoginCurrUser
    {
        /// <summary>
        /// 会员名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 会员密码
        /// </summary>
        public static string Pwd { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public static int Cap { get; set; }
    }
}
