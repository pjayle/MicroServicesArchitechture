using System;
using System.Collections.Generic;

namespace service.authapi.Models
{
    public partial class MUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolesId { get; set; }
        //public MRoles? Roles { get; set; }
    }

    public partial class MPermissions
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public bool IsMenu { get; set; }
        public int? SequenceNo { get; set; }
    }
    public partial class RolePermissons
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        //public MPermissions permissions { get; set; }
        //public MRoles mRoles { get; set; }
    }
}
