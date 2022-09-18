using grpcserv1;
using grpcserv1.Services;
using Npgsql;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//https://51.250.110.175 -- сайт
//http://51.250.109.24:5000/ -- grpc сервис
if (args.Count() > 0 && args[0].Equals("gentoken"))
{
    Console.WriteLine(grpcserv1.Tokens.TokenGenerator.Generate(6, 
                                                                        new string[] {
                                                                            "/TelemetryServ/GetLastTrainedRecId",
                                                                            "/TelemetryServ/GetForPeriod"
                                                                        }));
    return;
}

var builder = WebApplication.CreateBuilder(args);

//var envname=builder.Environment.EnvironmentName ;

builder.Services.AddGrpc();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jj =>
    {
        jj.SecurityTokenValidators.Add(new grpcserv1.Tokens.TokenValidator());
        jj.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                c.NoResult();
                c.Response.StatusCode = 500;
                c.Response.ContentType = "text/plain";
                return c.Response.WriteAsync("Нет доступа. не прошли аутентификацию!ы");
            }

        };

    });
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("GAuth", pol =>
    {
        pol.AddRequirements(new grpcserv1.Tokens.GrpcServAuth());
    });
});
builder.Services.AddHttpContextAccessor() ;
builder.Services.AddSingleton<IAuthorizationHandler, grpcserv1.Tokens.GrpcServAuthHandler>();
//var provider=builder.Configuration.GetFileProvider() ;

//builder.Logging.AddConsole() ;
//var props =builder.Host.Properties;

var app = builder.Build();



app.MapGrpcService<GObjectsService>();
app.MapGrpcService<GNarabotkaService>();
app.MapGrpcService<GRequestService>();
app.MapGrpcService<GTelemetryService>();
app.MapGrpcService<GGetTokensService>();
app.MapGet("/", () => "Second Variety")
    .RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
