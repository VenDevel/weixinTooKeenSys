using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeixinTookeen.Client.Controls;
using WeixinTookeen.Client.Https;
using WeixinTookeen.Client.Model;
using WeixinTookeen.Client.Model.Dto;
using WeixinTookeen.Client.Services;
using wxRobot.Util.Enums;
using wxRobot.Util.Utils;

namespace WeixinTookeen.Client
{
    public partial class FormMain : MetroForm
    {
        private static List<object> _contact_all = new List<object>();
        private static List<WXUser> contact_all = new List<WXUser>();
        //发送日志
        public event EventHandler SendSucess;
        //关闭窗体
        public event EventHandler ColoseWin;
        /// <summary>
        /// 当前登录微信用户
        /// </summary>
        private static WXUser _me;
        public FormMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            GetLoginQRCode();
            FromInit();
        }
        SendLogFrom from = null;
        public void SendMessageInit()
        {
            ServiceRecordSvc svc = new ServiceRecordSvc();
            var Authdata = svc.IsAuth();
            if (Authdata.Code == ResultCodeEnums.AuthExpire)
            {
                MetroMessageBox.Show(this, Authdata.Msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetLoginQRCode();
                return;
            }
            this.BeginInvoke((Action)(delegate ()
            {
                if (from == null)
                {
                    from = new SendLogFrom(this);
                }
                from.Left = this.Left + this.Width;
                from.Top = this.Top;
                from.Show();
            }));
            Thread.Sleep(500);
            ExecEven("正初化发送信息设置！", null);
            List<WXUser> list = FilterOjb();
            ExecEven(string.Format("一共获取了{0}个好友，此次将发送给{1}个好友", contact_all.Count(), list.Count()), null);
            GetSendMessage();
            ExecEvenColse("", null);
            WXServices services = new WXServices();
            services.Wxlogout();
            GetLoginQRCode();
        }



        private void GetSendMessage()
        {
            MessageTypeServices sevice = new MessageTypeServices();
            List<MessageType> message = sevice.GetNewMessageRemote();
            if (message.Count <= 0)
            {
                MetroMessageBox.Show(this, "未能获取到当前发送的信息，请设置好信息并点击保存按钮！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<WXMesssage> setWxMsg = new List<WXMesssage>();
            SetText(message, setWxMsg);
            UploadImage(message, setWxMsg);
            UploadViedo(message, setWxMsg);
            if (setWxMsg.Count <= 0)
            {
                MetroMessageBox.Show(this, "未能获取到当前发送的信息，请设置好信息并点击保存按钮！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ColoseWin(true, null);
                return;
            }
            SendMessage(setWxMsg);
        }

        private void SendMessage(List<WXMesssage> wxMsg)
        {
            List<WXUser> list = FilterOjb();
            for (int i = 1; i <= list.Count; i++)
            {
                foreach (var item in wxMsg)
                {
                    item.To = list[i - 1].UserName;
                    item.ToNickName = list[i - 1].NickName;
                    switch (item.Type)
                    {
                        case 1:
                            SendText(item, i);
                            break;
                        case 3:
                            SendImage(item, i);
                            break;
                        case 43:
                            SendViedo(item, i);
                            break;
                    }
                }
            }
        }

        private void SetText(List<MessageType> message, List<WXMesssage> wxMsgList)
        {
            var sendMsg = message.Where(a => a.SendType == "文本").FirstOrDefault();
            if (null != sendMsg)
            {
                WXMesssage msg = new WXMesssage();
                msg.Type = 1;
                msg.Msg = WXFace.AddFace(sendMsg.TxtContent);
                msg.Readed = false;
                msg.Time = DateTime.Now;
                msg.From = _me.UserName;
                wxMsgList.Add(msg);
            }

        }

        private void UploadImage(List<MessageType> message, List<WXMesssage> wxMsgList)
        {
            WXMesssage msg = new WXMesssage();
            var sendImage = message.Where(a => a.SendType == "图片").FirstOrDefault();
            if (null != sendImage)
            {
                if (!File.Exists(sendImage.TxtContent))
                {
                    MetroMessageBox.Show(this, "文件不存在，请选择好文件！！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //先上传
                WXServices wxServices = new WXServices();
                ExecEven("正在上传文件到微信服务器.........", null);
                var resultJson = wxServices.UploadImage(sendImage.TxtContent);
                if (!string.IsNullOrEmpty(resultJson))
                {
                    JObject obj = JsonConvert.DeserializeObject(resultJson) as JObject;
                    msg.Type = 3;
                    msg.MediaId = obj["MediaId"].ToString();
                    msg.Readed = false;
                    msg.Time = DateTime.Now;
                    msg.From = _me.UserName;
                    wxMsgList.Add(msg);
                }
            }
        }

        private void UploadViedo(List<MessageType> message, List<WXMesssage> wxMsgList)
        {
            WXMesssage msg = new WXMesssage();
            var sendVideo = message.Where(a => a.SendType == "视频").FirstOrDefault();
            if (null != sendVideo)
            {
                if (!File.Exists(sendVideo.TxtContent))
                {
                    MetroMessageBox.Show(this, "文件不存在，请选择好文件！！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                WXServices wxServices = new WXServices();
                ExecEven("正在上传文件到微信服务器.........", null);
                var resultJson = wxServices.UploadVideo(sendVideo.TxtContent);
                if (!string.IsNullOrEmpty(resultJson))
                {
                    JObject obj = JsonConvert.DeserializeObject(resultJson) as JObject;
                    msg.MediaId = obj["MediaId"].ToString();
                    msg.Type = 43;
                    msg.Readed = false;
                    msg.Time = DateTime.Now;
                    msg.From = _me.UserName;
                    wxMsgList.Add(msg);
                }
            }
        }

        private void SendText(WXMesssage msg, int count)
        {
            //发消息
            if (null != msg)
            {
                if ((count % 100) == 0)
                {
                    ExecEven("休息一下等待下一轮发送.........", null);
                    Thread.Sleep(60 * 1000);
                }
                _me.SendMsg(msg);
                Thread.Sleep(900);
                var semdLog = string.Format("{0}\t已发【文本】信息给{1}", DateTime.Now.ToString(), msg.ToNickName);
                ExecEven(semdLog, null);
            }

        }
        private void SendImage(WXMesssage msg, int count)
        {
            if (null != msg)
            {
                if ((count % 15) == 0)
                {
                    ExecEven("休息一下等待下一轮发送.........", null);
                    Thread.Sleep(60 * 1000);
                }
                _me.SendImage(msg);
                Thread.Sleep(900);
                var semdLog = string.Format("{0}\t已发【图片】信息给{1}", DateTime.Now.ToString(), msg.ToNickName);
                ExecEven(semdLog, null);
            }
        }
        private void SendViedo(WXMesssage msg, int count)
        {
            if (null != msg)
            {
                if ((count % 120) == 0)
                {
                    ExecEven("休息一下等待下一轮发送.........", null);
                    Thread.Sleep(60 * 1000);
                }
                _me.SendVideo(msg);
                Thread.Sleep(900);
                var semdLog = string.Format("{0}\t已发【视频】信息给{1}", DateTime.Now.ToString(), msg.ToNickName);
                ExecEven(semdLog, null);
            }
        }

        /// <summary>
        /// 获取信息接收对象
        /// </summary>
        /// <returns></returns>
        private List<WXUser> FilterOjb()
        {
            var sendOjb = contact_all;
            if (!checkMale.Checked)
            {
                sendOjb = sendOjb.Where(a => a.Sex != "1").ToList();
            }
            if (!CheckFemale.Checked)
            {
                sendOjb = sendOjb.Where(a => a.Sex != "2").ToList();
            }
            if (!CheckGroup.Checked)
            {
                sendOjb = sendOjb.Where(a => !a.UserName.Contains("@@")).ToList();
            }
            if (!CheckPublicAccount.Checked)
            {
                sendOjb = sendOjb.Where(a => a.VerifyFlag == "0").ToList();
            }
            if (cmbSheng.SelectedIndex != 0)
            {
                sendOjb = sendOjb.Where(a => a.Province == cmbSheng.SelectedText).ToList();
            }
            if (cmbShi.SelectedIndex != 0)
            {
                sendOjb = sendOjb.Where(a => a.Province == cmbSheng.SelectedText).ToList();
            }
            return sendOjb;
        }


        /// <summary>
        /// 发送日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        private void ExecEven(string message, EventArgs e)
        {
            if (SendSucess != null)
            {
                SendSucess(message, e);
            }
        }

        /// <summary>
        /// 发送日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        private void ExecEvenColse(string message, EventArgs e)
        {
            if (ColoseWin != null)
            {
                ColoseWin(metroCboColse.Checked, e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void GetLoginQRCode()
        {
            contact_all.Clear();
            _contact_all.Clear();
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
                                    user.ContactFlag = contact["ContactFlag"].ToString();
                                    user.VerifyFlag = contact["VerifyFlag"].ToString();                                    
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
                            SendMessageInit();
                            return;
                        }
                    }
                }
            })).BeginInvoke(null, null);
        }

        private void 添加文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDataForm selectForm = new SelectDataForm("请输入要发送的文字信息：", "文本");
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                MessageType type = new MessageType();
                type.SendType = "文本";
                type.TxtContent = SelectDataForm.returnValue;
                BindGrad(type);
            }

        }

        private void 添加图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDataForm selectForm = new SelectDataForm("请拖入发送的图片信息（只支持JPG格式）：", "图片");
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                MessageType type = new MessageType();
                type.SendType = "图片";
                type.TxtContent = SelectDataForm.returnValue;
                BindGrad(type);
            }
        }

        private void 添加视频ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDataForm selectForm = new SelectDataForm("请拖入发送的视频信息（只支持MP4格式）：", "视频");
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                MessageType type = new MessageType();
                type.SendType = "视频";
                type.TxtContent = SelectDataForm.returnValue;
                BindGrad(type);
            }
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

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void FromInit()
        {
            this.checkMale.Checked = true;
            this.CheckFemale.Checked = true;
            MessageTypeServices sevice = new MessageTypeServices();
            GridMessageContent.DataSource = sevice.GetNewMessageRemote();
            List<City> cityList = AllCity.allCityData;
            var data = cityList.Where(a => a.Pid == 0).ToList();
            data.Insert(0, new City() { Id = 0, Name = "全部" });
            cmbSheng.DataSource = data;
            cmbSheng.ValueMember = "Id";
            cmbSheng.DisplayMember = "Name";
            cmbSheng.SelectedIndex = 0;
            var data2 = new List<City>();
            data2.Insert(0, new City() { Id = 0, Name = "全部" });
            cmbShi.DataSource = data2;
            cmbShi.ValueMember = "Id";
            cmbShi.DisplayMember = "Name";
            cmbShi.SelectedIndex = 0;
            ServiceRecordSvc svc = new ServiceRecordSvc();
            var Authdata = svc.IsAuth();
            ServiceRecord rec = (ServiceRecord)Authdata.Data;
            lblDate.Text = rec.ExpireDate.GetDateTimeFormats('f')[0].ToString();
            MachineSvc mcSvc = new MachineSvc();
            var key = mcSvc.Get().MachineCode;
            lblMCCode.Text = key;
            lblUserName.Text = "软件试用期";
            if (Authdata.Code == ResultCodeEnums.Auth)
            {
                lblUserName.Text = GetAESInfo.Get(rec.SurplusTotal, key);
                lblAuthCartic.Visible = false;
                txtAuthCard.Visible = false;
                btnAuth.Visible = false;
            }
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
            var data = GetCheckMessage();
            if (data.Count <= 0)
            {
                return;
            }
            sevice.SetMessageRemote(data);
            ServiceRecordSvc svc = new ServiceRecordSvc();
            svc.SetRecord();
        }

