using System;
using System.Collections.Generic;

namespace grpcserv1
{
    public partial class Narabotka
    {
        public int? KodObject { get; set; }
        public DateOnly Data { get; set; }
        public float? Val { get; set; }
        public int Id { get; set; }

        public virtual Object? KodObjectNavigation { get; set; }
    }
}
