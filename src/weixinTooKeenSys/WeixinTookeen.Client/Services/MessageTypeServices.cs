using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeixinTookeen.Client.Model;
using WeixinTookeen.Client.Model.Dto;

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
            DateTime now = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            foreach (var item in listMessage)
            {
                sb.AppendFormat("insert into title,content,createtime,username,sendtype fygs_news values('{0}','{1}','{2}','{3}','{4}');", "", item.TxtContent, LoginCurrUser.UserName, now, item.SendType);
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
                        TxtContent = dt.Rows[i]["content"].ToString()
                    };
                    list.Add(message);
                }
            }
            return list;
        }
    }
}
