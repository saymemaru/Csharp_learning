using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem.EF6
{
    [Table("MenuT")] //db 中显示的表名
    public class MenuTModel
    {
        public int Id { get; set; }
        public string MenuText { get; set; }
        public string MenuImage { get; set; }
        public string MenuPage { get; set; }
    }
}
