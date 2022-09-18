using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using grpcserv1.Helpers;
using grpcserv1.Tokens ;
namespace grpcserv1.Services;


public class GGetTokensService: GGetTokensServ.GGetTokensServBase
{

     private readonly ILogger<GNarabotkaService> _logger;
    public GGetTokensService(ILogger<GNarabotkaService> logger)
    {
        _logger = logger;
    }


    [Authorize("GAuth")]
    public override Task<GClaim> GetToken(GGetTokenClaims request, ServerCallContext context)
    {
        if(request!=null && request.Claims.Count()>0)
        {
            var token =  TokenGenerator.Generate(6,request.Claims.Select(ccc => ccc.Uri).ToArray()) ;
            return Task.FromResult<GClaim>( new GClaim { Uri = token });
        }
        return Task.FromResult<GClaim>(new GClaim { });
    }
    
}