        private void cmbSheng_SelectedValueChanged(object sender, EventArgs e)
        {
            var index = this.cmbSheng.SelectedIndex;
            if (index == 0)
            {
                return;
            }
            List<City> cityList = AllCity.allCityData;
            var list = cityList.Where(a => a.Pid == index).ToList();
            list.Insert(0, new City() { Id = 0, Name = "全部" });
            cmbShi.DataSource = list;
            cmbShi.ValueMember = "Id";
            cmbShi.DisplayMember = "Name";
            cmbShi.SelectedIndex = 0;
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            //if (btnAuth.Text="再次授权")
            //{

            //}
            if (string.IsNullOrEmpty(txtAuthCard.Text))
            {
                MetroMessageBox.Show(this, "请输入授权卡！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ServiceRecordSvc recSvc = new ServiceRecordSvc();
            var data = recSvc.Auth(txtAuthCard.Text, lblMCCode.Text);
            MetroMessageBox.Show(this, data.Msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblMCCode_Click(object sender, EventArgs e)
        {
            //var key = Guid.NewGuid().ToString("N");
            //MachineSvc svc = new MachineSvc();
            //svc.Add(key);
            //lblMCCode.Text = key;
            Clipboard.SetDataObject(lblMCCode.Text);
            MetroMessageBox.Show(this, "机器码已复制！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void picQRCode_Click(object sender, EventArgs e)
        {
            GetLoginQRCode();
        }
    }
}
