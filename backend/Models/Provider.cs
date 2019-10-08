using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains the provider model
    /// </summary>
    public class Provider
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProviderName { get; set; }

        public virtual ICollection<ProviderPayment> ProviderPayments { get; set; }
    }
}