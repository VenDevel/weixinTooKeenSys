using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeixinTookeen.Client.Model;
using WeixinTookeen.Client.Model.Dto;
using WeixinTookeen.Client.Model.Enums;
using wxRobot.Util.Enums;
using wxRobot.Util.Utils;

namespace WeixinTookeen.Client.Services
{
    public class MessageTypeServices
    {
        public void SetMessage(List<MessageType> listMessage)
        {
            using (RobotContext db = new RobotContext())
            {
                var data = db.Set<MessageType>().ToList();
                db.Set<MessageType>().RemoveRange(data);
                db.SaveChanges();
                db.Set<MessageType>().AddRange(listMessage);
                db.SaveChanges();
            }
        }

        public List<MessageType> GetNewMessage()
        {
            using (RobotContext db = new RobotContext())
            {
                var data = db.Set<MessageType>().AsNoTracking().ToList();
                return data;
            }
        }

        public void SetMessageRemote(List<MessageType> listMessage)
        {
            string now = DateTime.Now.ToShortTimeString();
            StringBuilder sb = new StringBuilder();
            int sendCount = 0;
            if (LoginCurrUser.Cap == (int)CapEnum.Leve1)
            {
                sendCount = 3;
            }
            else if (LoginCurrUser.Cap == (int)CapEnum.Leve2)
            {
                sendCount = 100;
            }
            else if (LoginCurrUser.Cap == (int)CapEnum.Leve3)
            {
                sendCount = 200;
            }
            else
            {
                sendCount = 500;
            }
            foreach (var item in listMessage)
            {
                sb.AppendFormat("insert into fygs_news(title,content,createtime,username,sendtype,sendcount) values('{0}','{1}','{2}','{3}','{4}','{5}');", "", item.TxtContent, now, LoginCurrUser.UserName, item.SendType, sendCount);
            }
            Repository.MySqlHelper.ExecuteNonQuery(sb.ToString());
        }

        public List<MessageType> GetNewMessageRemote()
        {
            List<MessageType> list = new List<MessageType>();
            string sql = string.Format("select a.* from fygs_news a,(select sendtype,max(createtime) val from fygs_news group by sendtype) b where a.username='{0}' and a.sendtype = b.sendtype and a.createtime = b.val order by a.sendtype ", LoginCurrUser.UserName);
            DataTable dt = Repository.MySqlHelper.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MessageType message = new MessageType()
                    {
                        SendType = dt.Rows[i]["sendtype"].ToString(),
                        TxtContent = dt.Rows[i]["content"].ToString(),
                        Id = int.Parse(dt.Rows[i]["id"].ToString())
                    };
                    list.Add(message);
                }
            }
            return list;
        }

        public OperResult SetMessgeSendCount(int id)
        {
            OperResult result = new OperResult();
            string sql = string.Format("select * from fygs_news where id={0}", id);
            DataTable dt = Repository.MySqlHelper.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                int count = int.Parse(dt.Rows[0]["id"].ToString());
                if (count <= 0)
                {
                    result.Code = ResultCodeEnums.Error;
                    result.Msg = "该次活动发送次数已使用完，如需要发送更多，请升级会员";
                }
                else
                {
                    sql = string.Format("update fygs_news set SendCount={0}", count - 1);
                    Repository.MySqlHelper.ExecuteNonQuery(sql);
                    result.Code = ResultCodeEnums.success;
                }
            }
            else
            {
                result.Code = ResultCodeEnums.Error;
                result.Msg = "未能获取发送信息，请重新操作";
            }
            return result;
        }
    }
}
