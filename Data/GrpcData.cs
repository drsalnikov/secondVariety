using SecondVariety;
using Grpc.Net.Client;
using Microsoft.AspNetCore;
using Google.Protobuf.WellKnownTypes;
using SecondVariety.Models;
using Grpc.Core;
namespace SecondVariety
{

  public class GrpcClientService
  {

    public GrpcClientService()
    {
      var builder = WebApplication.CreateBuilder();
      clientChannelPath = builder.Configuration["GrpcChannels:SecondVariety"];
      jwtToken = builder.Configuration["JWTBearer:Token"];
    }

    public IEnumerable<Models.Object> GetObjects()
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Object> objs = new List<Models.Object>();
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var objallreply = gobclient.GetAll(new SecondVariety.Empty(), GetMetadata());

      if (objallreply != null && objallreply.Items.Count > 0)
      {
        return objallreply.Items.Select(oo => ObjectFromGObject(oo));
      }

      return objs;
    }

    public void DeleteObject(int Id)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var empt = gobclient.Delete(new GObjectId { Id = Id }, GetMetadata());
    }

    public Models.Object GetObjectById(int Id)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var gobj = gobclient.GetById(new GObjectId { Id = Id }, GetMetadata());
      return ObjectFromGObject(gobj);
    }

    public async Task AddObjectAsync(Models.Object obj)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var objallreply = await gobclient.PostAsync(GobjectFromObject(obj), GetMetadata());
    }

    public void AddObject(Models.Object obj)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var objallreply = gobclient.Post(GobjectFromObject(obj), GetMetadata());
    }

    public void UpdateObject(Models.Object obj)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var gobj = gobclient.Put(GobjectFromObject(obj), GetMetadata());
    }

    public async Task<IEnumerable<Models.Narabotka>> GetNarabotkasByObjectKod(int Kod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Narabotka> objs = new List<Models.Narabotka>();
      var gobclient = new NarabotkaServ.NarabotkaServClient(channel);
      var objallreply = await gobclient.GetByObjectKodAsync(new GObjectId { Id = Kod }, GetMetadata());

      if (objallreply != null && objallreply.Items.Count > 0)
      {
        return objallreply.Items.Select(oo => NarabotkaFromGNarabotka(oo));
      }

      return objs;
    }
    public IEnumerable<Models.Narabotka> GetNarabotkasByObjectKodSync(int Kod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Narabotka> objs = new List<Models.Narabotka>();
      var gobclient = new NarabotkaServ.NarabotkaServClient(channel);
      var objallreply = gobclient.GetByObjectKod(new GObjectId { Id = Kod }, GetMetadata());

      if (objallreply != null && objallreply.Items.Count > 0)
      {
        return objallreply.Items.Select(oo => NarabotkaFromGNarabotka(oo));
      }

      return objs;
    }
    public void AddNarabotka(Models.Narabotka narabotka)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);

      var gobclient = new NarabotkaServ.NarabotkaServClient(channel);
      var objallreply = gobclient.Post(GNarabotkaFromNarabotka(narabotka), GetMetadata());
    }

    public void UpdateNarabotka(Models.Narabotka narabotka)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);

      var gobclient = new NarabotkaServ.NarabotkaServClient(channel);
      var objallreply = gobclient.Put(GNarabotkaFromNarabotka(narabotka), GetMetadata());
    }

    public void DeleteNarabotka(int Id)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new NarabotkaServ.NarabotkaServClient(channel);
      var empt = gobclient.Delete(new GObjectId { Id = Id }, GetMetadata());
    }
    public void AddRequest(Models.Request request)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new RequestServ.RequestServClient(channel);
      var objallreply = gobclient.Post(GRequestFromRequest(request), GetMetadata());
    }

    public IEnumerable<Models.Request> GetRequests()
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Request> objs = new List<Models.Request>();
      var gobclient = new RequestServ.RequestServClient(channel);
      var objallreply = gobclient.GetAll(new SecondVariety.Empty(), GetMetadata());

      if (objallreply != null && objallreply.Items.Count > 0)
      {
        return objallreply.Items.Select(oo => RequestFromGRequest(oo));
      }

      return objs;
    }

    public async Task<IEnumerable<Models.Request>> GetRequestByObjectKod(int Kod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Request> objs = new List<Models.Request>();
      var gobclient = new RequestServ.RequestServClient(channel);
      var objallreply = await gobclient.GetByObjectKodAsync(new GObjectId { Id = Kod }, GetMetadata());

      if (objallreply != null && objallreply.Items.Count > 0)
      {
        return objallreply.Items.Select(oo => RequestFromGRequest(oo));
      }

      return objs;
    }

    public Models.Request? GetRequestByNum(int Num)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new RequestServ.RequestServClient(channel);
      var req = gobclient.GetByNum(new GObjectId { Id = Num }, GetMetadata());

      if (req != null && req.HasValue)
      {
        return RequestFromGRequest(req);
      }

      return null;
    }

    public Models.Request? GetRequestById(long Id)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new RequestServ.RequestServClient(channel);
      var req = gobclient.GetById(new GRequestId { Id = Id }, GetMetadata());

      if (req != null && req.HasValue)
      {
        return RequestFromGRequest(req);
      }

      return null;
    }

    public IEnumerable<Models.Telemetry> GetTelemetry(DateTime dt)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new TelemetryServ.TelemetryServClient(channel);
      var req = gobclient.GetForDate(FromDateTime(dt), GetMetadata());

      if (req != null && req.Items.Count > 0)
      {
        var enu = req.Items.Select(tt => TelemetryFromGTelemetry(tt));
        return enu;
      }

      return null;
    }

    public (IEnumerable<Models.Telemetry>, IEnumerable<Models.Telemetry>) GetWarning4(DateTime start, DateTime end, int objKod)
    {
      DateTime stDt = start;
      var enDt = end;
      //var startDate = TimeSFromDateTime(stDt);
      var period = new GTelemetryPeriod { Begin = TimeSFromDateTime(stDt), End = TimeSFromDateTime(enDt) };
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);

      List<Models.Telemetry> tel = new List<Telemetry>();
      List<Models.Telemetry> tel1 = new List<Telemetry>();

      try
      {
        var reqall = telemetryclient.GetForPeriodForObjectWarning4(new GTelemetryPeriodForObject { KodObject = new GObjectId { Id = objKod }, GTelemetryPeriod = period }, GetMetadata());
        if (reqall != null)
        {
          tel.AddRange(reqall.Type1.Items.Select(iii => TelemetryFromGTelemetry(iii)));
          tel1.AddRange(reqall.Type2.Items.Select(iii => TelemetryFromGTelemetry(iii)));
        }
      }
      catch { }

      return (tel, tel1);
    }


    public string GetToken(string[] URIs)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var tokenclient = new GGetTokensServ.GGetTokensServClient(channel);
      if (URIs.Count() < 0)
        return "";
      var gclaims = URIs.Select(uuu => new GClaim { Uri = uuu })?.ToArray();

      var claims = new GGetTokenClaims { };
      claims.Claims.AddRange(gclaims);

      var token = tokenclient.GetToken(claims, GetMetadata());
      return token.Uri;
    }

    public async Task<int> LastTrainingId()
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
      var ltrres = await telemetryclient.GetLastTrainedRecIdAsync(new Google.Protobuf.WellKnownTypes.Empty { }, GetMetadata());
      int? id = ltrres?.Id;
      return id ?? -1;
    }

    public async Task CheckingTrainingObj(int objKod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
      try
      {
        await telemetryclient.CheckingObjAsync(new GObjectId { Id = objKod }, GetMetadata());
      }
      catch { }
    }

    public async Task TelemetryTrainingObj(int obkKod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
      try
      {
        await telemetryclient.TrainingObjAsync(new GObjectId { Id = obkKod }, GetMetadata());
      }
      catch { }
    }

    public IEnumerable<Models.Telemetry> GetTelemetryServPeriod(DateTime start, DateTime end)
    {
      var stDt = start;
      var enDt = end;
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var period = new GTelemetryPeriod { Begin = TimeSFromDateTime(stDt), End = TimeSFromDateTime(enDt) };

      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
      List<Telemetry> tlmts = new List<Telemetry>();
      try
      {
        var reqall = telemetryclient.GetForPeriod(period, GetMetadata());
        tlmts.AddRange(reqall.Items.Select(gt => TelemetryFromGTelemetry(gt)));
      }
      catch { }
      return tlmts;
    }
    public IEnumerable<Models.Telemetry> GetTelemetryServPeriodByObjKod(DateTime start, DateTime end, int objKod)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var stDt = start;
      var enDt = end;

      var period = new GTelemetryPeriod { Begin = TimeSFromDateTime(stDt), End = TimeSFromDateTime(enDt) };

      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);

      List<Telemetry> tlmts = new List<Telemetry>();
      try
      {
        var reqall = telemetryclient.GetForPeriodForObjectByKod(new GTelemetryPeriodForObject { KodObject = new GObjectId { Id = objKod }, GTelemetryPeriod = period }, GetMetadata());
        tlmts.AddRange(reqall.Items.Select(gt => TelemetryFromGTelemetry(gt)));
      }
      catch { }
      return tlmts ;
    }

    public void DeleteRequest(long Id)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new RequestServ.RequestServClient(channel);
      var empt = gobclient.Delete(new GRequestId { Id = Id }, GetMetadata());
    }

    public void DeleteRequestByNum(int Num)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var gobclient = new RequestServ.RequestServClient(channel);
      var empt = gobclient.DeleteByNum(new GObjectId { Id = Num }, GetMetadata());
    }

    public void UpdateRequest(Models.Request request)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);

      var gobclient = new RequestServ.RequestServClient(channel);
      var objallreply = gobclient.Put(GRequestFromRequest(request), GetMetadata());
    }

    private Timestamp FromDateOnly(DateOnly? donly)
    {
      var utcv = DateTime.SpecifyKind(donly?.ToDateTime(TimeOnly.MaxValue).ToUniversalTime() ?? DateTime.UnixEpoch, DateTimeKind.Utc);
      var tstmp = Timestamp.FromDateTime(utcv);
      return tstmp;
    }

    private Timestamp FromDateTime(DateTime? dt)
    {
      var utcv = DateTime.SpecifyKind(dt?.ToUniversalTime() ?? DateTime.UnixEpoch, DateTimeKind.Utc);
      var tstmp = Timestamp.FromDateTime(utcv);
      return tstmp;
    }

    private GTelemetry GTelemetryFromTelemetry(Telemetry obj)
    {
      var greq = new GTelemetry
      {
        Type = obj.Type,
        Period = FromDateTime(obj.Period),
        Value = obj.Value ?? -1,
        Id = obj.Id,
        KodObject = obj.KodObject ?? -1,
      };
      return greq;
    }

    private Telemetry TelemetryFromGTelemetry(GTelemetry obj)
    {
      var req = new Telemetry
      {
        Type = obj.Type,
        Period = ToMoscowFromTimeStamp(obj.Period),
        Value = obj.Value,
        Id = obj.Id,
        KodObject = obj.KodObject
      };
      return req;
    }




    private GRequest GRequestFromRequest(Request obj)
    {
      var greq = new GRequest
      {
        Num = obj.Num,
        Data = FromDateTime(obj.Data),
        KodObject = obj.KodObject ?? -1,
        DateFrom = FromDateTime(obj.DateFrom),
        DateTo = FromDateTime(obj.DateTo),
        Status = obj.Status ?? -1,
        DateFromFakt = FromDateTime(obj.DateFromFakt),
        DateToFakt = FromDateTime(obj.DateToFakt),
        Comment = obj.Comment ?? "",
        Id = obj.Id,
      };
      return greq;
    }

    private Request RequestFromGRequest(GRequest obj)
    {
      var req = new Request
      {
        Num = obj.Num,
        Data = ToMoscowFromTimeStamp(obj.Data),
        KodObject = obj.KodObject,
        DateFrom = ToMoscowFromTimeStamp(obj.DateFrom),
        DateTo = ToMoscowFromTimeStamp(obj.DateTo),
        Status = obj.Status,
        DateFromFakt = ToMoscowFromTimeStamp(obj.DateFromFakt),
        DateToFakt = ToMoscowFromTimeStamp(obj.DateToFakt),
        Comment = obj.Comment,
        Id = obj.Id,
      };
      return req;
    }


    private GNarabotka GNarabotkaFromNarabotka(Narabotka obj)
    {
      var gnarab = new GNarabotka
      {
        KodObject = obj.KodObject ?? -1,
        Data = FromDateTime(obj.Data),
        Val = obj.Val ?? -1,
        Id = obj.Id
      };
      return gnarab;
    }

    private Narabotka NarabotkaFromGNarabotka(GNarabotka obj)
    {
      var narab = new Narabotka
      {
        KodObject = obj.KodObject,
        Data = ToMoscowFromTimeStamp(obj.Data),
        Val = obj.Val,
        Id = obj.Id
      };
      return narab;
    }
    private GObject GobjectFromObject(SecondVariety.Models.Object obj)
    {
      var gobj = new GObject
      {
        Kod = obj.Kod,
        Name = obj.Name ?? "",
        TekNar = obj.TekNar ?? -1,
        LastTo = FromDateTime(obj.LastTo),
        ToTime = obj.ToTime ?? -1,
        ToNar = obj.ToNar ?? -1,
        PlanYear = obj.PlanYear ?? -1,
        Koef2 = obj.Koef2 ?? -1,
        Koef1 = obj.Koef1 ?? -1,
        SredNar = obj.SredNar ?? -1,
        DateFrom = FromDateTime(obj.DateFrom),
        NarFrom = obj.NarFrom ?? -1,
        NextTo = FromDateTime(obj.NextTo),
        SredNarPlan = obj.SredNarPlan ?? -1,
        Status = obj.Status ?? -1,
        Id = obj.Id,
        TrainingFrom = FromDateTime(obj.TrainingFrom),
        TrainingTo = FromDateTime(obj.TrainingTo),
        WarningType = obj.WarningType ?? -1,
        WarningTime = FromDateTime(obj.WarningTime),
        WarningFrom = FromDateTime(obj.WarningFrom),
        WarningSensor = obj.WarningSensor ?? -1,
        ErrorRate = obj.ErrorRate ?? -1
      };
      return gobj;
    }

    private SecondVariety.Models.Object ObjectFromGObject(GObject obj)
    {
      var gobj = new SecondVariety.Models.Object
      {
        Kod = obj.Kod,
        Name = obj.Name ?? "",
        TekNar = obj.TekNar,
        LastTo = ToMoscowFromTimeStamp(obj.LastTo),
        ToTime = obj.ToTime,
        ToNar = obj.ToNar,
        PlanYear = obj.PlanYear,
        Koef2 = obj.Koef2,
        Koef1 = obj.Koef1,
        SredNar = obj.SredNar,
        DateFrom = ToMoscowFromTimeStamp(obj.DateFrom),
        NarFrom = obj.NarFrom,
        NextTo = ToMoscowFromTimeStamp(obj.NextTo),
        SredNarPlan = obj.SredNarPlan,
        Status = obj.Status,
        Id = obj.Id,
        TrainingFrom = ToMoscowFromTimeStamp(obj.TrainingFrom),
        TrainingTo = ToMoscowFromTimeStamp(obj.TrainingTo),
        WarningType = obj.WarningType,
        WarningTime = ToMoscowFromTimeStamp(obj.WarningTime),
        WarningFrom = ToMoscowFromTimeStamp(obj.WarningFrom),
        WarningSensor = obj.WarningSensor,
        ErrorRate = obj.ErrorRate
      };
      return gobj;
    }




    private Metadata? GetMetadata()
    {
      var headers = new Metadata();
      headers.Add("Authorization", $"Bearer {jwtToken}");
      return headers;
    }

    private DateTime ToMoscowFromTimeStamp(Timestamp tstmp)
    {
      var dt = DateTime.SpecifyKind(tstmp.ToDateTime().AddHours(3), DateTimeKind.Unspecified);
      return dt;
    }

    private DateOnly ToMoscowDateOnlyFromTimeStamp(Timestamp tstmp)
    {
      var dt = ToMoscowFromTimeStamp(tstmp);
      return DateOnly.FromDateTime(dt);
    }


    private Timestamp TimeSFromDateTime(DateTime dt)
    {
      return Timestamp.FromDateTime(DateTime.SpecifyKind(dt.ToUniversalTime(), DateTimeKind.Utc));
    }


    private Timestamp TimeSFromDateOnlyVals(int year, int month, int day)
    {
      return TimeSFromDateTime(new DateTime(year, month, day));
    }
    private string clientChannelPath;
    private string jwtToken;
  }
}

