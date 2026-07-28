using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem.Model
{
    public class MenuModel
    {
        public string MenuText { get; set; }
        public string MenuImage { get; set; }
        public string MenuPage { get; set; }
    }

    public enum MenuFunctionEnum
    {
        增加,
        修改,
        查看,
        删除,
        Excel导入,
        Excel导出
    }
}
