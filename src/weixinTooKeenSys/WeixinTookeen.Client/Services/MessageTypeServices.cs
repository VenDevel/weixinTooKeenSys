using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeixinTookeen.Client.Model;

namespace WeixinTookeen.Client.Services
{
    public class MessageTypeServices
    {
        public void SetMessage(List<MessageType> listMessage)
        {
            using (RobotContext db = new RobotContext())
            {
                var data= db.Set<MessageType>().ToList();
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
    }
}
