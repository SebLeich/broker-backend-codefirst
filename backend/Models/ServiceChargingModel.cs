﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains the service charging model
    /// </summary>
    public class ServiceChargingModel
    {
        [Key, Column(Order = 0)]
        public int ServiceId { get; set; }
        [Key, Column(Order = 1)]
        public int ChargingModelId { get; set; }

        public Service Service { get; set; }
        public ChargingModel ChargingModel { get; set; }
    }
}