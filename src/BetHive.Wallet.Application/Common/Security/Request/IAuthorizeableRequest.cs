using MediatR;

namespace BetHive.Wallet.Application.Common.Security.Request
{
    public interface IAuthorizeableRequest<T> : IRequest<T>
    {
    }
}