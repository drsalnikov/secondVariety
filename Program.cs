using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using SecondVariety.Areas.Identity.Data;
using Radzen;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SecondVariety
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


      // builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
      //           .AddEntityFrameworkStores<dbIdentityContext>();

      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("ElevatedRights", policy =>
        policy.RequireRole("Administrator", "Manager", "Worker"));
      });

      ///
      builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                      .AddRoles<IdentityRole>()
                      .AddEntityFrameworkStores<dbIdentityContext>();

      builder.Services.AddDbContext<dbIdentityContext>(options =>
                                                       options.UseNpgsql(connectionString));


      builder.Services.AddDatabaseDeveloperPageExceptionFilter();



      builder.Services.AddRazorPages();

      ///

      ///
      builder.Services.AddServerSideBlazor();

      ///


      ///
      //builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

      builder.Services.AddScoped<DialogService>();
      builder.Services.AddScoped<NotificationService>();
      builder.Services.AddScoped<TooltipService>();
      builder.Services.AddScoped<ContextMenuService>();
      builder.Services.AddScoped<GrpcClientService>();
      var app = builder.Build();

      AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllers();
      app.MapBlazorHub();
      app.MapFallbackToPage("/_Host");

      ///create users and roles
      // createRolesandUsers(app.Services,"Worker","rabotnik@mail.ru","rabotnik","Ghjiksq$827");
      // return ;
      ///
      app.Run();
    }

/*

    user.UserName = "mang";
    user.Email = "manager@manager.com";
    string userPWD = "Htrdbtv9324$";

    user.UserName = "admin";
    user.Email = "admin@admin.com";
    string userPWD = "Pfqrfgtht,trufq672!";

    "Worker","rabotnik@mail.ru","rabotnik","Ghjiksq$827"
*/

    ///create users and roles
    private static void createRolesandUsers(IServiceProvider sprov,string urole,string mail,string uname,string pass)
    {
      using (var scope = sprov.CreateScope())
      {
        var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

        // "Administrator", "Manager", "Worker"    
        var manr = roleManager.Roles.FirstOrDefault(rr => rr.Name.Equals(urole));
        
        if (manr == null)
        {

          var role = new IdentityRole();
          role.Name = urole;
          var rtsk = roleManager.CreateAsync(role);
          rtsk.Wait();

          var user = new IdentityUser();
          user.UserName = uname;
          user.Email = mail;


          var usertsk = userManager.CreateAsync(user, pass);
          usertsk.Wait();

          if (usertsk.Status == TaskStatus.RanToCompletion && usertsk.Result.Succeeded)
          {
            var result1 = userManager.AddToRoleAsync(user, urole);
            result1.Wait();
            if (result1.Status == TaskStatus.RanToCompletion)
            {
              var usertsk1 = userManager.FindByNameAsync(uname);
              usertsk1.Wait();
              if (usertsk1.Status == TaskStatus.RanToCompletion)
              {
                var userIdTask = userManager.GetUserIdAsync(usertsk1.Result);
                userIdTask.Wait();
                if (userIdTask.Status == TaskStatus.RanToCompletion)
                {
                  var codeTask = userManager.GenerateEmailConfirmationTokenAsync(usertsk1.Result);
                  codeTask.Wait();
                  if (codeTask.Status == TaskStatus.RanToCompletion)
                  {
                    var resultTask = userManager.ConfirmEmailAsync(usertsk1.Result, codeTask.Result);
                    resultTask.Wait();

                    var res = resultTask.Result;
                  }
                }
              }
            }


          }


        }

      }
    }


    ///
  }
}