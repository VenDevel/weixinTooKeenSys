namespace WeixinTookeen.Client
{
    partial class SendLogFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.btmEnter = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.richTextBoxLog.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.richTextBoxLog.Location = new System.Drawing.Point(13, 63);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(428, 328);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // metroTile1
            // 
            this.metroTile1.ActiveControl = null;
            this.metroTile1.Location = new System.Drawing.Point(23, 17);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(394, 38);
            this.metroTile1.TabIndex = 2;
            this.metroTile1.Text = "信息发送日志：";
            this.metroTile1.UseSelectable = true;
            // 
            // btmEnter
            // 
            this.btmEnter.Location = new System.Drawing.Point(315, 401);
            this.btmEnter.Name = "btmEnter";
            this.btmEnter.Size = new System.Drawing.Size(91, 28);
            this.btmEnter.TabIndex = 4;
            this.btmEnter.Text = "关 闭";
            this.btmEnter.UseSelectable = true;
            this.btmEnter.Click += new System.EventHandler(this.btmEnter_Click);
            // 
            // SendLogFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 452);
            this.Controls.Add(this.btmEnter);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.richTextBoxLog);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendLogFrom";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroButton btmEnter;
    }
}