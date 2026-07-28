using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem.EF6
{
    [Table("PermissionT")]
    public class PermissionTModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string MenuPage { get; set; }

        /// <summary>
        /// 菜单功能
        /// </summary>
        public string Functions { get; set; }
    }
}
