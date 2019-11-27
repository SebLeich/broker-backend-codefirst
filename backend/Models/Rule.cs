using backend.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Rule
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RuleCode { get; set; }

        public string RuleTitle { get; set; }

        public string RuleDesc { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();

        public Rule()
        {

        }
    }
}