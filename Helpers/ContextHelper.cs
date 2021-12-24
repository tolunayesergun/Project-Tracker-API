using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace TeamExpenseAPI.Helpers
{
    public static class ContextHelper
    {
        public static int GetUserID(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return -1;
            if (identity.Claims.Count() == 0) return -1;
            var UserID = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var canParsed = int.TryParse(UserID, out int intUserId);
            return canParsed ? intUserId : -1;
        }
    }
}