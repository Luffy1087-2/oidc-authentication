using System.Security.Claims;

namespace oidc_authentication.Model
{
    public record SessionModel(IEnumerable<Claim>? Claims, IDictionary<string, string?>? Metadata);
}
