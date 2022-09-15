using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
    public partial class Object
    {
        public Object()
        {
           // Narabotkas = new HashSet<Narabotka>();
           // Requests = new HashSet<Request>();
           // Telemetries = new HashSet<Telemetry>();
           // TrainingResults = new HashSet<TrainingResult>();
        }

        public int Kod { get; set; }
        public string? Name { get; set; }
        public float? TekNar { get; set; }
        public DateTime? LastTo { get; set; }
        public int? ToTime { get; set; }
        public float? ToNar { get; set; }
        public float? PlanYear { get; set; }
        public float? Koef2 { get; set; }
        public float? Koef1 { get; set; }
        public float? SredNar { get; set; }
        public DateTime? DateFrom { get; set; }
        public float? NarFrom { get; set; }
        public DateTime? NextTo { get; set; }
        public float? SredNarPlan { get; set; }
        public int? Status { get; set; }
        public int Id { get; set; }
        public DateTime? TrainingFrom { get; set; }
        public DateTime? TrainingTo { get; set; }
        public int? WarningType { get; set; }
        public DateTime? WarningTime { get; set; }
        public DateTime? WarningFrom { get; set; }
        public int? WarningSensor { get; set; }
         public int? ErrorPeriod { get; set; }
        public float? ErrorRate { get; set; }

      /*  public virtual ICollection<Narabotka> Narabotkas { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Telemetry> Telemetries { get; set; }
        public virtual ICollection<TrainingResult> TrainingResults { get; set; }*/
    }
}
