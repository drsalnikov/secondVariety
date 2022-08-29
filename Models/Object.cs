using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
    public partial class Object
    {
        [Key]
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
    }

  public partial class ObjectDTO
  {
    public int Kod { get; set; }
    public string Name { get; set; }
    public float TekNar { get; set; }
    public DateTime LastTo { get; set; }
    public int ToTime { get; set; }
    public float ToNar { get; set; }
    public float PlanYear { get; set; }
    public float Koef2 { get; set; }
    public float Koef1 { get; set; }
    public float SredNar { get; set; }
    public DateTime DateFrom { get; set; }
    public float NarFrom { get; set; }
    public DateTime NextTo { get; set; }

    public ObjectDTO(SecondVariety.Models.Object baseObject)
    {
      Kod = baseObject.Kod;
      Name = !String.IsNullOrEmpty(baseObject.Name) ? baseObject.Name as String : "" ;
      TekNar = baseObject.TekNar.HasValue ? baseObject.TekNar.Value : 0;
      LastTo = baseObject.LastTo.HasValue ? baseObject.LastTo.Value : DateTime.MinValue;
      ToTime = baseObject.ToTime.HasValue ? baseObject.ToTime.Value : 0;
      ToNar = baseObject.ToNar.HasValue ? baseObject.ToNar.Value : 0;
      PlanYear = baseObject.PlanYear.HasValue ? baseObject.PlanYear.Value : 0;
      Koef1 = baseObject.Koef1.HasValue ? baseObject.Koef1.Value : 0;
      Koef2 = baseObject.Koef2.HasValue ? baseObject.Koef2.Value : 0;
      SredNar = baseObject.SredNar.HasValue ? baseObject.SredNar.Value : 0;
      DateFrom = baseObject.DateFrom.HasValue ? baseObject.DateFrom.Value : DateTime.MinValue;
      NextTo = baseObject.NextTo.HasValue ? baseObject.NextTo.Value : DateTime.MinValue;
      NarFrom = baseObject.NarFrom.HasValue ? baseObject.NarFrom.Value : 0;
    }

    public ObjectDTO()
    {
      Kod = 0;
      Name = "";
      TekNar = 0;
      LastTo = DateTime.MinValue;
      ToTime = 0;
      ToNar = 0;
      PlanYear = 0;
      Koef1 = 0;
      Koef2 = 0;
      SredNar = 0;
      DateFrom = DateTime.MinValue;
      NextTo = DateTime.MinValue;
      NarFrom = 0;
    }

  }
}
