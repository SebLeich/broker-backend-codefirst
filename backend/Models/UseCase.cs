using backend.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class UseCase
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Creation { get; set; }

        public virtual List<ServiceClass> ServiceClassMapping {get; set; }

        public UseCase()
        {
            ServiceClassMapping = new List<ServiceClass>();
        }
    }

    public class ServiceClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        private ServiceClass(ServiceClassEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        [JsonIgnore]
        public virtual ICollection<UseCase> UseCases { get; set; }

        protected ServiceClass() { }

        public static implicit operator ServiceClass(ServiceClassEnum @enum) => new ServiceClass(@enum);

        public static implicit operator ServiceClassEnum(ServiceClass serviceClass) => (ServiceClassEnum)serviceClass.Id;
    }

    public enum ServiceClassEnum
    {
        BLOCKSTORAGESERVICE, 
        DIRECTATTACHEDSTORAGESERVICE, 
        KEYVALUESTORAGESERVICE, 
        OBJECTSTORAGESERVICE, 
        ONLINEDRIVESTORAGESERVICE,
        RELATIONALDATABASESTORAGESERVICE
    }
}