using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the n:m relation between a provider and a payment
    /// </summary>
    public class ProviderPayment
    {
        [Key, Column(Order = 0)]
        public int ProviderId { get; set; }
        [Key, Column(Order = 1)]
        public int PaymentId { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual Payment Payment { get; set; }
    }
}