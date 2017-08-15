using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeixinTookeen.Client.Model.Dto;
using WeixinTookeen.Client.Services;
using WeixinTookeen.Client.Util;
using wxRobot.Util.Enums;

namespace WeixinTookeen.Client
{
    public partial class FormLogin : MetroForm
    {
        public FormLogin()
        {
            InitializeComponent();
            tabLogin.Visible = false;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPwd.Text))
            {
                this.txtUserName.Focus();
                MetroMessageBox.Show(this, "请输入用户名和密码", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MemberLogin loginDto = new MemberLogin()
            {
                Pwd = txtPwd.Text,
                UserName = txtUserName.Text
            };
            MemberServices mServices = new MemberServices();
            var result = mServices.Login(loginDto);
            if (result.Code == ResultCodeEnums.Error)
            {
                MetroMessageBox.Show(this, result.Msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.DialogResult = DialogResult.OK;//关键:设置登陆成功状态  
                this.Close();
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserNamer.Text))
            {
                this.txtUserNamer.Focus();
                MetroMessageBox.Show(this, "请输入用户名", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtPwdrn.Text) || string.IsNullOrEmpty(txtPwdr.Text))
            {
                this.txtPwdrn.Focus();
                MetroMessageBox.Show(this, "请输入新密码", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtPwdrn.Text != txtPwdr.Text)
            {
                this.txtPwdrn.Focus();
                MetroMessageBox.Show(this, "两次输入密码不一至", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void brnEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserNameu.Text))
            {
                this.txtUserNameu.Focus();
                MetroMessageBox.Show(this, "请输入用户名", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtpwduo.Text))
            {
                this.txtpwduo.Focus();
                MetroMessageBox.Show(this, "请输入原密码", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtpwdun.Text) || string.IsNullOrEmpty(txtpwdune.Text))
            {
                this.txtpwdun.Focus();
                MetroMessageBox.Show(this, "请输入新密码", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtpwdun.Text != txtpwdune.Text)
            {
                this.txtpwdun.Focus();
                MetroMessageBox.Show(this, "两次输入密码不一至", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserNameAuth.Text))
            {
                this.txtUserNameAuth.Focus();
                MetroMessageBox.Show(this, "请输入用户名", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtAuthCard.Text))
            {
                this.txtAuthCard.Focus();
                MetroMessageBox.Show(this, "请输入授权卡", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtInit.Text = "正在检查网络是否正常……";
            bool result = CheckServe.CheckServeStatus();
            if (!result)
            {
                txtInit.Text = "无法连接到上网络服务！";
            }
            else
            {
                panInit.Visible = false;
                tabLogin.Visible = true;
            }
        }
    }
}
