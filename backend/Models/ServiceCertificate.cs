using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the service certificate model
    /// </summary>
    public class ServiceCertificate
    {
        [Key, Column(Order = 0)]
        public int ServiceId { get; set; }
        [Key, Column(Order = 1)]
        public int CertificateId { get; set; }

        public Service Service { get; set; }
        public Certificate Certificate { get; set; }
    }
}