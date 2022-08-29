using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SecondVariety.Models
{
    public partial class Narabotka
    {
        [Key]
        public int id { get; set; }
        public int? KodObject { get; set; }
        public DateTime Data { get; set; }
        public float? Val { get; set; }
    }
}
