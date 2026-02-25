namespace ManageSystem
{
    partial class AddPersonForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panelHead = new Panel();
            pictureBox1 = new PictureBox();
            panelContent = new Panel();
            tableLayoutPanelSplit = new TableLayoutPanel();
            panelMiddle = new Panel();
            textBox4 = new TextBox();
            buttonAdd = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            labelAddress = new Label();
            labelGender = new Label();
            labelName = new Label();
            labelId = new Label();
            labelTitle = new Label();
            comboBoxRole = new ComboBox();
            labelRole = new Label();
            tableLayoutPanel1.SuspendLayout();
            panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelContent.SuspendLayout();
            tableLayoutPanelSplit.SuspendLayout();
            panelMiddle.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panelHead, 0, 0);
            tableLayoutPanel1.Controls.Add(panelContent, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(585, 396);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelHead
            // 
            panelHead.BackColor = Color.FromArgb(255, 128, 0);
            panelHead.Controls.Add(pictureBox1);
            panelHead.Dock = DockStyle.Fill;
            panelHead.Location = new Point(0, 0);
            panelHead.Margin = new Padding(0);
            panelHead.Name = "panelHead";
            panelHead.Size = new Size(585, 80);
            panelHead.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.airborne;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(585, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(tableLayoutPanelSplit);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(3, 83);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(579, 310);
            panelContent.TabIndex = 1;
            // 
            // tableLayoutPanelSplit
            // 
            tableLayoutPanelSplit.ColumnCount = 3;
            tableLayoutPanelSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelSplit.Controls.Add(panelMiddle, 1, 1);
            tableLayoutPanelSplit.Dock = DockStyle.Fill;
            tableLayoutPanelSplit.Location = new Point(0, 0);
            tableLayoutPanelSplit.Name = "tableLayoutPanelSplit";
            tableLayoutPanelSplit.RowCount = 3;
            tableLayoutPanelSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanelSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelSplit.Size = new Size(579, 310);
            tableLayoutPanelSplit.TabIndex = 1;
            // 
            // panelMiddle
            // 
            panelMiddle.Controls.Add(labelRole);
            panelMiddle.Controls.Add(comboBoxRole);
            panelMiddle.Controls.Add(textBox4);
            panelMiddle.Controls.Add(labelTitle);
            panelMiddle.Controls.Add(buttonAdd);
            panelMiddle.Controls.Add(textBox3);
            panelMiddle.Controls.Add(textBox2);
            panelMiddle.Controls.Add(textBox1);
            panelMiddle.Controls.Add(labelAddress);
            panelMiddle.Controls.Add(labelGender);
            panelMiddle.Controls.Add(labelName);
            panelMiddle.Controls.Add(labelId);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(147, 34);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(283, 242);
            panelMiddle.TabIndex = 1;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(84, 132);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(142, 23);
            textBox4.TabIndex = 9;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(83, 192);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(79, 40);
            buttonAdd.TabIndex = 7;
            buttonAdd.Text = "添加";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(84, 107);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(142, 23);
            textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(84, 79);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(142, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(84, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(142, 23);
            textBox1.TabIndex = 4;
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelAddress.Location = new Point(3, 130);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(75, 28);
            labelAddress.TabIndex = 8;
            labelAddress.Text = "地址：";
            // 
            // labelGender
            // 
            labelGender.AutoSize = true;
            labelGender.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelGender.Location = new Point(3, 102);
            labelGender.Name = "labelGender";
            labelGender.Size = new Size(75, 28);
            labelGender.TabIndex = 3;
            labelGender.Text = "性别：";
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelName.Location = new Point(3, 74);
            labelName.Name = "labelName";
            labelName.Size = new Size(75, 28);
            labelName.TabIndex = 2;
            labelName.Text = "姓名：";
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelId.Location = new Point(3, 46);
            labelId.Name = "labelId";
            labelId.Size = new Size(52, 28);
            labelId.TabIndex = 1;
            labelId.Text = "Id：";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("华文中宋", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelTitle.Location = new Point(83, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(143, 36);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "添加菜单";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBoxRole
            // 
            comboBoxRole.FormattingEnabled = true;
            comboBoxRole.Location = new Point(84, 161);
            comboBoxRole.Name = "comboBoxRole";
            comboBoxRole.Size = new Size(142, 25);
            comboBoxRole.TabIndex = 10;
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelRole.Location = new Point(3, 161);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(75, 28);
            labelRole.TabIndex = 11;
            labelRole.Text = "角色：";
            // 
            // AddPersonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 396);
            Controls.Add(tableLayoutPanel1);
            Name = "AddPersonForm";
            Text = "AddMenuForm";
            tableLayoutPanel1.ResumeLayout(false);
            panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelContent.ResumeLayout(false);
            tableLayoutPanelSplit.ResumeLayout(false);
            panelMiddle.ResumeLayout(false);
            panelMiddle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelHead;
        private PictureBox pictureBox1;
        private Panel panelContent;
        private TableLayoutPanel tableLayoutPanelSplit;
        private Label labelTitle;
        private Panel panelMiddle;
        private TextBox textBox1;
        private Label labelGender;
        private Label labelName;
        private Label labelId;
        private Button buttonAdd;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label labelAddress;
        private TextBox textBox4;
        private Label labelRole;
        private ComboBox comboBoxRole;
    }
}