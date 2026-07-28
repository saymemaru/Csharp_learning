using ManageSystem.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem
{
    internal class UserState
    {
        private static readonly Lazy<UserState> instance =
            new Lazy<UserState>(() => new UserState());

        private UserState()
        {
            
        }
        public static UserState Instance => instance.Value;

        public PersonTModel CurrentLoginedUser { get; set; }

        public List<PermissionTModel> CurrentUserPermission {  get; set; }

        public Form1 CurrentIndexForm { get; set; }
    }
}
