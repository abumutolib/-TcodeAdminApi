using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Infrastructure.Common.Exceptions;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Methods for API Authentication
        public async Task<(string id, IList<string> roles)> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!isValidPassword)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, roles);
        }

        public async Task<(string id, string username, IList<string> roles)> RefreshToken(string token)
        {
            var refreshToken = _context.RefreshTokens.Where(x => x.Token == token).FirstOrDefault();//.FirstOrDefaultAsync();
            if (refreshToken == null)
                throw new NotFoundException("RefreshToken not found", token);

            var user = await _userManager.FindByIdAsync(refreshToken.Id);

            var roles = await _userManager.GetRolesAsync(user);

            if (refreshToken.IsExpired)
                throw new UnauthorizedException("Refres token is expired");

            return (user.Id, user.UserName, roles);
        }

        public async Task SetRefreshToken(string username, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new NotFoundException("User not found", username);

            var refreshToken = await _context.RefreshTokens.FindAsync(user.Id);
            if (refreshToken == null)
            {
                refreshToken = new AspNetUserRefreshToken
                {
                    Id = user.Id,
                    Token = token,
                    Created = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(30),
                };

                _context.RefreshTokens.Add(refreshToken);
            }
            else
            {
                refreshToken.Token = token;
                refreshToken.Created = DateTime.UtcNow;
                refreshToken.Expires = DateTime.UtcNow.AddDays(30);
                _context.RefreshTokens.Update(refreshToken);
            }

            await _context.SaveChangesAsync(new CancellationToken());
        }
    }
}
