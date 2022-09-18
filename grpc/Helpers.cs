

using Google.Protobuf.WellKnownTypes;

namespace grpcserv1.Helpers;


public static class Helper
{
    public static Timestamp FromDateOnly(DateOnly? donly)
    {
        var utcv = DateTime.SpecifyKind(donly?.ToDateTime(TimeOnly.MaxValue).ToUniversalTime() ?? DateTime.UnixEpoch, DateTimeKind.Utc);
        var tstmp = Timestamp.FromDateTime(utcv);
        return tstmp;
    }

    public static Timestamp FromDateTime(DateTime? dt)
    {
        var utcv = DateTime.SpecifyKind(dt?.ToUniversalTime() ?? DateTime.UnixEpoch, DateTimeKind.Utc);
        var tstmp = Timestamp.FromDateTime(utcv);
        return tstmp;
    }

    public static GTelemetry GTelemetryEmpty()
    {
        return new GTelemetry { HasValue = false };
    }

    public static GTelemetrys GTelemetrysEmpty()
    {
        return new GTelemetrys { HasValue = false };
    }
    public static GTelemetry GTelemetryFromTelemetry(Telemetry obj)
    {
        var greq = new GTelemetry
        {
            Type = obj.Type,
            Period = Helper.FromDateTime(obj.Period),
            Value = obj.Value ?? -1,
            Id = obj.Id,
            KodObject = obj.KodObject ?? -1,
            HasValue = true
        };
        return greq;
    }

    public static Telemetry TelemetryFromGTelemetry(GTelemetry obj)
    {
        var req = new Telemetry
        {
            Type = obj.Type,
            Period = Helper.ToMoscowFromTimeStamp(obj.Period),
            Value = obj.Value,
            Id = obj.Id,
            KodObject = obj.KodObject            
        };
        return req;
    }

    public static GRequest GRequestEmpty()
    {
        return new GRequest { HasValue = false };
    }
    public static GRequests GRequestsEmpty()
    {
        return new GRequests { HasValue = false };
    }
    public static GRequest GRequestFromRequest(Request obj)
    {
        var greq = new GRequest
        {
            Num = obj.Num,
            Data = Helper.FromDateOnly(obj.Data),
            KodObject = obj.KodObject ?? -1,
            DateFrom = Helper.FromDateOnly(obj.DateFrom),
            DateTo = Helper.FromDateOnly(obj.DateTo),
            Status = obj.Status ?? -1,
            DateFromFakt = Helper.FromDateOnly(obj.DateFromFakt),
            DateToFakt = Helper.FromDateOnly(obj.DateToFakt),
            Comment = obj.Comment ?? "",
            Id = obj.Id,
            HasValue = true
        };
        return greq;
    }

    public static Request RequestFromGRequest(GRequest obj)
    {
        var req = new Request
        {
            Num = obj.Num,
            Data = Helper.ToMoscowDateOnlyFromTimeStamp(obj.Data),
            KodObject = obj.KodObject,
            DateFrom = Helper.ToMoscowDateOnlyFromTimeStamp(obj.DateFrom),
            DateTo = Helper.ToMoscowDateOnlyFromTimeStamp(obj.DateTo),
            Status = obj.Status,
            DateFromFakt = Helper.ToMoscowDateOnlyFromTimeStamp(obj.DateFromFakt),
            DateToFakt = Helper.ToMoscowDateOnlyFromTimeStamp(obj.DateToFakt),
            Comment = obj.Comment,
            Id = obj.Id
        };
        return req;
    }

    public static GNarabotkas GNarabotkasEmpty()
    {
        return new GNarabotkas { HasValue = false };
    }

    public static GNarabotka GNarabotkaEmpty()
    {
        return new GNarabotka { HasValue = false };
    }

    public static GNarabotka GNarabotkaFromNarabotka(Narabotka obj)
    {
        var gnarab = new GNarabotka
        {
            KodObject = obj.KodObject ?? -1,
            Data = Helper.FromDateOnly(obj.Data),
            Val = obj.Val ?? -1,
            Id = obj.Id,
            HasValue = true
        };
        return gnarab;
    }

