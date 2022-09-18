using Grpc.Core;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using grpcserv1.Tokens;
using grpcserv1.Helpers;
using System.Security.Claims;
using System.Xml.Serialization;
using Npgsql.Bulk;
using Npgsql;
using System.Globalization;

namespace grpcserv1.Services;


public class GTelemetryService : TelemetryServ.TelemetryServBase
{
    private readonly ILogger<GTelemetryService> _logger;
    public GTelemetryService(ILogger<GTelemetryService> logger)
    {
        _logger = logger;
    }

    [Authorize("GAuth")]
    public override Task<GTelemetryTwoTypes> GetForPeriodForObjectWarning4(GTelemetryPeriodForObject request, ServerCallContext context)
    {
        GTelemetryTwoTypes gobjs = new GTelemetryTwoTypes();
        gobjs.Type1 = new GTelemetrys();
        gobjs.Type1.HasValue = false;
        gobjs.Type2 = new GTelemetrys();
        gobjs.Type2.HasValue = false;

        using (var dbcont = new db1Context())
        {
            var datest = Helper.ToMoscowFromTimeStamp(request.GTelemetryPeriod.Begin);
            var dateEnd = Helper.ToMoscowFromTimeStamp(request.GTelemetryPeriod.End); ;
            int objKod = request.KodObject.Id;

            try
            {
                var dependen = dbcont.Dependences.First(tt => tt.KodObject == objKod);

                if (dependen == null)
                {
                    return Task.FromResult<GTelemetryTwoTypes>(gobjs);
                }

                var iqtelemetries = dbcont.Telemetries.Where(tt => tt.Period >= datest && tt.Period < dateEnd
                                                                                       && tt.Type == dependen.Type1
                                                                                        && tt.KodObject == objKod)
                                                                      ?.Select(obj => Helper.GTelemetryFromTelemetry(obj));

                var iqtelemetries1 = dbcont.Telemetries.Where(tt => tt.Period >= datest && tt.Period < dateEnd
                                                                                       && tt.Type == dependen.Type2
                                                                                        && tt.KodObject == objKod)
                                                                      ?.Select(obj => Helper.GTelemetryFromTelemetry(obj));

                if ((iqtelemetries != null) && (iqtelemetries1 != null) && iqtelemetries1.Count() > 0 && iqtelemetries.Count() > 0)
                {
                    gobjs.Type1.HasValue = true;
                    gobjs.Type2.HasValue = true;
                    gobjs.Type1.Items.AddRange(iqtelemetries);
                    gobjs.Type2.Items.AddRange(iqtelemetries1);
                    return Task.FromResult(gobjs);
                }
            }
            catch { }
        }
        return Task.FromResult<GTelemetryTwoTypes>(gobjs); ;
    }

    [Authorize("GAuth")]
    public override async Task<Empty> Delete(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Telemetry? obj = await dbcont.Telemetries.FindAsync(request.Id);
            if (obj != null)
            {
                dbcont.Telemetries.Remove(obj);
                dbcont.SaveChanges();
                return new Empty();
            }
        }

