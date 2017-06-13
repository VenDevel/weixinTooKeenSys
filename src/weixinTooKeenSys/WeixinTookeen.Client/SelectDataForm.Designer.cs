namespace WeixinTookeen.Client
{
    partial class SelectDataForm
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
            this.txtMessage = new MetroFramework.Controls.MetroTextBox();
            this.btmEnter = new MetroFramework.Controls.MetroButton();
            this.Txttile = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            // 
            // 
            // 
            this.txtMessage.CustomButton.Image = null;
            this.txtMessage.CustomButton.Location = new System.Drawing.Point(195, 1);
            this.txtMessage.CustomButton.Name = "";
            this.txtMessage.CustomButton.Size = new System.Drawing.Size(107, 107);
            this.txtMessage.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMessage.CustomButton.TabIndex = 1;
            this.txtMessage.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMessage.CustomButton.UseSelectable = true;
            this.txtMessage.CustomButton.Visible = false;
            this.txtMessage.Lines = new string[0];
            this.txtMessage.Location = new System.Drawing.Point(13, 63);
            this.txtMessage.MaxLength = 32767;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PasswordChar = '\0';
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMessage.SelectedText = "";
            this.txtMessage.SelectionLength = 0;
            this.txtMessage.SelectionStart = 0;
            this.txtMessage.ShortcutsEnabled = true;
            this.txtMessage.Size = new System.Drawing.Size(303, 109);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.UseSelectable = true;
            this.txtMessage.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMessage.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btmEnter
            // 
            this.btmEnter.Location = new System.Drawing.Point(225, 189);
            this.btmEnter.Name = "btmEnter";
            this.btmEnter.Size = new System.Drawing.Size(91, 28);
            this.btmEnter.TabIndex = 3;
            this.btmEnter.Text = "确 定";
            this.btmEnter.UseSelectable = true;
            this.btmEnter.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // Txttile
            // 
            this.Txttile.ActiveControl = null;
            this.Txttile.Location = new System.Drawing.Point(13, 19);
            this.Txttile.Name = "Txttile";
            this.Txttile.Size = new System.Drawing.Size(303, 38);
            this.Txttile.TabIndex = 4;
            this.Txttile.Text = "输入你要发送的文字：";
            this.Txttile.UseSelectable = true;
            // 
            // SelectDataForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 240);
            this.Controls.Add(this.Txttile);
            this.Controls.Add(this.btmEnter);
            this.Controls.Add(this.txtMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectDataForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SelectDataForm_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtMessage;
        private MetroFramework.Controls.MetroButton btmEnter;
        private MetroFramework.Controls.MetroTile Txttile;
    }
}