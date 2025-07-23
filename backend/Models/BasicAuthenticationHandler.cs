using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using backend.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend.Models;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    UserDbContext context)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            /* 1. Check if the Authorization header exists in the request. */
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            /* 2. Retrieve the Authorization header value */
            var authorizationHeader = Request.Headers["Authorization"].ToString();

            /* 3. Parse the header to seperate scheme and parameter. */
            if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            /* 4. Verify that the scheme is "Basic" */
            if (!"Basic".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Authorization Scheme.");
            }

            /* 5. Decode the Base64-encoded credentials ("username-password") */
            var credentialBytes = Convert.FromBase64String(headerValue.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":", 2);

            /* 6. Validate that the ecoded string contains exactly username and password */
            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            /* 7. Extract username and password from credentials */
            var email = credentials[0];
            var password = credentials[1];

            /* 8. Query the database for the user by email. */
            var user = await context.Users2.SingleOrDefaultAsync(user => user.Email == email);
            if (user == null || !PasswordHasherTwo.VerifyPassword(user.PasswordHash, password))
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            /* 9. Create claims that represent the user's identity and roles. */
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            /* 10. Split roles by comma and add multiple claims */
            var roles = user.Role.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            /* 11. Create a ClaimsIdentity with the authentication scheme name. */
            var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);

            /* 12. Create ClaimsPrincipal that holds the ClaimsIdnetity  */
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            /* 13. Create an AuthenticationTicket which encapsulates the user's Idnetity and scheme info. */
            var authentiactionTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            /* 14. Return success result with the AuthenticationTicket indicating successfully */
            return AuthenticateResult.Success(authentiactionTicket);
        }
        catch
        {
            return AuthenticateResult.Fail("Error occurred during authentication");
        }
    }
}