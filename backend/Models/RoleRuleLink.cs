using System.Collections.Generic;

namespace backend.Models
{
    public class RoleRuleLink
    {
        public List<RoleModel> Roles { get; set; } = new List<RoleModel>();
        public int RuleId { get; set; }
        public Rule Rule { get; set; }
        public bool IsAllowed { get; set; }
    }
}