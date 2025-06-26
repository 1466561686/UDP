namespace UDP.UI
{
    partial class HomeForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStartTest = new Sunny.UI.UIButton();
            this.buttonCloseLowerMachine = new Sunny.UI.UIButton();
            this.buttonEndTest = new Sunny.UI.UIButton();
            this.panel1 = new Sunny.UI.UIPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartTest
            // 
            this.buttonStartTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStartTest.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonStartTest.Location = new System.Drawing.Point(12, 15);
            this.buttonStartTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStartTest.MinimumSize = new System.Drawing.Size(1, 1);
            this.buttonStartTest.Name = "buttonStartTest";
            this.buttonStartTest.Size = new System.Drawing.Size(75, 28);
            this.buttonStartTest.TabIndex = 45;
            this.buttonStartTest.Text = "开始测试";
            this.buttonStartTest.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStartTest.Click += new System.EventHandler(this.buttonStartTest_Click);
            // 
            // buttonCloseLowerMachine
            // 
            this.buttonCloseLowerMachine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCloseLowerMachine.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonCloseLowerMachine.Location = new System.Drawing.Point(204, 15);
            this.buttonCloseLowerMachine.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCloseLowerMachine.MinimumSize = new System.Drawing.Size(2, 2);
            this.buttonCloseLowerMachine.Name = "buttonCloseLowerMachine";
            this.buttonCloseLowerMachine.Size = new System.Drawing.Size(81, 28);
            this.buttonCloseLowerMachine.TabIndex = 41;
            this.buttonCloseLowerMachine.Text = "关闭上位机";
            this.buttonCloseLowerMachine.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCloseLowerMachine.Click += new System.EventHandler(this.buttonCloseLowerMachine_Click);
            // 
            // buttonEndTest
            // 
            this.buttonEndTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEndTest.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonEndTest.Location = new System.Drawing.Point(105, 15);
            this.buttonEndTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEndTest.MinimumSize = new System.Drawing.Size(1, 1);
            this.buttonEndTest.Name = "buttonEndTest";
            this.buttonEndTest.Size = new System.Drawing.Size(75, 28);
            this.buttonEndTest.TabIndex = 46;
            this.buttonEndTest.Text = "结束测试";
            this.buttonEndTest.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEndTest.Click += new System.EventHandler(this.buttonEndTest_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.buttonStartTest);
            this.panel1.Controls.Add(this.buttonEndTest);
            this.panel1.Controls.Add(this.buttonCloseLowerMachine);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.panel1.Location = new System.Drawing.Point(-3, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.panel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1775, 820);
            this.panel1.TabIndex = 57;
            this.panel1.Text = null;
            this.panel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1772, 820);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HomeForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton buttonStartTest;
        private Sunny.UI.UIButton buttonCloseLowerMachine;
        private Sunny.UI.UIButton buttonEndTest;
        private Sunny.UI.UIPanel panel1;
    }
}

