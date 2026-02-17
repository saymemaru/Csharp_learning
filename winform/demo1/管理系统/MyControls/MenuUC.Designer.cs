namespace ManageSystem.MyControls
{
    partial class MenuUC
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = ManageSystem.Properties.Resources.Pistol;
            pictureBox1.Location = new Point(10, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("华文中宋", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.White;
            label1.Location = new Point(90, 12);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(114, 40);
            label1.TabIndex = 1;
            label1.Text = "Menu";
            // 
            // MenuUC
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlText;
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Margin = new Padding(0);
            Name = "MenuUC";
            Size = new Size(240, 80);
            MouseEnter += MenuUC_MouseEnter;
            MouseLeave += MenuUC_MouseLeave;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
    }
}
