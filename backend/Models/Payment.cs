using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the payment model
    /// </summary>
    public class Payment
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PaymentType { get; set; }

        public virtual ICollection<ProviderPayment> ProviderPayments { get; set; }
    }
}