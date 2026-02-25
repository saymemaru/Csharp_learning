using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageSystem.EF6
{
    [Table("PersonT")]
    public class PersonTModel
    {
        [Key] // 指定主键
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //取消int类型的默认自增
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }



        [NotMapped] // 不映射到数据库
        public string RoleName 
        { 
            get 
            { 
                string name = ""; 
                switch(RoleId)
                {
                    case 1:
                        name = "新兵";
                        return name;
                    case 2:
                        name = "老兵";
                        return name;
                    case 3:
                        name = "管理员";
                        return name;
                    default:
                        name = "未知人员";
                        return name;
                }
            } 
        }
    }
}
