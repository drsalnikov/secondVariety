using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
    public partial class Telemetry
    {
        public int Type { get; set; }
        public DateTime? Period { get; set; }
        public float? Value { get; set; }
        public int Id { get; set; }
        public int? KodObject { get; set; }

        //public virtual Object? KodObjectNavigation { get; set; }
    }
}
