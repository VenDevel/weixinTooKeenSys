using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeixinTookeen.Client.Controls;
using WeixinTookeen.Client.Https;
using WeixinTookeen.Client.Model;
using WeixinTookeen.Client.Model.Dto;
using WeixinTookeen.Client.Services;

namespace WeixinTookeen.Client
{
    public partial class FormMain : MetroForm
    {
        private static List<object> _contact_all = new List<object>();
        private static List<WXUser> contact_all = new List<WXUser>();
        /// <summary>
        /// 当前登录微信用户
        /// </summary>
        private static WXUser _me;
        public FormMain()
        {
            InitializeComponent();
            GetLoginQRCode();
            FromInit();
        }




        private void GetLoginQRCode()
        {
            picQRCode.Image = null;
            picQRCode.SizeMode = PictureBoxSizeMode.Zoom;
            ((Action)(delegate ()
            {
                //异步加载二维码
                LoginService ls = new LoginService();
                Image qrcode = ls.GetQRCode();
                if (qrcode != null)
                {
                    this.BeginInvoke((Action)delegate ()
                    {
                        picQRCode.Image = qrcode;
                    });
                    object login_result = null;
                    while (true)  //循环判断手机扫面二维码结果
                    {
                        login_result = ls.LoginCheck();
                        if (login_result is Image) //已扫描 未登录
                        {
                            this.BeginInvoke((Action)delegate ()
                            {
                                picQRCode.SizeMode = PictureBoxSizeMode.CenterImage;  //显示头像
                                picQRCode.Image = login_result as Image;
                            });
                        }
                        if (login_result is string)  //已完成登录
                        {
                            //访问登录跳转URL
                            ls.GetSidUid(login_result as string);
                            //初始化API
                            HttpApi api = new HttpApi();
                            api.InitApi(login_result.ToString());
                            //获取好友和并绑定
                            UserServices userServices = new UserServices();
                            WXServices wxServices = new WXServices();
                            JObject initResult = wxServices.WxInit();
                            if (initResult != null)
                            {
                                _me = new WXUser();
                                _me.UserName = initResult["User"]["UserName"].ToString();
                                _me.City = "";
                                _me.HeadImgUrl = initResult["User"]["HeadImgUrl"].ToString();
                                _me.NickName = initResult["User"]["NickName"].ToString();
                                _me.Province = "";
                                _me.PYQuanPin = initResult["User"]["PYQuanPin"].ToString();
                                _me.RemarkName = initResult["User"]["RemarkName"].ToString();
                                _me.RemarkPYQuanPin = initResult["User"]["RemarkPYQuanPin"].ToString();
                                _me.Sex = initResult["User"]["Sex"].ToString();
                                _me.Signature = initResult["User"]["Signature"].ToString();
                            }

                            JObject contact_result = userServices.GetContact(); //通讯录
                            if (contact_result != null)
                            {
                                foreach (JObject contact in contact_result["MemberList"])  //完整好友名单
                                {
                                    WXUser user = new WXUser();
                                    user.UserName = contact["UserName"].ToString();
                                    user.City = contact["City"].ToString();
                                    user.HeadImgUrl = contact["HeadImgUrl"].ToString();
                                    user.NickName = contact["NickName"].ToString();
                                    user.Province = contact["Province"].ToString();
                                    user.PYQuanPin = contact["PYQuanPin"].ToString();
                                    user.RemarkName = contact["RemarkName"].ToString();
                                    user.RemarkPYQuanPin = contact["RemarkPYQuanPin"].ToString();
                                    user.Sex = contact["Sex"].ToString();
                                    user.Signature = contact["Signature"].ToString();
                                    contact_all.Add(user);
                                }
                            }
                            IOrderedEnumerable<WXUser> list_all = contact_all.OrderBy(e => (e as WXUser).ShowPinYin);

                            WXUser wx;
                            string start_char;
                            foreach (object o in list_all)
                            {
                                wx = o as WXUser;
                                start_char = wx.ShowPinYin == "" ? "" : wx.ShowPinYin.Substring(0, 1);
                                if (!_contact_all.Contains(start_char.ToUpper()))
                                {
                                    _contact_all.Add(start_char.ToUpper());
                                }
                                _contact_all.Add(o);
                            }
                            return;
                        }
                    }
                }
            })).BeginInvoke(null, null);
        }

