
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
            this.txtReceived = new Sunny.UI.UITextBox();
            this.bt_clear_recv = new Sunny.UI.UIButton();
            this.lblReceiveData = new Sunny.UI.UILabel();
            this.txtSend = new Sunny.UI.UITextBox();
            this.bt_clear_send = new Sunny.UI.UIButton();
            this.lblSendData = new Sunny.UI.UILabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SelectPort = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tab_data = new System.Windows.Forms.TabControl();
            this.txtReceived.SuspendLayout();
            this.txtSend.SuspendLayout();
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
            // txtReceived
            // 
            this.txtReceived.Controls.Add(this.bt_clear_recv);
            this.txtReceived.Controls.Add(this.lblReceiveData);
            this.txtReceived.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtReceived.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReceived.Location = new System.Drawing.Point(5, 16);
            this.txtReceived.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceived.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Padding = new System.Windows.Forms.Padding(5);
            this.txtReceived.ShowText = false;
            this.txtReceived.Size = new System.Drawing.Size(699, 211);
            this.txtReceived.TabIndex = 59;
            this.txtReceived.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtReceived.Watermark = "";
            // 
            // bt_clear_recv
            // 
            this.bt_clear_recv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_clear_recv.FillColor = System.Drawing.Color.White;
            this.bt_clear_recv.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.bt_clear_recv.ForeColor = System.Drawing.Color.Black;
            this.bt_clear_recv.Location = new System.Drawing.Point(431, 4);
            this.bt_clear_recv.MinimumSize = new System.Drawing.Size(1, 1);
            this.bt_clear_recv.Name = "bt_clear_recv";
            this.bt_clear_recv.RectColor = System.Drawing.Color.Gainsboro;
            this.bt_clear_recv.Size = new System.Drawing.Size(86, 27);
            this.bt_clear_recv.TabIndex = 39;
            this.bt_clear_recv.Text = "清除";
            this.bt_clear_recv.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_clear_recv.Click += new System.EventHandler(this.bt_clear_recv_Click);
            // 
            // lblReceiveData
            // 
            this.lblReceiveData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReceiveData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblReceiveData.Location = new System.Drawing.Point(1, 4);
            this.lblReceiveData.Name = "lblReceiveData";
            this.lblReceiveData.Size = new System.Drawing.Size(506, 23);
            this.lblReceiveData.TabIndex = 38;
            this.lblReceiveData.Text = "接收数据";
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
            // SelectPort
            // 
            this.SelectPort.DataSource = null;
            this.SelectPort.FillColor = System.Drawing.Color.White;
            this.SelectPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectPort.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.SelectPort.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.SelectPort.Location = new System.Drawing.Point(728, 48);
            this.SelectPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SelectPort.MinimumSize = new System.Drawing.Size(63, 0);
            this.SelectPort.Name = "SelectPort";
            this.SelectPort.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.SelectPort.Size = new System.Drawing.Size(150, 29);
            this.SelectPort.SymbolSize = 24;
            this.SelectPort.TabIndex = 62;
            this.SelectPort.Text = "ch0";
            this.SelectPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectPort.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(724, 20);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(319, 23);
            this.uiLabel1.TabIndex = 63;
            this.uiLabel1.Text = "发送数据去到了的目标端口";
            // 
            // tab_data
            // 
            this.tab_data.Location = new System.Drawing.Point(-273, -86);
            this.tab_data.Name = "tab_data";
            this.tab_data.SelectedIndex = 0;
            this.tab_data.Size = new System.Drawing.Size(1769, 757);
            this.tab_data.TabIndex = 64;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 585);
            this.Controls.Add(this.SelectPort);
            this.Controls.Add(this.txtReceived);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.tab_data);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.txtReceived.ResumeLayout(false);
            this.txtSend.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton buttonSend;
        private Sunny.UI.UITextBox txtReceived;
        private Sunny.UI.UILabel lblReceiveData;
        private Sunny.UI.UITextBox txtSend;
        private Sunny.UI.UILabel lblSendData;
        private Sunny.UI.UIButton bt_clear_recv;
        private Sunny.UI.UIButton bt_clear_send;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Sunny.UI.UIComboBox SelectPort;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.TabControl tab_data;
    }
}