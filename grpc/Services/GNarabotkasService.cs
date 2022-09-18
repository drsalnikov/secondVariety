using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using grpcserv1.Helpers;

namespace grpcserv1.Services;


public class GNarabotkaService : NarabotkaServ.NarabotkaServBase
{
    private readonly ILogger<GNarabotkaService> _logger;
    public GNarabotkaService(ILogger<GNarabotkaService> logger)
    {
        _logger = logger;
    }


    [Authorize("GAuth")]
    public override Task<Empty> Delete(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Narabotka? obj = dbcont.Narabotkas.Find(request.Id);
            if (obj != null)
            {
                dbcont.Narabotkas.Remove(obj);
                dbcont.SaveChanges();
                return Task.FromResult<Empty>(new Empty());
            }
        }

        return Task.FromResult<Empty>(new Empty { });
    }

    [Authorize("GAuth")]
    public override Task<GNarabotka> Put(GNarabotka request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            Narabotka? obj = dbcont.Narabotkas.Find(request.Id);
            if (obj != null)
            {
                obj.KodObject = request.KodObject;
                obj.Data = Helper.ToMoscowDateOnlyFromTimeStamp(request.Data);
                obj.Val = request.Val;
                obj.Id = obj.Id;

                dbcont.Narabotkas.Update(obj);
                dbcont.SaveChanges();
                return Task.FromResult(Helper.GNarabotkaFromNarabotka(obj));
            }
        }
        return Task.FromResult<GNarabotka>(Helper.GNarabotkaEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GNarabotka> Post(GNarabotka request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var nobj = Helper.NarabotkaFromGNarabotka(request);
            var res = dbcont.Add(nobj);
            dbcont.SaveChanges();
            var rgobj = Helper.GNarabotkaFromNarabotka(res.Entity);
            return Task.FromResult(rgobj);
        }
        return Task.FromResult<GNarabotka>(Helper.GNarabotkaEmpty());
    }

    [Authorize("GAuth")]
    public override Task<GNarabotka> GetById(GObjectId request, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            var obj = dbcont.Narabotkas.Find(request.Id);
            if (obj != null)
            {
                var gobj = Helper.GNarabotkaFromNarabotka(obj);
                return Task.FromResult(gobj);
            }
        }
        return Task.FromResult<GNarabotka>(Helper.GNarabotkaEmpty());
    }
    [Authorize("GAuth")]
    public override Task<GNarabotkas> GetAll(Empty request, ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        using (var dbcont = new db1Context())
        {
            GNarabotkas gobjs = new GNarabotkas();
            var agobjs = dbcont.Narabotkas.Select(obj => Helper.GNarabotkaFromNarabotka(obj)).ToArray();
            gobjs.Items.Add(agobjs);
            return Task.FromResult(gobjs);
        }

        return Task.FromResult<GNarabotkas>(Helper.GNarabotkasEmpty());
    }
    [Authorize("GAuth")]
    public override Task<GNarabotkas> GetByObjectKod(GObjectId objectKod, ServerCallContext context)
    {
        using (var dbcont = new db1Context())
        {
            GNarabotkas gobjs = new GNarabotkas();
            var narabs = dbcont.Narabotkas.Where(nn => nn.KodObject == objectKod.Id)
                                                        ?.Select(obj => Helper.GNarabotkaFromNarabotka(obj));

            if (narabs != null && narabs.Count() > 0)
            {
                gobjs.Items.Add(narabs.ToArray());
                return Task.FromResult(gobjs);
            }
        }

        return Task.FromResult<GNarabotkas>(Helper.GNarabotkasEmpty());
    }

}