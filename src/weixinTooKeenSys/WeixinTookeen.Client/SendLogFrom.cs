using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeixinTookeen.Client
{
    public partial class SendLogFrom :  MetroForm
    {

        public SendLogFrom()
        {
            InitializeComponent();
            this.ControlBox = false;   // 设置不出现关闭按钮
        }

        public SendLogFrom(FormMain from):this()
        {
            from.SendSucess += From_SendSucess;
            from.ColoseWin += From_ColoseWin;
        }

        private void From_ColoseWin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void From_SendSucess(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)delegate ()
            {
                this.richTextBoxLog.AppendText(sender.ToString() + "\n");
            });
            
        }

        private void btmEnter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
