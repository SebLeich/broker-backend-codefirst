using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class RoleRuleLink
    {
        public Guid RoleId { get; set; }
        public RoleModel Role { get; set; }
        public int RuleId { get; set; }
        public List<Rule> Rules { get; set; }
        public bool IsAllowed { get; set; }
    }
}