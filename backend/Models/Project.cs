using backend.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Project
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        
        public virtual SearchVector SearchVector { get; set; }

        public virtual List<MatchingResponse> MatchingResponse { get; set; }

        public Project()
        {

        }
    }
}