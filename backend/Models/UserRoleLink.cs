using System.Collections.Generic;

namespace backend.Models
{
    public class UserRoleLink
    {
        public string userName { get; set; }
        public List<string> roles { get; set; } = new List<string>();
    }
}