        private void 添加文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetroTaskWindow.ShowTaskWindow(this, "文本信息窗口", new MessaeControl());
            //MessageType type = new MessageType();
            //type.SendType = "文本";
            //type.TxtContent = "你好哈";
            //BindGrad(type);
        }

        private void 添加图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageType type = new MessageType();
            type.SendType = "图片";
            type.TxtContent = "你好哈";
            BindGrad(type);
        }

        private void 添加视频ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageType type = new MessageType();
            type.SendType = "视频";
            type.TxtContent = "你好哈";
            BindGrad(type);
        }

        /// <summary>
        /// 获取选中信息
        /// </summary>
        /// <returns></returns>
        private List<MessageType> GetCheckMessage()
        {
            List<MessageType> listMsgType = new List<MessageType>();
            int count = GridMessageContent.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)GridMessageContent.Rows[i].Cells[0];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    MessageType msgType = new MessageType()
                    {
                        SendType = this.GridMessageContent.Rows[i].Cells[1].Value.ToString(),
                        TxtContent = this.GridMessageContent.Rows[i].Cells[2].Value.ToString()
                    };
                    listMsgType.Add(msgType);
                }
            }
            return listMsgType;
        }

        private void FromInit()
        {
            this.checkMale.Checked = true;
            this.CheckFemale.Checked = true;
            MessageTypeServices sevice = new MessageTypeServices();
            GridMessageContent.DataSource = sevice.GetNewMessage();
            AllCity city = new AllCity();
            List<City> cityList = city.GetAllCity();
            var data= cityList.Where(a => a.Pid == 0);
            cmbSheng.DataSource = cityList.Where(a => a.Pid == 0);
            cmbSheng.ValueMember = "Id";
            cmbSheng.DisplayMember = "Name";
        }

        /// <summary>
        /// 绑定信息
        /// </summary>
        /// <param name="messageType"></param>
        private void BindGrad(MessageType messageType)
        {
            List<MessageType> listMsgType = new List<MessageType>();
            int count = GridMessageContent.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                MessageType msgType = new MessageType()
                {
                    SendType = this.GridMessageContent.Rows[i].Cells[1].Value.ToString(),
                    TxtContent = this.GridMessageContent.Rows[i].Cells[2].Value.ToString()
                };
                listMsgType.Add(msgType);
            }
            bool isAdd = true;
            foreach (var item in listMsgType)
            {
                if (messageType.SendType == item.SendType)
                {
                    item.TxtContent = messageType.TxtContent;
                    isAdd = false;
                }
            }
            if (isAdd)
            {
                listMsgType.Add(messageType);
            }
            GridMessageContent.DataSource = listMsgType;
        }

        /// <summary>
        /// 删除选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<MessageType> listMsgType = new List<MessageType>();
            int count = GridMessageContent.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)GridMessageContent.Rows[i].Cells[0];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == false)
                {
                    MessageType msgType = new MessageType()
                    {
                        SendType = this.GridMessageContent.Rows[i].Cells[1].Value.ToString(),
                        TxtContent = this.GridMessageContent.Rows[i].Cells[2].Value.ToString()
                    };
                    listMsgType.Add(msgType);
                }
            }
            GridMessageContent.DataSource = listMsgType;
        }

        /// <summary>
        /// 设置选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridMessageContent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //获取控件的值
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.GridMessageContent.Rows[e.RowIndex].Cells["Check"];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    checkCell.Value = false;
                }
                else
                {
                    checkCell.Value = true;
                }
            }
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton1_Click(object sender, EventArgs e)
        {
            MessageTypeServices sevice = new MessageTypeServices();
            sevice.SetMessage(GetCheckMessage());
        }
    }
}
