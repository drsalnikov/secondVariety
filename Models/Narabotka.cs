using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
    public partial class Narabotka
    {
        public int? KodObject { get; set; }
        public DateTime Data { get; set; }
        public float? Val { get; set; }
        public int Id { get; set; }

      //  public virtual Object? KodObjectNavigation { get; set; }
    }
}
