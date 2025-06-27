
namespace UDP.UI
{
    partial class DataForm
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
            this.buttonSend = new Sunny.UI.UIButton();
            this.txtSend = new Sunny.UI.UITextBox();
            this.bt_clear_send = new Sunny.UI.UIButton();
            this.lblSendData = new Sunny.UI.UILabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtReceived = new Sunny.UI.UITextBox();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtSend.SuspendLayout();
            this.txtReceived.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSend.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonSend.Location = new System.Drawing.Point(718, 360);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSend.MinimumSize = new System.Drawing.Size(3, 2);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(161, 36);
            this.buttonSend.TabIndex = 58;
            this.buttonSend.Text = "发送";
            this.buttonSend.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Controls.Add(this.bt_clear_send);
            this.txtSend.Controls.Add(this.lblSendData);
            this.txtSend.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSend.Location = new System.Drawing.Point(12, 246);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSend.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSend.Name = "txtSend";
            this.txtSend.Padding = new System.Windows.Forms.Padding(5);
            this.txtSend.ShowText = false;
            this.txtSend.Size = new System.Drawing.Size(692, 154);
            this.txtSend.TabIndex = 61;
            this.txtSend.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSend.Watermark = "";
            // 
            // bt_clear_send
            // 
            this.bt_clear_send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_clear_send.FillColor = System.Drawing.Color.White;
            this.bt_clear_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.bt_clear_send.ForeColor = System.Drawing.Color.Black;
            this.bt_clear_send.Location = new System.Drawing.Point(438, 5);
            this.bt_clear_send.MinimumSize = new System.Drawing.Size(1, 1);
            this.bt_clear_send.Name = "bt_clear_send";
            this.bt_clear_send.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bt_clear_send.Size = new System.Drawing.Size(83, 27);
            this.bt_clear_send.TabIndex = 59;
            this.bt_clear_send.Text = "清除";
            this.bt_clear_send.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_clear_send.Click += new System.EventHandler(this.bt_clear_send_Click);
            // 
            // lblSendData
            // 
            this.lblSendData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSendData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblSendData.Location = new System.Drawing.Point(4, 5);
            this.lblSendData.Name = "lblSendData";
            this.lblSendData.Size = new System.Drawing.Size(503, 23);
            this.lblSendData.TabIndex = 35;
            this.lblSendData.Text = "输入发送数据";
            // 
            // txtReceived
            // 
            this.txtReceived.Controls.Add(this.uiButton1);
            this.txtReceived.Controls.Add(this.uiLabel2);
            this.txtReceived.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtReceived.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReceived.Location = new System.Drawing.Point(12, 48);
            this.txtReceived.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceived.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Padding = new System.Windows.Forms.Padding(5);
            this.txtReceived.ShowText = false;
            this.txtReceived.Size = new System.Drawing.Size(692, 154);
            this.txtReceived.TabIndex = 64;
            this.txtReceived.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtReceived.Watermark = "";
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.FillColor = System.Drawing.Color.White;
            this.uiButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.uiButton1.ForeColor = System.Drawing.Color.Black;
            this.uiButton1.Location = new System.Drawing.Point(438, 5);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiButton1.Size = new System.Drawing.Size(83, 27);
            this.uiButton1.TabIndex = 59;
            this.uiButton1.Text = "清除";
            this.uiButton1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(4, 5);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(503, 23);
            this.uiLabel2.TabIndex = 35;
            this.uiLabel2.Text = "接收到的数据";
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 585);
            this.Controls.Add(this.txtReceived);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.buttonSend);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.txtSend.ResumeLayout(false);
            this.txtReceived.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton buttonSend;
        private Sunny.UI.UITextBox txtSend;
        private Sunny.UI.UILabel lblSendData;
        private Sunny.UI.UIButton bt_clear_send;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Sunny.UI.UITextBox txtReceived;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UILabel uiLabel2;
    }
}