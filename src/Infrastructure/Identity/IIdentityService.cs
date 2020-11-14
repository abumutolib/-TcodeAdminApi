using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public interface IIdentityService
    {
        Task SetRefreshToken(string username, string token);
        Task<(string id, string username, IList<string> roles)> RefreshToken(string token);
        Task<(string id, IList<string> roles)> Authenticate(string username, string password);
    }
}