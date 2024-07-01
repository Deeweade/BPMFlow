using System.Security.Claims;

namespace KPICatalog.API.Middlewares;

public class DevAuthMiddleware
{
    private readonly RequestDelegate _next;

    public DevAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "devuser"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "devuser@rencredit.ru"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "Development");
            context.User = new ClaimsPrincipal(identity);
        }
        catch
        {
            throw;
        }
        finally
        {
            await _next.Invoke(context);
        }
    }
}
