using Microsoft.AspNetCore.Diagnostics;

namespace BetHive.Wallet.Api.Filters
{
    internal class LogExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // log error here!
            return new ValueTask<bool>(true);
        }
    }
}
