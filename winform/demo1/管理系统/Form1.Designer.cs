namespace ManageSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelContainer = new TableLayoutPanel();
            tableLayoutPanelContent = new TableLayoutPanel();
            flowLayoutPanelMenu = new FlowLayoutPanel();
            panelContent = new Panel();
            panelHead = new Panel();
            pictureBoxTitle = new PictureBox();
            tableLayoutPanelContainer.SuspendLayout();
            tableLayoutPanelContent.SuspendLayout();
            panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTitle).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelContainer
            // 
            tableLayoutPanelContainer.BackColor = Color.FromArgb(255, 128, 0);
            tableLayoutPanelContainer.ColumnCount = 1;
            tableLayoutPanelContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelContainer.Controls.Add(tableLayoutPanelContent, 0, 1);
            tableLayoutPanelContainer.Controls.Add(panelHead, 0, 0);
            tableLayoutPanelContainer.Dock = DockStyle.Fill;
            tableLayoutPanelContainer.Location = new Point(0, 0);
            tableLayoutPanelContainer.Name = "tableLayoutPanelContainer";
            tableLayoutPanelContainer.RowCount = 3;
            tableLayoutPanelContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanelContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelContainer.Size = new Size(1141, 661);
            tableLayoutPanelContainer.TabIndex = 0;
            // 
            // tableLayoutPanelContent
            // 
            tableLayoutPanelContent.BackColor = Color.Snow;
            tableLayoutPanelContent.ColumnCount = 2;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Controls.Add(flowLayoutPanelMenu, 0, 0);
            tableLayoutPanelContent.Controls.Add(panelContent, 1, 0);
            tableLayoutPanelContent.Dock = DockStyle.Fill;
            tableLayoutPanelContent.Location = new Point(0, 80);
            tableLayoutPanelContent.Margin = new Padding(0);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.RowCount = 1;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Size = new Size(1141, 561);
            tableLayoutPanelContent.TabIndex = 0;
            // 
            // flowLayoutPanelMenu
            // 
            flowLayoutPanelMenu.BackColor = Color.Silver;
            flowLayoutPanelMenu.Dock = DockStyle.Fill;
            flowLayoutPanelMenu.Location = new Point(0, 0);
            flowLayoutPanelMenu.Margin = new Padding(0);
            flowLayoutPanelMenu.Name = "flowLayoutPanelMenu";
            flowLayoutPanelMenu.Size = new Size(240, 561);
            flowLayoutPanelMenu.TabIndex = 0;
            // 
            // panelContent
            // 
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(240, 0);
            panelContent.Margin = new Padding(0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(901, 561);
            panelContent.TabIndex = 1;
            // 
            // panelHead
            // 
            panelHead.Controls.Add(pictureBoxTitle);
            panelHead.Dock = DockStyle.Fill;
            panelHead.Location = new Point(0, 0);
            panelHead.Margin = new Padding(0);
            panelHead.Name = "panelHead";
            panelHead.Size = new Size(1141, 80);
            panelHead.TabIndex = 1;
            // 
            // pictureBoxTitle
            // 
            pictureBoxTitle.Image = Properties.Resources.airborne;
            pictureBoxTitle.Location = new Point(0, 3);
            pictureBoxTitle.Name = "pictureBoxTitle";
            pictureBoxTitle.Size = new Size(154, 74);
            pictureBoxTitle.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTitle.TabIndex = 1;
            pictureBoxTitle.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(1141, 661);
            Controls.Add(tableLayoutPanelContainer);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tableLayoutPanelContainer.ResumeLayout(false);
            tableLayoutPanelContent.ResumeLayout(false);
            panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTitle).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelContainer;
        private TableLayoutPanel tableLayoutPanelContent;
        private FlowLayoutPanel flowLayoutPanelMenu;
        private PictureBox pictureBoxTitle;
        private Panel panelContent;
        private MyControls.MenuUC menuuc1;
        private MyControls.MenuUC menuuc2;
        private MyControls.MenuUC menuuc3;
        private Panel panelHead;
    }
}
