using System;
using System.Collections.Generic;

namespace grpcserv1
{
    public partial class TrainingResult
    {
        public int Id { get; set; }
        public int? KodObject { get; set; }
        public float? MinVal { get; set; }
        public float? MaxVal { get; set; }
        public float? MedVal { get; set; }
        public float? GradVal { get; set; }
        public int? Type { get; set; }

        public virtual Object? KodObjectNavigation { get; set; }
    }
}
