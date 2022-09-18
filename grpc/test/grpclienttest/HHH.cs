using System;
using grpclienttest;
using Google.Protobuf.WellKnownTypes;

namespace Helpers
{

    public static class ZHelper
    {

        public static void Print(GTelemetry gobj)
        {
            var period = gobj.Period.ToDateTime().ToLocalTime();
          

            Console.WriteLine($"\t{period.ToString().PadRight(20, ' ')}\t\t {gobj.Id.ToString().PadRight(5, ' ')}\t\t {gobj.Value.ToString().PadRight(10, ' ')}\t\t {gobj.Type.ToString().PadRight(5, ' ')}\t\t {gobj.KodObject.ToString().PadRight(5, ' ')}");
        }



        public static void Print(GRequest gobj)
        {
            var df = gobj.Data.ToDateTime().ToLocalTime().Date;
            var dt = gobj.DateTo.ToDateTime().ToLocalTime().Date;
            var dfakt = gobj.DateFromFakt.ToDateTime().ToLocalTime().Date;
            var dtfakt = gobj.DateToFakt.ToDateTime().ToLocalTime().Date;


            Console.WriteLine($"\t{gobj.Id}\t{gobj.KodObject}\t\t{gobj.KodObject}\t{gobj.Comment.ToString().PadRight(10, ' ')}\t\t {df.ToString().PadRight(10, ' ')}\t\t {dt.ToString().PadRight(10, ' ')}\t\t {dfakt.ToString().PadRight(10, ' ')}\t\t{dtfakt.ToString().PadRight(10, ' ')}\t\t");
        }


        public static void Print(GObject gobj)
        {
            var df = gobj?.DateFrom?.ToDateTime().ToLocalTime().Date??DateTime.MinValue;
            var wt = gobj?.WarningTime?.ToDateTime().ToLocalTime()??DateTime.MinValue;

            Console.WriteLine($"\t{gobj.Name.PadRight(20, ' ')}\t{gobj.ErrorPeriod.ToString().PadRight(20, ' ')}\t\t {df.ToString().PadRight(20, ' ')}\t\t{wt.ToString()}");
        }

        public static void Print(GNarabotka gobj)
        {
            var df = gobj.Data.ToDateTime().ToLocalTime().Date;
          

            Console.WriteLine($"\t{gobj.Id}\t{gobj.KodObject}\t{gobj.Val.ToString().PadRight(10, ' ')}\t\t {df.ToString().PadRight(20, ' ')}\t\t");
        }

        public static Timestamp TimeSFromDateTime(DateTime dt)
        {
            return Timestamp.FromDateTime(DateTime.SpecifyKind(dt.ToUniversalTime(), DateTimeKind.Utc));
        }


        public static Timestamp TimeSFromDateOnlyVals(int year, int month, int day)
        {
            return TimeSFromDateTime(new DateTime(year, month, day));
        }

            public static DateTime ToMoscowFromTimeStamp(Timestamp tstmp)
    {
        var dt = DateTime.SpecifyKind(tstmp.ToDateTime().AddHours(3), DateTimeKind.Unspecified);
        return dt;
    }

    public static DateOnly ToMoscowDateOnlyFromTimeStamp(Timestamp tstmp)
    {
        var dt = ToMoscowFromTimeStamp(tstmp);
        return DateOnly.FromDateTime(dt);
    }

    }
}