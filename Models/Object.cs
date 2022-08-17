using System;
using System.Collections.Generic;

namespace SecondVariety.Models
{
    public partial class Object
    {
        public int? Kod { get; set; }
        public string? Name { get; set; }
        public float? TekNar { get; set; }
        public DateOnly? LastTo { get; set; }
        public int? ToTime { get; set; }
        public float? ToNar { get; set; }
        public float? PlanYear { get; set; }
        public float? Koef2 { get; set; }
        public float? Koef1 { get; set; }
        public float? SredNar { get; set; }
        public DateOnly? DateFrom { get; set; }
        public float? NarFrom { get; set; }
        public DateOnly? NextTo { get; set; }
    }

  public partial class MyObject
  {
    public int Kod { get; set; }
    public string Name { get; set; }
    public float TekNar { get; set; }
    public DateOnly LastTo { get; set; }
    public float ToTime { get; set; }
    public float ToNar { get; set; }
    public float PlanYear { get; set; }
    public float Koef2 { get; set; }
    public float Koef1 { get; set; }
    public float SredNar { get; set; }
    public DateOnly DateFrom { get; set; }
    public float NarFrom { get; set; }
    public DateOnly NextTo { get; set; }

    public MyObject(SecondVariety.Models.Object baseObject)
    {
      Kod = baseObject.Kod.HasValue ? baseObject.Kod.Value : 0;
      Name = !String.IsNullOrEmpty(baseObject.Name) ? baseObject.Name as String : "" ;
      TekNar = baseObject.TekNar.HasValue ? baseObject.TekNar.Value : 0;
      LastTo = baseObject.LastTo.HasValue ? baseObject.LastTo.Value : DateOnly.MinValue;
      ToTime = baseObject.ToTime.HasValue ? baseObject.ToTime.Value : 0;
      ToNar = baseObject.ToNar.HasValue ? baseObject.ToNar.Value : 0;
      PlanYear = baseObject.PlanYear.HasValue ? baseObject.PlanYear.Value : 0;
      Koef1 = baseObject.Koef1.HasValue ? baseObject.Koef1.Value : 0;
      Koef2 = baseObject.Koef2.HasValue ? baseObject.Koef2.Value : 0;
      SredNar = baseObject.SredNar.HasValue ? baseObject.SredNar.Value : 0;
      DateFrom = baseObject.DateFrom.HasValue ? baseObject.DateFrom.Value : DateOnly.MinValue;
      NextTo = baseObject.NextTo.HasValue ? baseObject.NextTo.Value : DateOnly.MinValue;
      NarFrom = baseObject.NarFrom.HasValue ? baseObject.NarFrom.Value : 0;
    }

    public MyObject()
    {
      Kod = 0;
      Name = "";
      TekNar = 0;
      LastTo = DateOnly.MinValue;
      ToTime = 0;
      ToNar = 0;
      PlanYear = 0;
      Koef1 = 0;
      Koef2 = 0;
      SredNar = 0;
      DateFrom = DateOnly.MinValue;
      NextTo = DateOnly.MinValue;
      NarFrom = 0;
    }

  }
}
