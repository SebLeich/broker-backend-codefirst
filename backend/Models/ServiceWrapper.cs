using System;

namespace backend.Models
{
    public class ServiceWrapper
    {
        public int Id { get; set; }
        public string Discriminator { get; set; }
        public string ServiceName { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastModified { get; set; }
    }
}