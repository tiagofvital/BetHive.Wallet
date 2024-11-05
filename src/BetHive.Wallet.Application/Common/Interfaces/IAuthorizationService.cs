using ErrorOr;

namespace BetHive.Wallet.Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        ErrorOr<Success> AuthorizeCurrentUser<T>();
    }
}