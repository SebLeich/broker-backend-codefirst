using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    /// <summary>
    /// the class contains the certificate model
    /// </summary>
    public class Certificate
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CertificateName { get; set; }

        public virtual ICollection<ServiceCertificate> ServiceCertificates { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        public Certificate()
        {
            ServiceCertificates = new HashSet<ServiceCertificate>();
            Projects = new HashSet<Project>();
        }
    }
}