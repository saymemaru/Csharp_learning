namespace ManageSystem.Pages
{
    partial class MenuFunctionPage
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
            labelMenuTitle = new Label();
            flowLayoutPanelContent = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelMenuTitle
            // 
            labelMenuTitle.AutoSize = true;
            labelMenuTitle.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelMenuTitle.Location = new Point(3, 0);
            labelMenuTitle.Name = "labelMenuTitle";
            labelMenuTitle.Size = new Size(67, 25);
            labelMenuTitle.TabIndex = 0;
            labelMenuTitle.Text = "label1";
            // 
            // flowLayoutPanelContent
            // 
            flowLayoutPanelContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelContent.AutoScroll = true;
            flowLayoutPanelContent.Location = new Point(5, 35);
            flowLayoutPanelContent.Margin = new Padding(5);
            flowLayoutPanelContent.Name = "flowLayoutPanelContent";
            flowLayoutPanelContent.Size = new Size(614, 113);
            flowLayoutPanelContent.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanelContent, 0, 1);
            tableLayoutPanel1.Controls.Add(labelMenuTitle, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(624, 153);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // MenuFunctionPage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(5);
            Name = "MenuFunctionPage";
            Size = new Size(624, 153);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelMenuTitle;
        private FlowLayoutPanel flowLayoutPanelContent;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
