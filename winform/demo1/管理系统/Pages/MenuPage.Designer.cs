namespace ManageSystem.Pages
{
    partial class MenuPage
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panelHead = new Panel();
            panelContent = new Panel();
            panelTail = new Panel();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelHead, 0, 0);
            tableLayoutPanel1.Controls.Add(panelContent, 0, 1);
            tableLayoutPanel1.Controls.Add(panelTail, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.Size = new Size(890, 604);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelHead
            // 
            panelHead.BackColor = Color.FromArgb(255, 255, 192);
            panelHead.Dock = DockStyle.Fill;
            panelHead.Location = new Point(0, 0);
            panelHead.Margin = new Padding(0);
            panelHead.Name = "panelHead";
            panelHead.Size = new Size(890, 50);
            panelHead.TabIndex = 0;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(192, 255, 192);
            panelContent.Controls.Add(dataGridView1);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 50);
            panelContent.Margin = new Padding(0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(890, 504);
            panelContent.TabIndex = 1;
            // 
            // panelTail
            // 
            panelTail.BackColor = Color.FromArgb(192, 255, 255);
            panelTail.Dock = DockStyle.Fill;
            panelTail.Location = new Point(0, 554);
            panelTail.Margin = new Padding(0);
            panelTail.Name = "panelTail";
            panelTail.Size = new Size(890, 50);
            panelTail.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(890, 504);
            dataGridView1.TabIndex = 0;
            // 
            // MenuPage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "MenuPage";
            Size = new Size(890, 604);
            tableLayoutPanel1.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelHead;
        private Panel panelContent;
        private Panel panelTail;
        private DataGridView dataGridView1;
    }
}
