using BetHive.Wallet.Application.Common.Interfaces;

using ErrorOr;

namespace BetHive.Wallet.Infrastructure.Security
{
    public class AuthorizationService() : IAuthorizationService
    {
        public ErrorOr<Success> AuthorizeCurrentUser<T>()
        {
            return Result.Success;
        }
    }
}