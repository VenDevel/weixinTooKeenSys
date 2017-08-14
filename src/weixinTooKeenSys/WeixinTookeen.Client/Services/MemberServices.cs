using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WeixinTookeen.Client.Model.Dto;
using wxRobot.Util.Enums;
using wxRobot.Util.Utils;

namespace WeixinTookeen.Client.Services
{
    public class MemberServices
    {
        public OperResult Login(MemberLogin loginDto)
        {
            OperResult result = new OperResult();
            byte[] r = Encoding.Default.GetBytes(loginDto.Pwd);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(r);
            loginDto.Pwd = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的
            string sql = string.Format("select * from fygs_member where fukuan>0 and fid>0 and yonghuming ='{0}'  and mima='{1}'", loginDto.UserName, loginDto.Pwd);
            DataTable dt = Repository.MySqlHelper.ExecuteQuery(sql);
            if (dt.Rows.Count <= 0)
            {
                result.Code = ResultCodeEnums.Error;
                result.Msg = "用户名或密码错误！请重新操作。";
            }
            else
            {
                result.Code = ResultCodeEnums.success;
                loginDto.Cap = int.Parse(dt.Rows[0]["cap"].ToString());
                LoginCurrUser.Cap = loginDto.Cap;
                LoginCurrUser.UserName = loginDto.UserName;
            }
            return result;
        }
    }
}
