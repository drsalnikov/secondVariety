using System;
using System.Collections.Generic;

namespace SecondVariety.Models
{
    public partial class Request
    {
        public int? Num { get; set; }
        public DateOnly? Data { get; set; }
        public int? KodObject { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public int? Status { get; set; }
        public DateOnly? DateFromFakt { get; set; }
        public DateOnly? DateToFakt { get; set; }
        public string? Comment { get; set; }
    }
}
