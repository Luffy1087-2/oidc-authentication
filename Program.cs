using DotEnv.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
new EnvLoader().Load();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(defaultScheme: "Cookie")
    .AddCookie(authenticationScheme: "Cookie", (opt) =>
    {
      opt.Cookie.Name = "sso-auth";
      opt.ExpireTimeSpan = TimeSpan.FromHours(8);
      opt.LoginPath = "/login";
    });

WebApplication app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages()
    .RequireAuthorization();
app.MapGet("/", () => Results.Redirect("/login"));
app.Run();