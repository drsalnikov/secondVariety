using Grpc.Core;
using grpcserv1;
using Microsoft.AspNetCore.Authorization;
using grpcserv1.Helpers;

namespace grpcserv1.Services;

public class GObjectsService : ObjectsServ.ObjectsServBase
{
    private readonly ILogger<GObjectsService> _logger;
    public GObjectsService(ILogger<GObjectsService> logger)
    {
        _logger = logger;
    }
    
    /* rpc GetAll(Empty) returns(GObjects);
      rpc GetById(GObjectId) returns(GObject);
      rpc Post(GObject) returns(GObject);
      rpc Put(GObject) returns(GObject);
      rpc Delete(GObjectId) returns(Empty);
      */
    [Authorize("GAuth")]
    public override Task<Empty> Delete(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Object? obj = dbcont.Objects.Find(request.Id);
            if (obj != null)
            {
                dbcont.Objects.Remove(obj) ;
                dbcont.SaveChanges();
                return Task.FromResult<Empty>(new Empty());
            }
        }

        return  Task.FromResult<Empty>(new Empty{});
    }

    [Authorize("GAuth")]
    public override Task<GObject> Put(GObject request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Object? obj = dbcont.Objects.Find(request.Id);
            if (obj != null)
            {
                obj.Kod = obj.Kod;
                obj.Name = request.Name;
                obj.TekNar = request.TekNar;
                obj.LastTo = Helper.ToMoscowDateOnlyFromTimeStamp(request.LastTo);
                obj.ToTime = request.ToTime;
                obj.ToNar = request.ToNar;
                obj.PlanYear = request.PlanYear;
                obj.Koef2 = request.Koef2;
                obj.Koef1 = request.Koef1;
                obj.SredNar = request.SredNar;
                obj.DateFrom = Helper.ToMoscowDateOnlyFromTimeStamp(request.DateFrom);
                obj.NarFrom = request.NarFrom;
                obj.NextTo = Helper.ToMoscowDateOnlyFromTimeStamp(request.NextTo);
                obj.SredNarPlan = request.SredNarPlan;
                obj.Status = request.Status;
                obj.Id = obj.Id;
                obj.TrainingFrom = Helper.ToMoscowDateOnlyFromTimeStamp(request.TrainingFrom);
                obj.TrainingTo = Helper.ToMoscowDateOnlyFromTimeStamp(request.TrainingTo);
                obj.WarningType = request.WarningType;
                obj.WarningTime = Helper.ToMoscowFromTimeStamp(request.WarningTime);
                obj.WarningFrom = Helper.ToMoscowDateOnlyFromTimeStamp(request.WarningFrom);
                obj.WarningSensor = request.WarningSensor;
                obj.ErrorRate = request.ErrorRate;
                dbcont.Objects.Update(obj) ;
                dbcont.SaveChanges() ;
                return Task.FromResult(Helper.GobjectFromObject(obj)) ;
            }
        }
        return Task.FromResult<GObject>(Helper.GObjectEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GObject> Post(GObject request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var nobj = Helper.ObjectFromGObject(request);
            var res = dbcont.Add(nobj);
            dbcont.SaveChanges();
            var rgobj = Helper.GobjectFromObject(res.Entity);
            return Task.FromResult(rgobj);
        }
        return Task.FromResult<GObject>(Helper.GObjectEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GObject> GetById(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var obj = dbcont.Objects.Find(request.Id);
            if (obj != null)
            {
                var gobj = Helper.GobjectFromObject(obj);
                return Task.FromResult(gobj);
            }
        }
        return Task.FromResult<GObject>(Helper.GObjectEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GObjects> GetAll(Empty request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            GObjects gobjs = new GObjects();
            var agobjs = dbcont.Objects.Select(obj => Helper.GobjectFromObject(obj)).ToArray();
            gobjs.Items.Add(agobjs);
            return Task.FromResult(gobjs);
        }

        return Task.FromResult<GObjects>(Helper.GObjectsEmpty());
    }



}
