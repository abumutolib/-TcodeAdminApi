using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;

namespace Application
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<(Result Result, string UserId)> CreateUserAsync(User user, string userName, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task<Result> DeleteUserAsync(string userId);

        //Sign manager methods
        Task SignOutAsync();
        Task<(bool Signed, ApplicationUser User)> IsSignedIn(ClaimsPrincipal user);
        Task SignInAsync(ApplicationUser user, bool isPersistent);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<IEnumerable<object>> GetExternalAuthenticationSchemesAsync();

        IdentityOptions Options { get; set; }
    }
}
