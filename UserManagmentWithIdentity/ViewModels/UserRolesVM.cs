using System.Collections.Generic;

namespace UserManagmentWithIdentity.ViewModels
{
    public class UserRolesVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public List<RoleVM> Roles { get; set; }
    }
}
