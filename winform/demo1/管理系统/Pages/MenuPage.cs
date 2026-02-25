using ManageSystem.EF6;
using ManageSystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem.Pages
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
            SetDataGridViewStyle(dataGridView1);
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            LoadDBMenus();
        }
        public void LoadDBMenus()
        {
            //加载数据库内容
            using (AppDbContext db = new())
            {
                List<MenuTModel> menuTModels = db.Menus.ToList();

                // 为 dataGridView 组件添加数据源
                dataGridView1.DataSource = menuTModels;
            }
        }

        private void SetDataGridViewStyle(DataGridView dataGridView)
        {
            // 根据数据自动生成列
            dataGridView.AutoGenerateColumns = false;

            // 行头隐藏
            dataGridView.RowHeadersVisible = false;

            // 列头高度模式设置,手动设置高度
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.ColumnHeadersHeight = 30;
            // 列头居中
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 列尺寸模式
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行高
            dataGridView.RowTemplate.Height = 32;

            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 单元格
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        // 添加记录菜单 按钮
        private void buttonAddMenu_Click(object sender, EventArgs e)
        {
            AddMenuForm addMenuForm = new AddMenuForm(this);
            addMenuForm.ShowDialog();
        }

        //操作栏图标长宽
        private readonly int imageWidth = 32;
        private readonly int imageHeight = 32;
        //绘制事件
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                //e.CellStyle.BackColor = Color.Gray;
                //绘制背景色，是否保留选择颜色
                e.PaintBackground(e.CellBounds, true);

                //单元格图片左侧起点
                int imageLeftStart = e.CellBounds.Left + e.CellBounds.Width / 2 - imageWidth;

                //绘制图片 x2
                e.Graphics.DrawImage(Resources.BtEdit,
                    new Rectangle(imageLeftStart, e.CellBounds.Top, imageWidth, imageHeight));
                e.Graphics.DrawImage(Resources.BtDisband,
                    new Rectangle(imageLeftStart + imageWidth, e.CellBounds.Top, imageWidth, imageHeight));

                //阻止其他的绘制事件，否则会覆盖已绘制的内容
                e.Handled = true;
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 是否是操作栏
            if (e.ColumnIndex == dataGridView1.Columns["ColOperation"].Index && e.RowIndex >= 0)
            {
                //client中鼠标位置
                Point position = dataGridView1.PointToClient(Cursor.Position);
                //单元格长方形信息（client中的坐标）
                Rectangle rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                //单元格中x坐标
                int xInCell = position.X - rect.X;
                //单元格图片左侧起点
                int imageLeftStart = rect.Width / 2 - imageWidth;

                string menuId = dataGridView1.Rows[e.RowIndex].Cells["ColID"].Value.ToString();
                // 是否在第一个图标 编辑
                if (xInCell > imageLeftStart && xInCell < imageLeftStart + imageWidth)
                {
                    EditMenuForm editMenuForm = new(this, menuId);
                    editMenuForm.ShowDialog();
                    return;
                }
                // 是否在第二个图标 删除
                if (xInCell > imageLeftStart + imageWidth && xInCell < imageLeftStart + imageWidth * 2)
                {
                    DialogResult dialogResult = MessageBox.Show($"是否要删除【{dataGridView1.Rows[e.RowIndex].Cells["ColMenuText"].Value.ToString()}】",
                        "警告",MessageBoxButtons.YesNo);

                    // 不删除
                    if (dialogResult != DialogResult.Yes)
                        return;

                    // 删除
                    using (AppDbContext db = new())
                    {
                        MenuTModel model = new ();
                        model.Id = int.Parse(menuId);
                        db.Menus.Attach(model);
                        db.Menus.Remove(model);
                        db.SaveChanges();
                    }
                    LoadDBMenus();
                }  
            }
        }
    }
}
