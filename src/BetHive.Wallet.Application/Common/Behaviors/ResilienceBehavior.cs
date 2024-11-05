using ErrorOr;

using MediatR;

using Polly;

namespace BetHive.Wallet.Application.Common.Behaviors
{
    public class ResilienceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await Pipeline.ExecuteAsync(async token => await next(), cancellationToken);
        }

        private static readonly ResiliencePipeline Pipeline = BuildResiliencePipeline();

        private static ResiliencePipeline BuildResiliencePipeline()
        {
            return new ResiliencePipelineBuilder()
                .AddTimeout(TimeSpan.FromSeconds(1)) // Tipically this would be specific per operation, taking in consideration the 95 percentil of response time.
                .AddCircuitBreaker(new Polly.CircuitBreaker.CircuitBreakerStrategyOptions()) // better to fail-fast!
                .Build();
        }
    }
}
