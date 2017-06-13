namespace WeixinTookeen.Client.Controls
{
    partial class MessaeControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.txtMessage = new MetroFramework.Controls.MetroTextBox();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(218, 138);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(91, 28);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "确 定";
            this.metroButton1.UseSelectable = true;
            // 
            // txtMessage
            // 
            // 
            // 
            // 
            this.txtMessage.CustomButton.Image = null;
            this.txtMessage.CustomButton.Location = new System.Drawing.Point(214, 2);
            this.txtMessage.CustomButton.Name = "";
            this.txtMessage.CustomButton.Size = new System.Drawing.Size(73, 73);
            this.txtMessage.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMessage.CustomButton.TabIndex = 1;
            this.txtMessage.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMessage.CustomButton.UseSelectable = true;
            this.txtMessage.CustomButton.Visible = false;
            this.txtMessage.Lines = new string[0];
            this.txtMessage.Location = new System.Drawing.Point(19, 47);
            this.txtMessage.MaxLength = 32767;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PasswordChar = '\0';
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMessage.SelectedText = "";
            this.txtMessage.SelectionLength = 0;
            this.txtMessage.SelectionStart = 0;
            this.txtMessage.ShortcutsEnabled = true;
            this.txtMessage.Size = new System.Drawing.Size(290, 78);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.UseSelectable = true;
            this.txtMessage.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMessage.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(19, 1);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(182, 38);
            this.metroTile1.TabIndex = 1;
            this.metroTile1.Text = "输入你要发送的文字：";
            this.metroTile1.UseSelectable = true;
            // 
            // MessaeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.txtMessage);
            this.Name = "MessaeControl";
            this.Size = new System.Drawing.Size(337, 179);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtMessage;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroTile metroTile1;
    }
}
