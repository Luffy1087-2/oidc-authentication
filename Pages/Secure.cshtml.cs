using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using oidc_authentication.Model;
using System.Security.Claims;

namespace sso_authentication.Pages
{
  public class SecureModel : PageModel
  {
    public SessionModel? SessionModel { get; set; }

    public async Task OnGet()
    {
      AuthenticateResult authResult = await HttpContext.AuthenticateAsync();
      SessionModel = new SessionModel(authResult?.Principal?.Claims, authResult?.Properties?.Items);
    }
  }
}
