using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace grpcserv1.Tokens
{

    public enum GServicesIds
    {
        objects = 2,
        narabotkas = 4,
        requests = 8,
        telemetries = 16,
        tokens = 32
    }


    /*
     1 rpc GetAll(Empty) returns(GNarabotkas);
     2 rpc GetById(GObjectId) returns(GNarabotka);
     3 rpc GetByObjectKod(GObjectId) returns(GNarabotkas);
     4 rpc Post(GNarabotka) returns(GNarabotka);
     5 rpc Put(GNarabotka) returns(GNarabotka);
     6 rpc Delete(GObjectId) returns(Empty);
     7 rpc GetByNum(GObjectId) returns(GRequest);
     8 rpc DeleteByNum(GObjectId) returns(Empty);
     9 rpc GetForDate(google.protobuf.Timestamp) returns(GTelemetrys);
    10 rpc GetForPeriod(GTelemetryPeriod) returns(GTelemetrys) ;
    11 rpc GetForPeriodForObjectByKod(GTelemetryPeriodForObject) returns(GTelemetrys) ;
    12 rpc GetLastTrainedRecId(google.protobuf.Empty) returns(GObjectId);
    13 rpc TrainingObj(GObjectId) returns(Empty) ;
    14 rpc CheckingObj(GObjectId) returns(Empty) ;
    */
    public enum GMethodsIds
    {
        GetAll

    }
    public class GrpcServAuth : IAuthorizationRequirement
    {
        public GrpcServAuth()
        {

        }
    }
    public class GrpcServAuthHandler : AuthorizationHandler<GrpcServAuth>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GrpcServAuthHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GrpcServAuth requirement)
        {
            var user = context.User;
            if (user.Identity.IsAuthenticated)
            {

                var claimNameId = user.Claims.FirstOrDefault(cc => cc.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));

                if (claimNameId != null)
                {
                    var tp = claimNameId.Type;
                    var val = context.User.Claims.ElementAt(0).Value;

                    if (val == "0")
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                    if (val.Equals("6"))
                    {
                        //http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri
                        var claimURIs= user.Claims.Where(cc => cc.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri"));

                        if (claimURIs!=null && claimURIs.Count()>0 && context.Resource is Microsoft.AspNetCore.Http.DefaultHttpContext)
                        {
                            Microsoft.AspNetCore.Http.DefaultHttpContext? resource = context.Resource as Microsoft.AspNetCore.Http.DefaultHttpContext;
                            if (resource != null)
                            { //"/secondvariety.TelemetryServ/GetLastTrainedRecId"
                                var path = resource.Request.Path.Value.Replace("secondvariety.","");
                               //GGetTokensServ/GetToken
                                if(path!=null&&(!path.Equals("/GGetTokensServ/GetToken"))&&claimURIs.Select(ccc => ccc.Value).Contains(path))   
                                {
                                      context.Succeed(requirement);
                                      return Task.CompletedTask;
                                }                           
                            }
                        }
                    }
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }

    }



    public class TokenValidator : ISecurityTokenValidator
    {

        public TokenValidator()
        {
            mHandle = new JwtSecurityTokenHandler();
        }


        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "SecondVariety",
                ValidAudience = "Clients",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WsetKweKsdfvsdL124.$"))
            };

            var claimsPrincipal = mHandle.ValidateToken(securityToken, tokenValidationParameters, out validatedToken);
            return claimsPrincipal;
        }

        public bool CanReadToken(string securityToken)
        {
            return mHandle.CanReadToken(securityToken);
        }

        public int MaximumTokenSizeInBytes
        {
            get => mHandle.MaximumTokenSizeInBytes;
            set => throw new NotImplementedException();
        }

        public bool CanValidateToken => mHandle.CanValidateToken;

        private JwtSecurityTokenHandler mHandle;
    }

}