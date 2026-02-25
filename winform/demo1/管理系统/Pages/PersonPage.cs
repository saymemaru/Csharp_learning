using ManageSystem.EF6;
using ManageSystem.Properties;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem.Pages
{
    public partial class PersonPage : UserControl
    {
        public PersonPage()
        {
            InitializeComponent();
            SetDataGridViewStyle(dataGridView1);
        }

        private void MenuPage_Load(object sender, EventArgs e)
        {
            LoadDBPersons();
        }
        public void LoadDBPersons()
        {
            //加载数据库内容
            using (AppDbContext db = new())
            {
                List<PersonTModel> personTModels = db.Persons.ToList();

                // 为 dataGridView 组件添加数据源
                dataGridView1.DataSource = personTModels;
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

            // 单元格格式
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        // 添加记录菜单 按钮
        private void buttonAddMenu_Click(object sender, EventArgs e)
        {
            AddPersonForm addPersonForm = new(this);
            addPersonForm.ShowDialog();
        }

        //操作栏图标长宽
        private readonly int imageWidth = 32;
        private readonly int imageHeight = 32;

        // 绘制事件 
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 操作栏绘制
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

        // 单元格点击事件
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

                string personId = dataGridView1.Rows[e.RowIndex].Cells["ColPersonID"].Value.ToString();
                // 是否在第一个图标 编辑
                if (xInCell > imageLeftStart && xInCell < imageLeftStart + imageWidth)
                {
                    EditPersonForm editPersonForm = new(this, personId);
                    editPersonForm.ShowDialog();
                    return;
                }
                // 是否在第二个图标 删除
                if (xInCell > imageLeftStart + imageWidth && xInCell < imageLeftStart + imageWidth * 2)
                {
                    DialogResult dialogResult = MessageBox.Show($"是否要删除【{dataGridView1.Rows[e.RowIndex].Cells["ColPersonID"].Value.ToString()}】",
                        "警告", MessageBoxButtons.YesNo);

                    // 不删除
                    if (dialogResult != DialogResult.Yes)
                        return;

                    // 删除
                    using (AppDbContext db = new())
                    {
                        PersonTModel personTModel = new();
                        personTModel.PersonId = int.Parse(personId);
                        db.Persons.Attach(personTModel);
                        db.Persons.Remove(personTModel);
                        db.SaveChanges();
                    }

                    //重新加载数据库
                    LoadDBPersons();
                }
            }
        }
        private void buttonImport_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.Environment.CurrentDirectory;
            dialog.Filter = "打开表格文件|*.xls|表格文件|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //获取选择的文件路径
                var filePath = dialog.FileName;
                List<PersonTModel> personTModels = ReadExcelToPersons(filePath);
                using (AppDbContext db = new())
                {
                    //添加personId
                    PersonTModel? model = db.Persons.OrderByDescending(x => x.PersonId).FirstOrDefault();
                    //已有数据，从最大序号开始累加
                    if(model != null)
                    {
                        int maxPersonId = model.PersonId;
                        foreach(var item in personTModels)
                        {
                            maxPersonId++;
                            item.PersonId = maxPersonId;
                        }
                    }
                    //无数据，从0开始累加
                    else
                    {
                        int maxPersonId = 0;
                        foreach (var item in personTModels)
                        {
                            maxPersonId++;
                            item.PersonId = maxPersonId;
                        }
                    }

                    db.Persons.AddRange(personTModels);
                    db.SaveChanges();
                }

                //重新加载
                LoadDBPersons();
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存数据文件";
            sfd.Filter = "表格文件 (*.xls)|*.xls|所有文件 (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.FileName = "人员名单.xls";
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fullPath = sfd.FileName; // 完整路径+文件名
                string fileNameOnly = Path.GetFileName(fullPath); // 仅文件名
                string directoryPath = Path.GetDirectoryName(fullPath); // 仅路径
                using (AppDbContext db = new())
                {
                    List<PersonTModel> personTModels = db.Persons.ToList();
                    ExportPersonToExcel(personTModels, fullPath);
                }
            }
        }


        /// <summary>
		/// 读取Excel表格，返回List<PersonnelTModel>模型数据（无PersonId项）
		/// </summary>
		/// <param name="filePath">Excel文件路径</param>
		/// <returns></returns>
		public static List<PersonTModel> ReadExcelToPersons(string filePath)
        {
            List<PersonTModel> list = new List<PersonTModel>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                //读取工作簿对象 .xlsx（Excel 2007+，XSSF） .xls（Excel 2003，HSSF）
                //IWorkbook workbook = new HSSFWorkbook(); // 2003
                IWorkbook workbook = new XSSFWorkbook(fs);
                //读取工作簿对象下的Sheet页面对象
                ISheet sheet = workbook.GetSheetAt(0);

                //循环遍历所有行数据（直接跳过第一行）
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    if (i == 0)
                        continue;

                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    PersonTModel model = new PersonTModel
                    {
                        Name = row.GetCell(0).ToString(),
                        Gender = row.GetCell(1).ToString(),
                        Address = row.GetCell(2).ToString()
                    };

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 导出List<PersonnelTModel>模型数据到Excel
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void ExportPersonToExcel(List<PersonTModel> list, string filePath)
        {
            //创建xlsx格式的工作簿
            IWorkbook workbook = new XSSFWorkbook(); // xlsx
            ISheet sheet = workbook.CreateSheet("Personnel");

            //创建表头
            IRow headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("学号");
            headerRow.CreateCell(1).SetCellValue("姓名");
            headerRow.CreateCell(2).SetCellValue("性别");
            headerRow.CreateCell(3).SetCellValue("地址");

            //已有表头，直接从第二行开始创建
            int i = 1;
            foreach (var model in list)
            {
                IRow row = sheet.CreateRow(i);

                row.CreateCell(0).SetCellValue(model.PersonId);
                row.CreateCell(1).SetCellValue(model.Name);
                row.CreateCell(2).SetCellValue(model.Gender);
                row.CreateCell(3).SetCellValue(model.Address);

                //每次创建一行，i++，下次创建下一行
                i++;
            }

            //写入文件
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fs);
            }
        }
    }
}
