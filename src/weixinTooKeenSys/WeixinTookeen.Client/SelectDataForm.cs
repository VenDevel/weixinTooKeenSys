using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeixinTookeen.Client
{
    public partial class SelectDataForm : MetroForm
    {
        public static string returnValue;
        private string _sentdType = string.Empty;
        public SelectDataForm(string message, string sentdType)
        {
            InitializeComponent();
            Txttile.Text = message;
            _sentdType = sentdType;
        }

        private void SelectDataForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void SelectDataForm_DragDrop(object sender, DragEventArgs e)
        {
            txtMessage.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text))
            {
                MetroMessageBox.Show(this, "请输入你要发送的信息！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_sentdType == "文本")
            {

            }
            else if (_sentdType == "图片")
            {
                if (!txtMessage.Text.ToLower().Contains(".jpg"))
                {
                    MetroMessageBox.Show(this, "请选择.JPG格式的图片！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (_sentdType == "视频")
            {
                if (!txtMessage.Text.ToLower().Contains(".mp4"))
                {
                    MetroMessageBox.Show(this, "请选择.JMP4格式的视频！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    FileInfo file = new FileInfo(txtMessage.Text);
                    if (!file.Exists)
                    {
                        MetroMessageBox.Show(this, "视频文件不存在，请重新拽入文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (file.Length> 10485760)
                    {
                        MetroMessageBox.Show(this, "文件过大，请选择文件大小小于10M的视频！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            returnValue = txtMessage.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
