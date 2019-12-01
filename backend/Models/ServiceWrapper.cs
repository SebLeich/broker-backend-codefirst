using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class ServiceWrapper
    {
        public int Id { get; set; }
        public string Discriminator { get; set; }
        public string ServiceName { get; set; }
    }
}