using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem.Model
{
    internal class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }

    public enum RoleEnum
    {
        新兵 = 1,
        老兵 = 2,
        管理员 = 3
    }
}
