using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;

namespace Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUserService.UserId ?? string.Empty;
                string userName = string.Empty;

                if (!string.IsNullOrEmpty(userId))
                {
                    userName = _currentUserService.Username;
                }

                _logger.LogInformation("TcodingTj Request: {Name} {@UserId} {@UserName} {@Request}",
                    requestName, userId, userName, request);
            }, cancellationToken);
        }
    }
}
