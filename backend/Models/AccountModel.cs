using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
    }
}