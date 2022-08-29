using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
  public partial class Telemetry
  {
    [Key]
    public int id { get; set; }
    public int type { get; set; }

    public DateTime period { get; set; }

    public float value { get; set; }

    public int kod_object { get; set; }
  }
}
