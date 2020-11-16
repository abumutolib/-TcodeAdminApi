using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }

        public string UserId { get; }
        public string Username { get; }
    }
}
