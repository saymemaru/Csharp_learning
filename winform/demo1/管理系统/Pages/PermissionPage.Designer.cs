namespace ManageSystem.Pages
{
    partial class PermissionPage
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
            tableLayoutPanelHead = new TableLayoutPanel();
            comboBoxRoleSelect = new ComboBox();
            buttonSave = new Button();
            flowLayoutPanelContent = new FlowLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanelHead.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanelHead, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanelContent, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(660, 476);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanelHead
            // 
            tableLayoutPanelHead.ColumnCount = 3;
            tableLayoutPanelHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanelHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanelHead.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanelHead.Controls.Add(comboBoxRoleSelect, 0, 0);
            tableLayoutPanelHead.Controls.Add(buttonSave, 1, 0);
            tableLayoutPanelHead.Dock = DockStyle.Fill;
            tableLayoutPanelHead.Location = new Point(3, 3);
            tableLayoutPanelHead.Name = "tableLayoutPanelHead";
            tableLayoutPanelHead.RowCount = 1;
            tableLayoutPanelHead.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelHead.Size = new Size(654, 54);
            tableLayoutPanelHead.TabIndex = 2;
            // 
            // comboBoxRoleSelect
            // 
            comboBoxRoleSelect.Dock = DockStyle.Fill;
            comboBoxRoleSelect.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            comboBoxRoleSelect.FormattingEnabled = true;
            comboBoxRoleSelect.Location = new Point(10, 10);
            comboBoxRoleSelect.Margin = new Padding(10);
            comboBoxRoleSelect.Name = "comboBoxRoleSelect";
            comboBoxRoleSelect.Size = new Size(110, 29);
            comboBoxRoleSelect.TabIndex = 0;
            comboBoxRoleSelect.SelectedValueChanged += comboBoxRoleSelect_SelectedValueChanged;
            // 
            // buttonSave
            // 
            buttonSave.Dock = DockStyle.Fill;
            buttonSave.Location = new Point(140, 10);
            buttonSave.Margin = new Padding(10);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(110, 34);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "保存";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // flowLayoutPanelContent
            // 
            flowLayoutPanelContent.AutoScroll = true;
            flowLayoutPanelContent.Dock = DockStyle.Fill;
            flowLayoutPanelContent.Location = new Point(5, 65);
            flowLayoutPanelContent.Margin = new Padding(5);
            flowLayoutPanelContent.Name = "flowLayoutPanelContent";
            flowLayoutPanelContent.Size = new Size(650, 406);
            flowLayoutPanelContent.TabIndex = 3;
            // 
            // PermissionPage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PermissionPage";
            Size = new Size(660, 476);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanelHead.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comboBoxRoleSelect;
        private TableLayoutPanel tableLayoutPanelHead;
        private FlowLayoutPanel flowLayoutPanelContent;
        private Button buttonSave;
    }
}
