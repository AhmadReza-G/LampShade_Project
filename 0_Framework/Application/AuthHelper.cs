using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;
            var claims = _contextAccessor.HttpContext
                    .User
                    .Claims.ToList();

            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId")!.Value);
            result.Username = claims.FirstOrDefault(x => x.Type == "Username")!.Value;
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value);
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
            result.Role = claims.FirstOrDefault(x => x.Type == "RoleName")!.Value;
            result.ProfilePhoto = claims.FirstOrDefault(x => x.Type == "ProfilePhoto")!.Value;

            return result;
        }

        public string? CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext
                    .User
                    .Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Role)
                    !.Value;
            return null;
        }

        public bool IsAuthenticated()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            //if (claims.Count > 0)
            //    return true;
            //return false;
            return claims.Count > 0;
        }

        public void Signin(AuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim("RoleName", account.Role),
                new Claim("Username", account.Username),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("ProfilePhoto", account.ProfilePhoto ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}