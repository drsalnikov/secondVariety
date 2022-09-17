using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecondVariety.Models
{
    public partial class Request
    {
        public int Num { get; set; }
        public DateTime? Data { get; set; }
        public int? KodObject { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Status { get; set; }
        public DateTime? DateFromFakt { get; set; }
        public DateTime? DateToFakt { get; set; }
        public string? Comment { get; set; }
        public long Id { get; set; }

      //  public virtual Object? KodObjectNavigation { get; set; }
    }

    public partial class RequestDTO
    {
      public int? Num { get; set; }
      public DateTime? Data { get; set; }
      public int? KodObject { get; set; }
      public string NameObject { get; set; }
      public DateTime? DateFrom { get; set; }
      public DateTime? DateTo { get; set; }
      public StatusTypes Status { get; set; }
      public DateTime? DateFromFakt { get; set; }
      public DateTime? DateToFakt { get; set; }
      public string? Comment { get; set; }
      public long Id { get; set; }
    }

    public enum StatusTypes : int
    {
      [Display(Description = "Закрыта", Name = "asda ")]
      Closed,
      [Display(Description = "Согласована")]
      Agreed,
      [Display(Description = "На согласовании")]
      ToBeAgreed,
      [Display(Description = "В работе")]
      InWork
    }
}
