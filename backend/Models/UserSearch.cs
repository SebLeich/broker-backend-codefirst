using backend.Core;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class UserSearch
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        [ForeignKey(nameof(User))]
        public Nullable<Guid> UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}