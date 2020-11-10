namespace LSP
{
    partial class LSP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LSP));
            this.panel_Status = new System.Windows.Forms.Panel();
            this.Lab_Status = new System.Windows.Forms.Label();
            this.Lab_Msg = new System.Windows.Forms.Label();
            this.panel_Web = new System.Windows.Forms.Panel();
            this.panel_Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Status
            // 
            this.panel_Status.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel_Status.Controls.Add(this.Lab_Status);
            this.panel_Status.Controls.Add(this.Lab_Msg);
            this.panel_Status.Location = new System.Drawing.Point(1, 423);
            this.panel_Status.Name = "panel_Status";
            this.panel_Status.Size = new System.Drawing.Size(799, 26);
            this.panel_Status.TabIndex = 3;
            // 
            // Lab_Status
            // 
            this.Lab_Status.Location = new System.Drawing.Point(660, 7);
            this.Lab_Status.Name = "Lab_Status";
            this.Lab_Status.Size = new System.Drawing.Size(136, 14);
            this.Lab_Status.TabIndex = 1;
            // 
            // Lab_Msg
            // 
            this.Lab_Msg.AutoSize = true;
            this.Lab_Msg.Location = new System.Drawing.Point(3, 7);
            this.Lab_Msg.Name = "Lab_Msg";
            this.Lab_Msg.Size = new System.Drawing.Size(0, 12);
            this.Lab_Msg.TabIndex = 0;
            // 
            // panel_Web
            // 
            this.panel_Web.Location = new System.Drawing.Point(1, 1);
            this.panel_Web.Name = "panel_Web";
            this.panel_Web.Size = new System.Drawing.Size(799, 423);
            this.panel_Web.TabIndex = 2;
            // 
            // LSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_Status);
            this.Controls.Add(this.panel_Web);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LSP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LSP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LSP_FormClosing);
            this.Load += new System.EventHandler(this.LSP_Load);
            this.panel_Status.ResumeLayout(false);
            this.panel_Status.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Status;
        private System.Windows.Forms.Label Lab_Msg;
        private System.Windows.Forms.Panel panel_Web;
        private System.Windows.Forms.Label Lab_Status;
    }
}

