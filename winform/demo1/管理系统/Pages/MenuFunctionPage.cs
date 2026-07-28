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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManageSystem.Pages
{
    public partial class MenuFunctionPage : UserControl
    {
        public MenuTModel CurrentMenuTModel { get; }
        public MenuFunctionPage(MenuTModel model)
        {
            InitializeComponent();
            CurrentMenuTModel = model;

            labelMenuTitle.Text = CurrentMenuTModel.MenuText;

            foreach (var item in Enum.GetValues(typeof(MenuFunctionEnum)))
            {
                flowLayoutPanelContent.Controls.Add(
                    new CheckBox() { Text = item.ToString() });
            }
        }

        /// <summary>
        /// 获取控件代表的menuPage和function字符串
        /// </summary>
        /// <returns></returns>
        public (string, List<string>) GetMenuPagesAndFunctions()
        {
            string menuPage = CurrentMenuTModel.MenuPage;

            List<string> functions = new List<string>();

            foreach (var checkBox in flowLayoutPanelContent.Controls.OfType<CheckBox>())
            {
                if (checkBox.Checked)
                {
                    string text = checkBox.Text;
                    functions.Add(text);
                }
            }
            return (menuPage, functions);
        }

        /// <summary>
        /// 载入permission信息，显示在checkbox
        /// </summary>
        /// <param name="models"></param>
        public void LoadPermissions(IEnumerable<PermissionTModel> models)
        {
            //匹配MenuPage
            PermissionTModel currentPageModels = models
                .Where(x => x.MenuPage == CurrentMenuTModel.MenuPage)
                .First();
            foreach (CheckBox checkBox in flowLayoutPanelContent.Controls.OfType<CheckBox>())
                checkBox.Checked = currentPageModels.Functions.Contains(checkBox.Text);
        }
    }
}
