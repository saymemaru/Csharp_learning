namespace pointcloud
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
            groupBox1 = new GroupBox();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            textBox1 = new TextBox();
            groupBox3 = new GroupBox();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(996, 447);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "3D Display Box";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // panel1
            // 
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(984, 425);
            panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox1);
            groupBox2.Location = new Point(12, 465);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(996, 125);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Data Box";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.MenuText;
            textBox1.ForeColor = SystemColors.Window;
            textBox1.Location = new Point(0, 22);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(990, 97);
            textBox1.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button9);
            groupBox3.Controls.Add(button8);
            groupBox3.Controls.Add(button7);
            groupBox3.Controls.Add(button6);
            groupBox3.Controls.Add(button5);
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(button1);
            groupBox3.Location = new Point(1014, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(233, 578);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Button Box";
            groupBox3.Enter += groupBox3_Enter;
            // 
            // button9
            // 
            button9.Location = new Point(6, 422);
            button9.Name = "button9";
            button9.Size = new Size(130, 44);
            button9.TabIndex = 8;
            button9.Text = "test";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button10_Click;
            // 
            // button8
            // 
            button8.Location = new Point(6, 372);
            button8.Name = "button8";
            button8.Size = new Size(130, 44);
            button8.TabIndex = 7;
            button8.Text = "button8";
            button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(6, 322);
            button7.Name = "button7";
            button7.Size = new Size(130, 44);
            button7.TabIndex = 6;
            button7.Text = "显示匹配结果";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new Point(6, 272);
            button6.Name = "button6";
            button6.Size = new Size(130, 44);
            button6.TabIndex = 5;
            button6.Text = "加载背景文件";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(6, 222);
            button5.Name = "button5";
            button5.Size = new Size(130, 44);
            button5.TabIndex = 4;
            button5.Text = "三角化/显示模板";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(6, 172);
            button4.Name = "button4";
            button4.Size = new Size(130, 44);
            button4.TabIndex = 3;
            button4.Text = "显示去噪对齐结果";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(6, 122);
            button3.Name = "button3";
            button3.Size = new Size(130, 44);
            button3.TabIndex = 2;
            button3.Text = "显示模板点云模型";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(6, 72);
            button2.Name = "button2";
            button2.Size = new Size(130, 44);
            button2.TabIndex = 1;
            button2.Text = "模板点云阈值切割";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(6, 22);
            button1.Name = "button1";
            button1.Size = new Size(130, 44);
            button1.TabIndex = 0;
            button1.Text = "加载点云文件";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1259, 602);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Panel panel1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox textBox1;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button9;
        private Button button8;
        private Button button7;
    }
}
