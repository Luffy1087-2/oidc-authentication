using DotEnv.Core;
using oidc_authentication.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
new EnvLoader().Load();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(o =>
{
  o.DefaultScheme = LayerSchemas.Cookie;
  o.DefaultChallengeScheme = LayerSchemas.Oidc;
})
.AddCookie(authenticationScheme: LayerSchemas.Cookie, (opt) =>
{
  opt.Cookie.Name = "oidc-auth";
  opt.ExpireTimeSpan = TimeSpan.FromHours(8);
})
.AddOpenIdConnect(LayerSchemas.Oidc, o =>
{
  o.Authority = "https://demo.duendesoftware.com";
  o.ClientId = "interactive.public";
  o.MapInboundClaims = false;
  o.Scope.Clear();
  o.Scope.Add("openid");
  o.SaveTokens = true; // Store session and refresh tokens into the metadata
  o.UsePkce = true;
  o.ResponseType = "code";
});

WebApplication app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages()
    .RequireAuthorization();
app.MapGet("/", () => Results.Redirect("Secure"));
app.Run();