using ManageSystem.EF6;
using ManageSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSystem.Pages
{
    public partial class PermissionPage : UserControl
    {
        public PermissionPage()
        {
            InitializeComponent();

            comboBoxRoleSelect.DataSource = Enum.GetValues(typeof(RoleEnum));

            using (var db = new AppDbContext())
            {
                List<MenuTModel> models = db.Menus.ToList();
                foreach (var model in models)
                {
                    MenuFunctionPage menuFunction = new MenuFunctionPage(model);
                    //menuFunction.Dock = DockStyle.Top;
                    flowLayoutPanelContent.Controls.Add(menuFunction);
                }
            }

            LoadDBPermissions();
        }

        void LoadDBPermissions()
        {
            int roleId = (int)(RoleEnum)comboBoxRoleSelect.SelectedValue;
            List<PermissionTModel> permissionTModels = new();
            //加载数据库内容
            using (AppDbContext db = new())
            {
                //匹配RoleId
                permissionTModels = db.Permissions.Where(x => x.RoleId == roleId).ToList();
            }

            foreach (var page in flowLayoutPanelContent.Controls.OfType<MenuFunctionPage>())
            {
                page.LoadPermissions(permissionTModels);
            }
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            int roleId = (int)(RoleEnum)comboBoxRoleSelect.SelectedValue;

            List<PermissionTModel> models = new();

            foreach (var page in flowLayoutPanelContent.Controls.OfType<MenuFunctionPage>())
            {
                (string? menuPage, List<string>? functions) = page.GetMenuPagesAndFunctions();

                PermissionTModel model = new()
                {
                    RoleId = roleId,
                    MenuPage = menuPage,
                    Functions = string.Join(",", functions)
                };
                models.Add(model);
            }

            using (AppDbContext db = new())
            {
                //清除旧的数据
                List<PermissionTModel> oldModels = db.Permissions.Where(x => x.RoleId == roleId).ToList();
                db.Permissions.RemoveRange(oldModels);
                db.SaveChanges();

                db.Permissions.AddRange(models);
                db.SaveChanges();
            }

        }

        private void comboBoxRoleSelect_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDBPermissions();
        }
    }
}