        return new Empty { };
    }


    [Authorize("GAuth")]
    public override async Task<GTelemetry> Put(GTelemetry request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Telemetry? obj = await dbcont.Telemetries.FindAsync(request.Id);
            if (obj != null)
            {
                try
                {
                    obj.Type = obj.Type;
                    obj.Period = Helper.ToMoscowFromTimeStamp(request.Period);
                    obj.Value = request.Value;
                    obj.Id = obj.Id;
                    obj.KodObject = request.KodObject;

                    dbcont.Telemetries.Update(obj);
                    dbcont.SaveChanges();
                    return Helper.GTelemetryFromTelemetry(obj);
                }
                catch { }
            }
        }
        return Helper.GTelemetryEmpty();
    }


    [Authorize("GAuth")]
    public override async Task<GTelemetry> Post(GTelemetry request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            try
            {
                var nobj = Helper.TelemetryFromGTelemetry(request);
                var res = await dbcont.AddAsync(nobj);
                await dbcont.SaveChangesAsync();
                var rgobj = Helper.GTelemetryFromTelemetry(res.Entity);
                return rgobj;
            }
            catch { }
        }
        return Helper.GTelemetryEmpty();
    }

    [Authorize("GAuth")]
    public override async Task<GTelemetry> GetById(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            try
            {
                var obj = await dbcont.Telemetries.FindAsync(request.Id);
                if (obj != null)
                {
                    var gobj = Helper.GTelemetryFromTelemetry(obj);
                    return gobj;
                }
            }
            catch { }
        }
        return Helper.GTelemetryEmpty();
    }


    [Authorize("GAuth")]
    public override Task<GTelemetrys> GetForDate(Google.Protobuf.WellKnownTypes.Timestamp request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var datest = Helper.ToMoscowFromTimeStamp(request);
            var dateEnd = datest.AddDays(1);
            GTelemetrys gobjs = new GTelemetrys();

            try
            {
                var iqtelemetries = dbcont.Telemetries.Where(tt => tt.Period >= datest && tt.Period < dateEnd)
                                                                      ?.Select(obj => Helper.GTelemetryFromTelemetry(obj));

                if (iqtelemetries != null && iqtelemetries.Count() > 0)
                {
                    gobjs.HasValue = true;
                    gobjs.Items.Add(iqtelemetries.ToArray());
                    return Task.FromResult(gobjs);
                }
            }
            catch { }
        }
        return Task.FromResult<GTelemetrys>(Helper.GTelemetrysEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GTelemetrys> GetForPeriod(GTelemetryPeriod request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var datest = Helper.ToMoscowFromTimeStamp(request.Begin);
            var dateEnd = Helper.ToMoscowFromTimeStamp(request.End); ;
            GTelemetrys gobjs = new GTelemetrys();
            try
            {
                var iqtelemetries = dbcont.Telemetries.Where(tt => tt.Period >= datest && tt.Period < dateEnd)
                                                                      ?.Select(obj => Helper.GTelemetryFromTelemetry(obj));

                if (iqtelemetries != null && iqtelemetries.Count() > 0)
                {
                    gobjs.HasValue = true;
                    gobjs.Items.Add(iqtelemetries.ToArray());
                    return Task.FromResult(gobjs);
                }
            }
            catch { }
        }
        return Task.FromResult<GTelemetrys>(Helper.GTelemetrysEmpty());
    }


    [Authorize("GAuth")]
    public override Task<GTelemetrys> GetForPeriodForObjectByKod(GTelemetryPeriodForObject request, ServerCallContext context)
    {
        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        _logger.LogInformation($"TimeZoneInfo.Local.StandardName : {TimeZoneInfo.Local.StandardName }; offset: {offset} ;") ;
        using (var dbcont = new db1Context())
        {
            var datest = Helper.ToDateTimFromTimeStamp(request.GTelemetryPeriod.Begin);
            var dateEnd = Helper.ToDateTimFromTimeStamp(request.GTelemetryPeriod.End); 
            int objKod = request.KodObject.Id;
           
            _logger.LogInformation($"datest.ToUniversalTime: {datest.ToUniversalTime()} datest.Kind: {datest.Kind} ; datest: {datest.ToString()}; dateEnd:{dateEnd.ToString()}; objKod: {objKod}") ;
          
            GTelemetrys gobjs = new GTelemetrys();
            try
            {
                var  telms = dbcont.Telemetries.Where(tt => tt.Period >= datest && tt.Period < dateEnd
                                                                                        && tt.KodObject == objKod).ToList() ;
                var iqtelemetries =     telms?.Select(obj => Helper.GTelemetryFromTelemetry(obj))
                                                                      ?
                                                                      .ToList();

                
                if (iqtelemetries != null && iqtelemetries.Count() > 0)
                {
                    _logger.LogInformation($"telms id: {telms.FirstOrDefault().Id}; telms period: {telms.FirstOrDefault().Period.ToString()}  ") ;
                    
                     var iqtf = iqtelemetries.FirstOrDefault() ;
                
                    _logger.LogInformation($"iqtelms id: {iqtf.Id}; telms period: {iqtf.Period.ToDateTime().ToString()}  ") ;
                    
                    gobjs.HasValue = true;
                    gobjs.Items.Add(iqtelemetries.ToArray());
                    return Task.FromResult(gobjs);
                }
            }
            catch(Exception exp) { 
                var msg = exp.Message ;

            }
        }
        return Task.FromResult<GTelemetrys>(Helper.GTelemetrysEmpty());
    }

    [Authorize("GAuth")]
    public override async Task<Empty> TrainingObj(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {

            try
            {
                var res = await dbcont.Database.ExecuteSqlRawAsync("CALL training({0})", request.Id);
            }
            catch (Exception e)
            {
                string txt = e.Message;

            }
        }
        return new Empty { };
    }

    [Authorize("GAuth")]
    public override async Task<Empty> CheckingObj(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {

            try
            {
                var res = await dbcont.Database.ExecuteSqlRawAsync("CALL checking({0})", request.Id);
            }
            catch (Exception e)
            {
                string txt = e.Message;

            }
        }
        return new Empty { };
    }

    //GetLastTrainedRecId

    //[ClaimsAuthorizationAttribute(ClaimType = ClaimTypes.NameIdentifier,ClaimValue ="5")]
    [Authorize("GAuth")]
    public override async Task<GObjectId> GetLastTrainedRecId(Google.Protobuf.WellKnownTypes.Empty emp, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {

            try
            {
                int? maxId = await dbcont.TrainingResults?.MaxAsync(aa => aa.Id);

                if (maxId != null)
                {
                    return new GObjectId { Id = maxId ?? -1 };
                }
            }
            catch (Exception e)
            {
                string txt = e.Message;

            }
        }
        return new GObjectId { Id = -1 };
    }


    [Authorize("GAuth")]
    public override async Task<GUploadCount> UploadTelemetry(Grpc.Core.IAsyncStreamReader<GWitsml> gwdata, ServerCallContext context)
    {
        var watch = new System.Diagnostics.Stopwatch();
        var watchdb = new System.Diagnostics.Stopwatch();
        var wbld = WebApplication.CreateBuilder();
        var constr = wbld.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value ?? "";

        watch.Start();
        long count = 0;
        List<byte> resBytes = new List<byte>();
        while (await gwdata.MoveNext())
        {
            var b = gwdata.Current;
            resBytes.AddRange(b.Data);
           // count += b.Data.Count();
        }

        //stack
        //1- kod
        //2- type
        //3- period y
        //4- period m
        //5- period d
        //6- period h
        //7- period m 
        int tss = 7 * sizeof(int);
        var zwfile = resBytes.GetRange(0, resBytes.Count - tss);
        var intObjBts = resBytes.GetRange(resBytes.Count - tss, sizeof(int));
        int objcode = BitConverter.ToInt32(intObjBts.ToArray());

        var intTypeBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int), sizeof(int));
        int type = BitConverter.ToInt32(intTypeBts.ToArray());

        var intYearBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int) * 2, sizeof(int));
        int year = BitConverter.ToInt32(intYearBts.ToArray());

        var intMonthBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int) * 3, sizeof(int));
        int month = BitConverter.ToInt32(intMonthBts.ToArray());

        var intDayBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int) * 4, sizeof(int));
        int day = BitConverter.ToInt32(intDayBts.ToArray());

        var intHourBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int) * 5, sizeof(int));
        int hour = BitConverter.ToInt32(intHourBts.ToArray());

        var intMinBts = resBytes.GetRange(resBytes.Count - tss + sizeof(int) * 6, sizeof(int));
        int min = BitConverter.ToInt32(intMinBts.ToArray());

        DateTime startDateTime = new DateTime(year,month,day,hour,min,0) ;    
        var dcms = new MemoryStream(zwfile.ToArray());
        var dcmso = new MemoryStream();
        using (System.IO.Compression.DeflateStream dstream = new System.IO.Compression.DeflateStream(dcms, System.IO.Compression.CompressionMode.Decompress))
        {
            dstream.CopyTo(dcmso);
        }
        await dcmso.FlushAsync();
        dcmso.Position = 0;

       // var tDT = new DateTime(year, month, day, hour, min, 0);
        var serilizer = new XmlSerializer(typeof(BadWitsml.logs));
        int batchCount = 15000;
        try
        {
            var deser = serilizer.Deserialize(dcmso);
            watch.Stop();
            watchdb.Start();
            if (deser != null && deser is BadWitsml.logs && ((BadWitsml.logs)deser).Log.data.Count() > 0)
            {
                var data = ((BadWitsml.logs)deser).Log.data.ToList();

                // using (var dbcont = new db1Context())
                {
                    try
                    {
                        Queue<TelemetryUploadValDate> valqto = new Queue<TelemetryUploadValDate>();
                      
                        {

                            foreach (var val in data)
                            {
                                valqto.Enqueue(new TelemetryUploadValDate {Val = val, Time = startDateTime});
                                startDateTime = startDateTime.AddMinutes(1) ;
                                if (valqto.Count() > batchCount)
                                {
                                    int lcount = valqto.Count ;
                                   
                                    var sres = await SendToPostgree(valqto, type, objcode,  constr);
                                    while (!sres)
                                    {
                                        System.Threading.Thread.Sleep(1000);
                                        sres = await SendToPostgree(valqto, type, objcode,  constr);
                                    }
                                    count+=lcount ;
                                    valqto.Clear();
                                }
                            }

                        }

                        if (valqto.Count() > 0)
                        {
                            int lcount = valqto.Count ;
                            var sres = await SendToPostgree(valqto, type, objcode, constr);
                            while (!sres)
                            {
                                System.Threading.Thread.Sleep(1000);
                                sres = await SendToPostgree(valqto, type, objcode, constr);
                            }
                            count+=lcount ;
                            valqto.Clear();
                            
                        }

                    }
                    catch (Exception exp)
                    {
                        var msg = exp.Message;

                    }
                }
            }
        }
        catch (Exception exp)
        {
            var str = exp.Message;
        }
        watchdb.Stop();
        long parsedectime = watch.ElapsedMilliseconds;
        long watchdbtime = watchdb.ElapsedMilliseconds;
        long totalTime = parsedectime+watchdbtime ;
        return new GUploadCount { Count = count, TimeTotal =totalTime };
    }

    private async Task<bool> SendToPostgree(Queue<TelemetryUploadValDate> stk, int type, int objcode, string constr)
    {
       // var ldt = new DateTime(dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second) ;
                             
        try
        {
            using (var npqsqlcon = new NpgsqlConnection(constr))
            {
                await npqsqlcon.OpenAsync();

                await using (var batch = new NpgsqlBatch(npqsqlcon))
                {
                    NpgsqlBatchCommand npqbcmd;
            
                    string cmdStr = @"INSERT INTO public.telemetry(type, period, value, kod_object) VALUES ({0},'{1}' , {2}, {3});";

                    while (stk.Count > 0)
                    {
                        //20221011 13:15:02 -- timestamp
                        var upval = stk.Dequeue();
                        var csmdstr = String.Format(cmdStr, type, $"{upval.Time.Year.ToString().PadLeft(2,'0')}{upval.Time.Month.ToString().PadLeft(2,'0')}{upval.Time.Day.ToString().PadLeft(2,'0')} {upval.Time.Hour.ToString().PadLeft(2,'0')}:{upval.Time.Minute.ToString().PadLeft(2,'0')}:{00}", upval.Val.ToString(CultureInfo.CreateSpecificCulture("en-GB")), objcode);
                        //dt=dt.AddMinutes(1);
                        npqbcmd = new NpgsqlBatchCommand(csmdstr);                        
                        batch.BatchCommands.Add(npqbcmd);
                      
                    }
                    var reader = await batch.ExecuteNonQueryAsync();                              
                }
                await npqsqlcon.CloseAsync();
            }
        }
        catch (Exception exp)
        {
            string msg = exp.Message;
          //  string innMsg = exp.InnerException.Message;
          //  string stack = exp.StackTrace;
          //  dt = ldt ;
            return false;
        }
        return true;
    }

    private struct TelemetryUploadValDate
    {
        public decimal Val {get ; set;} 
        public DateTime Time {get ; set;} 
    }
}