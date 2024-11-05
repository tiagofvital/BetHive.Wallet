using BetHive.Wallet.Application.BatchMovements.Commands;
using BetHive.Wallet.Application.BatchMovements.Query;
using BetHive.Wallet.Contracts.Wallets.Batch;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetHive.Wallet.Api.Controllers
{
    [Route("api/v1/tenants/{tenantId:int}/batch")]
    [AllowAnonymous]
    public class BatchController(ISender _mediator) : ApiController
    {
        [HttpGet("movements/{batchId:guid}")]
        public async Task<IActionResult> GetBatch(int tenantId, Guid batchId)
        {
            var command = new GetBatchByIdQuery(batchId, tenantId);

            var result = await _mediator.Send(command);

            return result.Match(Ok, Problem);
        }

        [HttpPost("movements")]
        public async Task<IActionResult> CreateBatch(int tenantId, CreateBatchMovementRequest request)
        {
            var command = new CreateBatchMovementRequestCommand(tenantId, request.ExternalId, request.Movements);

            var result = await _mediator.Send(command);

            return result.Match(
                batch => AcceptedAtAction(
                    actionName: nameof(GetBatch),
                    routeValues: new { tenantId, result.Value.BatchId },
                    value: batch),
                Problem);
        }
    }
}