    public static Narabotka NarabotkaFromGNarabotka(GNarabotka obj)
    {
        var narab = new Narabotka
        {
            KodObject = obj.KodObject,
            Data = Helper.ToMoscowDateOnlyFromTimeStamp(obj.Data),
            Val = obj.Val,
            Id = obj.Id
        };
        return narab;
    }


    public static GObject GObjectEmpty()
    {
        return new GObject { HasValue = false };
    }

    public static GObjects GObjectsEmpty()
    {
        return new GObjects { HasValue = false };
    }
    
    public static GObject GobjectFromObject(Object obj)
    {
        var gobj = new GObject
        {
            Kod = obj.Kod,
            Name = obj.Name ?? "",
            TekNar = obj.TekNar ?? -1,
            LastTo = Helper.FromDateOnly(obj.LastTo),
            ToTime = obj.ToTime ?? -1,
            ToNar = obj.ToNar ?? -1,
            PlanYear = obj.PlanYear ?? -1,
            Koef2 = obj.Koef2 ?? -1,
            Koef1 = obj.Koef1 ?? -1,
            SredNar = obj.SredNar ?? -1,
            DateFrom = Helper.FromDateOnly(obj.DateFrom),
            NarFrom = obj.NarFrom ?? -1,
            NextTo = Helper.FromDateOnly(obj.NextTo),
            SredNarPlan = obj.SredNarPlan ?? -1,
            Status = obj.Status ?? -1,
            Id = obj.Id,
            TrainingFrom = Helper.FromDateOnly(obj.TrainingFrom),
            TrainingTo = Helper.FromDateOnly(obj.TrainingTo),
            WarningType = obj.WarningType ?? -1,
            WarningTime = Helper.FromDateTime(obj.WarningTime),
            WarningFrom = Helper.FromDateOnly(obj.WarningFrom),
            WarningSensor = obj.WarningSensor ?? -1,
            ErrorRate = obj.ErrorRate ?? -1,
            ErrorPeriod = obj.ErrorPeriod??-1 ,
            HasValue = true
        };
        return gobj;
    }

    public static Object ObjectFromGObject(GObject obj)
    {
        var gobj = new Object
        {
            Kod = obj.Kod,
            Name = obj.Name ?? "",
            TekNar = obj.TekNar,
            LastTo = ToMoscowDateOnlyFromTimeStamp(obj.LastTo),
            ToTime = obj.ToTime,
            ToNar = obj.ToNar,
            PlanYear = obj.PlanYear,
            Koef2 = obj.Koef2,
            Koef1 = obj.Koef1,
            SredNar = obj.SredNar,
            DateFrom = ToMoscowDateOnlyFromTimeStamp(obj.DateFrom),
            NarFrom = obj.NarFrom,
            NextTo = ToMoscowDateOnlyFromTimeStamp(obj.NextTo),
            SredNarPlan = obj.SredNarPlan,
            Status = obj.Status,
            Id = obj.Id,
            TrainingFrom = ToMoscowDateOnlyFromTimeStamp(obj.TrainingFrom),
            TrainingTo = ToMoscowDateOnlyFromTimeStamp(obj.TrainingTo),
            WarningType = obj.WarningType,
            WarningTime = ToMoscowFromTimeStamp(obj.WarningTime),
            WarningFrom = ToMoscowDateOnlyFromTimeStamp(obj.WarningFrom),
            WarningSensor = obj.WarningSensor,
            ErrorPeriod = obj.ErrorPeriod,
            ErrorRate = obj.ErrorRate
        };
        return gobj;
    }

     public static DateTime ToDateTimFromTimeStamp(Timestamp tstmp)
    {
        var dt = DateTime.SpecifyKind(tstmp.ToDateTime(), DateTimeKind.Unspecified);
        return dt;
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
