using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using grpcserv1.Helpers;

namespace grpcserv1.Services;


public class GRequestService : RequestServ.RequestServBase
{
    private readonly ILogger<GRequestService> _logger;
    public GRequestService(ILogger<GRequestService> logger)
    {
        _logger = logger;
    }

    [Authorize("GAuth")]
    public override Task<Empty> Delete(GRequestId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Request? obj = dbcont.Requests.Find(request.Id);
            if (obj != null)
            {
                dbcont.Requests.Remove(obj);
                dbcont.SaveChanges();
                return Task.FromResult<Empty>(new Empty());
            }
        }

        return Task.FromResult<Empty>(new Empty{});
    }

[Authorize("GAuth")]
 public override Task<Empty> DeleteByNum(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Request? obj = dbcont.Requests.FirstOrDefault(rrr => rrr.Num == request.Id);
            if (obj != null)
            {
                dbcont.Requests.Remove(obj);
                dbcont.SaveChanges();
                return Task.FromResult<Empty>(new Empty());
            }
        }

        return Task.FromResult<Empty>(new Empty{});
    }

    [Authorize("GAuth")]
    public override Task<GRequest> Put(GRequest request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Request? obj = dbcont.Requests.Find(request.Id);
            if (obj != null)
            {

                obj.Num = obj.Num;
                obj.Data = Helper.ToMoscowDateOnlyFromTimeStamp(request.Data);
                obj.KodObject = request.KodObject;
                obj.DateFrom = Helper.ToMoscowDateOnlyFromTimeStamp(request.DateFrom);
                obj.DateTo = Helper.ToMoscowDateOnlyFromTimeStamp(request.DateTo);
                obj.Status = request.Status;
                obj.DateFromFakt = Helper.ToMoscowDateOnlyFromTimeStamp(request.DateFromFakt);
                obj.DateToFakt = Helper.ToMoscowDateOnlyFromTimeStamp(request.DateToFakt);
                obj.Comment = request.Comment;
                obj.Id = obj.Id;

                dbcont.Requests.Update(obj);
                dbcont.SaveChanges();
                return Task.FromResult(Helper.GRequestFromRequest(obj));
            }
        }
        return Task.FromResult<GRequest>(Helper.GRequestEmpty());
    }

   [Authorize("GAuth")]
    public override Task<GRequest> Post(GRequest request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var nobj = Helper.RequestFromGRequest(request);
            var res = dbcont.Add(nobj);
            dbcont.SaveChanges();
            var rgobj = Helper.GRequestFromRequest(res.Entity);
            return Task.FromResult(rgobj);
        }
        return Task.FromResult<GRequest>(Helper.GRequestEmpty());
    }

   [Authorize("GAuth")]
    public override Task<GRequest> GetById(GRequestId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var obj = dbcont.Requests.Find(request.Id);
            if (obj != null)
            {
                var gobj = Helper.GRequestFromRequest(obj);
                return Task.FromResult(gobj);
            }
        }
        return Task.FromResult<GRequest>(Helper.GRequestEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GRequests> GetAll(Empty request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            GRequests gobjs = new GRequests();
            var agobjs = dbcont.Requests.Select(obj => Helper.GRequestFromRequest(obj)).ToArray();
            gobjs.Items.Add(agobjs);
            return Task.FromResult(gobjs);
        }

        return Task.FromResult<GRequests>(Helper.GRequestsEmpty());
    }


    [Authorize("GAuth")]
    public override Task<GRequests> GetByObjectKod(GObjectId objectKod, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            GRequests gobjs = new GRequests();
            var reqs = dbcont.Requests.Where(nn => nn.KodObject == objectKod.Id)
                                                        ?.Select(obj => Helper.GRequestFromRequest(obj));

            if (reqs != null && reqs.Count() > 0)
            {
                gobjs.Items.Add(reqs.ToArray());
                return Task.FromResult(gobjs);
            }
        }

        return Task.FromResult<GRequests>(Helper.GRequestsEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GRequest> GetByNum(GObjectId objectKod, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            
            var req = dbcont.Requests?.FirstOrDefault(nn => nn.Num == objectKod.Id)
                                                       ;

            if (req != null )
            {
                return Task.FromResult(Helper.GRequestFromRequest(req));
            }
        }

        return  Task.FromResult(Helper.GRequestEmpty());
    }


}