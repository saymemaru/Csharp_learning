using ManageSystem.EF6;
using ManageSystem.Model;
using ManageSystem.MyControls;
using ManageSystem.Pages;
using ManageSystem.Properties;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManageSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuuc2_ControlsClick(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            HomePage homePage = new();
            panelContent.Controls.Add(homePage);
        }


        // 反射获取Bitmap资源
        public Bitmap? GetBitmapFromRes(string name)
        {
            Type type = typeof(Resources);
            PropertyInfo? propertyInfo = type.GetProperty(name, BindingFlags.Static | BindingFlags.NonPublic);
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(Bitmap))
            {
                return (Bitmap?)propertyInfo.GetValue(null);
            }
            return null;
        }

        /// <summary>
        /// 按数据库信息重新加载menu选项
        /// </summary>
        public void LoadMenu()
        {
            //清空
            flowLayoutPanelMenu.Controls.Clear();

            // 根据db加载menu选项
            using (AppDbContext? db = new AppDbContext())
            {
                List<MenuTModel> menus = db.Menus.ToList();
                //MessageBox.Show(db.menus.ToList().Count.ToString());
                foreach (MenuTModel menuTModel in menus)
                {
                    MenuUC menuUC = new MenuUC();
                    menuUC.MenuText = menuTModel.MenuText;
                    menuUC.MenuImage = GetBitmapFromRes(menuTModel.MenuImage);
                    menuUC.ControlsClick += (newSender, newe) =>
                    {
                        LoadPage(menuTModel.MenuPage);
                    };

                    flowLayoutPanelMenu.Controls.Add(menuUC);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMenu();
            pageDic = new();
        }

        // page字典
        private Dictionary<string, UserControl> pageDic;
        private void LoadPage(string pageName)
        {
            // 当page在当前程序集内，通过反射获取页面类型
            Assembly? assembly = Assembly.GetExecutingAssembly();
            string fullTypeName = $"{this.GetType().Namespace}.Pages.{pageName}";
            Type? pageType = assembly.GetType(fullTypeName);

            if (pageType == null)
            {
                MessageBox.Show($"未找到page：{fullTypeName}");
                return;
            }
            // 是否已有page
            if (pageDic.ContainsKey(pageName) && pageDic[pageName] != null)
            {
                AddPage(pageDic[pageName]);
                return;
            }
            // 生成page实例
            var page = Activator.CreateInstance(pageType) as UserControl;
            if (page == null)
            {
                MessageBox.Show($"无法创建page：{pageName}");
                return;
            }
            // 添加到字典
            pageDic[pageName] = page;

            // 添加页面
            AddPage(page);
        }

        // 设置page属性，添加page到panelContent
        private void AddPage(UserControl page)
        {
            panelContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
        }

        private void MigrateFromTXT()
        {
            string path = @"E:\work\C#\winform\demo1\管理系统\TestMenu.txt";
            string menuContext = File.ReadAllText(path);
            List<MenuModel>? menuModels = JsonSerializer.Deserialize<List<MenuModel>>(menuContext);

            List<MenuTModel> menuTModels = new();
            foreach (MenuModel item in menuModels)
            {
                MenuTModel menuTModel = new MenuTModel();
                menuTModel.MenuText = item.MenuText;
                menuTModel.MenuImage = item.MenuImage;
                menuTModel.MenuPage = item.MenuPage;

                menuTModels.Add(menuTModel);
            }

            using (var db = new AppDbContext())
            {
                //添加到数据库
                db.Menus.AddRange(menuTModels);
                db.SaveChanges();
            }
        }
    }
}
