using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem.MyControls
{
    public partial class MenuUC : UserControl
    {
        public event EventHandler? ControlsClick;
        public MenuUC()
        {
            InitializeComponent();

            RegisterControlsEvent();
        }

        private void RegisterControlsEvent()
        {
            this.Click += AllControlsClick;

            this.MouseEnter += MenuUC_MouseEnter;
            this.MouseLeave += MenuUC_MouseLeave;

            foreach (Control control in this.Controls)
            {
                control.Click += AllControlsClick;

                control.MouseEnter += MenuUC_MouseEnter;
                control.MouseLeave += MenuUC_MouseLeave;
            }
        }

        private void AllControlsClick(object? sender, EventArgs e)
        {
            this.BackColor = MenuMousePressedColor;
            ControlsClick?.Invoke(sender, e);
        }

        private void MenuUC_MouseEnter(object? sender, EventArgs e)
        {
            this.BackColor = MenuMouseEnterColor;
        }

        private void MenuUC_MouseLeave(object? sender, EventArgs e)
        {
            this.BackColor = MenuBaseColor;
        }

        [Category("Content")]
        [Browsable(true)]
        [Description("设置显示的文字")]
        public string MenuText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        [Category("Content")]
        [Description("设置显示的图标")]
        public Image MenuImage
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Description("基础背景颜色")]
        public Color MenuBaseColor { get; set; } = Color.Black;

        [DefaultValue(typeof(Color), "Gray")]
        [Description("鼠标进入背景颜色")]
        public Color MenuMouseEnterColor { get; set; } = Color.Gray;

        [DefaultValue(typeof(Color), "DimGray")]
        [Description("鼠标点击背景颜色")]
        public Color MenuMousePressedColor { get; set; } = Color.DimGray;

    }
}
