using SecondVariety;
using Grpc.Net.Client;
using Microsoft.AspNetCore;
using Google.Protobuf.WellKnownTypes;
using SecondVariety.Models;
using Grpc.Core;
using System.Diagnostics ;
namespace SecondVariety
{

  public class GrpcClientService
  {
    private readonly ILogger<GrpcClientService> _logger;
    public GrpcClientService(ILogger<GrpcClientService> logger)
    {
      _logger = logger;
      var builder = WebApplication.CreateBuilder();
      clientChannelPath = builder.Configuration["GrpcChannels:SecondVariety"];
      jwtToken = builder.Configuration["JWTBearer:Token"];
      _logger.LogInformation($"Created GrpcClientService. StackTrace: {Environment.StackTrace}");
    }

    //public GrpcClientService()
    //{
    //  
    //}

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
    public async Task<IEnumerable<Models.Object>> GetObjectsAsync()
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      IEnumerable<Models.Object> objs = new List<Models.Object>();
      var gobclient = new ObjectsServ.ObjectsServClient(channel);
      var objallreply = await gobclient.GetAllAsync(new SecondVariety.Empty(), GetMetadata());

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
    /*
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
    }*/

    public IEnumerable<Models.Telemetry> GetTelemetryDataById(int Id)
    {
      var retTelemes = new List<Models.Telemetry>();
      _logger.LogInformation("GetTelemetryDataById");
      
      try
      {

        using var channel = GrpcChannel.ForAddress(clientChannelPath);
        var gobclient = new ObjectsServ.ObjectsServClient(channel);
        var obj = gobclient.GetById(new GObjectId { Id = Id }, GetMetadata());

        if (obj != null)
        {
          var errorPeriod = obj.ErrorPeriod;
          var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
          var warningDate = obj.WarningTime.ToDateTime();
          var stDt = warningDate.AddMinutes(-1 * errorPeriod / 2);//Add(offset) ;
          var enDt = warningDate.AddMinutes(1 * errorPeriod / 2);//.Add(offset) ;
          var startDate = TimeSFromDateTime(stDt);
          var endDate = TimeSFromDateTime(enDt);
           _logger.LogInformation($"TimeZoneInfo.Local.StandardName : {TimeZoneInfo.Local.StandardName }; offset: {offset} ;") ;
           _logger.LogInformation($"stDt: {stDt.ToString()}; enDt: {enDt.ToString()} ;") ;
          _logger.LogInformation($"startDate: {startDate.ToDateTime().ToString()}; endDate: {endDate.ToDateTime().ToString()} ;") ;
          var period = new GTelemetryPeriodForObject
          {
            KodObject = new GObjectId { Id = obj.Kod },
            GTelemetryPeriod = new GTelemetryPeriod { Begin = startDate, End = endDate }
          }
                                                      ;

          var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
          var reqall = telemetryclient.GetForPeriodForObjectByKod(period, GetMetadata());
          if (reqall != null && reqall.Items.Count > 0)
          {
            retTelemes.AddRange(reqall.Items.Select(ttt => TelemetryFromGTelemetryAsIs(ttt)));
          }
        }
      }
      catch { }

      return retTelemes;

    }
    //return count records writed to db and total time
    //on error return (null,null)
    public async Task<(long?, long?)> UploadTelemetry(int oKod, int tType, DateTime dt, byte[] fInWitsmlFile)
    {
      using var channel = GrpcChannel.ForAddress(clientChannelPath);
      var telemetryclient = new TelemetryServ.TelemetryServClient(channel);
      var fbytes0 = fInWitsmlFile;
      var ams1 = new MemoryStream();

      try
      {
        using (System.IO.Compression.DeflateStream dstream = new System.IO.Compression.DeflateStream(ams1, System.IO.Compression.CompressionLevel.Optimal))
        {
          await dstream.WriteAsync(fbytes0, 0, fbytes0.Count());
        }

        var fbytes = ams1.ToArray();
        int scount = 0;
        if (fbytes != null && fbytes.Count() > 0)
        {
          var asyncfc = async () =>
          {
            using (var tcall = telemetryclient.UploadTelemetry(GetMetadata()))
            {
              int i = 0;
              int step = 5024;
              for (i = 0; i < fbytes.Count(); i += step)
              {
                byte[] buf;
                if (i < (fbytes.Count() - step))
                {
                  buf = new byte[step];
                  Array.Copy(fbytes, i, buf, 0, step);
                }
                else
                {
                  var sz = fbytes.Count() - i;
                  buf = new byte[sz];
                  Array.Copy(fbytes, i, buf, 0, sz);
                }
                var bara = Google.Protobuf.ByteString.CopyFrom(buf);

                await tcall.RequestStream.WriteAsync(new GWitsml { Data = bara });
                scount = i;
              }
              int objcode = oKod;
              //stack
              //1- kod
              //2- type
              //3- period y
              //4- period m
              //5- period d
              //6- period h
              //7- period m 
              var szi = sizeof(int);
              var kodBts = BitConverter.GetBytes(objcode);
              var typeBts = BitConverter.GetBytes(tType);
              var yearBts = BitConverter.GetBytes(dt.Year);
              var monthBts = BitConverter.GetBytes(dt.Month);
              var dayBts = BitConverter.GetBytes(dt.Day);
              var hourBts = BitConverter.GetBytes(dt.Hour);
              var minBts = BitConverter.GetBytes(dt.Minute);


              var bArKod = Google.Protobuf.ByteString.CopyFrom(kodBts);
              var bArType = Google.Protobuf.ByteString.CopyFrom(typeBts);
              var bArYear = Google.Protobuf.ByteString.CopyFrom(yearBts);
              var bArMonth = Google.Protobuf.ByteString.CopyFrom(monthBts);
              var bArDay = Google.Protobuf.ByteString.CopyFrom(dayBts);
              var bArHour = Google.Protobuf.ByteString.CopyFrom(hourBts);
              var bArMin = Google.Protobuf.ByteString.CopyFrom(minBts);

              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArKod });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArType });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArYear });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArMonth });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArDay });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArHour });
              await tcall.RequestStream.WriteAsync(new GWitsml { Data = bArMin });



              await tcall.RequestStream.CompleteAsync();
              return await tcall.ResponseAsync;
            }
          };

          var res = await asyncfc();
          return (res.Count, res.TimeTotal);
        }
      }
      catch { }
      return (null, null);
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
        Period = DateTimeFromTimeStampLocal(obj.Period),
        Value = obj.Value,
        Id = obj.Id,
        KodObject = obj.KodObject
      };
      return req;
    }

    private Telemetry TelemetryFromGTelemetryAsIs(GTelemetry obj)
    {
      var req = new Telemetry
      {
        Type = obj.Type,
        Period = DateTimeFromTimeStampLocal(obj.Period),
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
        Data = DateTimeFromTimeStampLocal(obj.Data),
        KodObject = obj.KodObject,
        DateFrom = DateTimeFromTimeStampLocal(obj.DateFrom),
        DateTo = DateTimeFromTimeStampLocal(obj.DateTo),
        Status = obj.Status,
        DateFromFakt = DateTimeFromTimeStampLocal(obj.DateFromFakt),
        DateToFakt = DateTimeFromTimeStampLocal(obj.DateToFakt),
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
        Data = DateTimeFromTimeStampLocal(obj.Data),
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
        ErrorPeriod = obj.ErrorPeriod ?? -1,
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
        LastTo = DateTimeFromTimeStampLocal(obj.LastTo),
        ToTime = obj.ToTime,
        ToNar = obj.ToNar,
        PlanYear = obj.PlanYear,
        Koef2 = obj.Koef2,
        Koef1 = obj.Koef1,
        SredNar = obj.SredNar,
        DateFrom = DateTimeFromTimeStampLocal(obj.DateFrom),
        NarFrom = obj.NarFrom,
        NextTo = DateTimeFromTimeStampLocal(obj.NextTo),
        SredNarPlan = obj.SredNarPlan,
        Status = obj.Status,
        Id = obj.Id,
        TrainingFrom = DateTimeFromTimeStampLocal(obj.TrainingFrom),
        TrainingTo = DateTimeFromTimeStampLocal(obj.TrainingTo),
        WarningType = obj.WarningType,
        WarningTime = DateTimeFromTimeStampLocal(obj.WarningTime),
        WarningFrom = DateTimeFromTimeStampLocal(obj.WarningFrom),
        WarningSensor = obj.WarningSensor,
        ErrorPeriod = obj.ErrorPeriod,
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
    private DateTime DateTimeFromTimeStampLocal(Timestamp tstmp)
    {
      var dt = DateTime.SpecifyKind(tstmp.ToDateTime(), DateTimeKind.Local);
      return dt;
    }

    private DateTime DateTimeFromTimeStampUnspecificKind(Timestamp tstmp)
    {
      var dt = DateTime.SpecifyKind(tstmp.ToDateTime(), DateTimeKind.Unspecified);
      return dt;
    }

    /*private DateTime ToMoscowFromTimeStamp(Timestamp tstmp)
    {
      var dt = DateTime.SpecifyKind(tstmp.ToDateTime().AddHours(3), DateTimeKind.Unspecified);
      return dt;
    }

    private DateOnly ToMoscowDateOnlyFromTimeStamp(Timestamp tstmp)
    {
      var dt = ToMoscowFromTimeStamp(tstmp);
      return DateOnly.FromDateTime(dt);
    }*/


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

