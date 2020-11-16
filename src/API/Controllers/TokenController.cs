using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API.OAuth;
using API.OAuth.Models;
using Infrastructure.Identity;
using Application.Common.Exceptions;

namespace API.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly string _audienceId;
        private readonly TokenHelper _tokenHelper;
        private readonly IIdentityService _identityService;

        public TokenController(TokenHelper tokenHelper, IIdentityService identityService, IConfiguration config)
        {
            _tokenHelper = tokenHelper;
            _identityService = identityService;
            _audienceId = config.GetValue<string>("JwtConfig:AudienceId");
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!_audienceId.Equals(request.ClientId))
            {
                throw new BadRequestException("Invalid ClientId");
            }

            var result = await _identityService.Authenticate(request.Username, request.Password);
            var refreshToken = _tokenHelper.GenerateRefreshToken();
            await _identityService.SetRefreshToken(request.Username, refreshToken);

            var output = new LoginResponse
            {
                AccessToken = _tokenHelper.GenerateAccessToken(result.id, request.Username, result.roles),
                RefreshToken = refreshToken
            };
            return Ok(output);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!_audienceId.Equals(request.ClientId))
            {
                throw new BadRequestException("Invalid ClientId");
            }

            var result = await _identityService.RefreshToken(request.Token);
            var refreshToken = _tokenHelper.GenerateRefreshToken();
            await _identityService.SetRefreshToken(result.username, refreshToken);

            var output = new LoginResponse
            {
                AccessToken = _tokenHelper.GenerateAccessToken(result.id, result.username, result.roles),
                RefreshToken = refreshToken
            };

            return Ok(output);
        }
    }
}
