using BetHive.Wallet.Application.Wallets.Commands.CreateWallet;
using BetHive.Wallet.Application.Wallets.Commands.Deposit;
using BetHive.Wallet.Application.Wallets.Queries;
using BetHive.Wallet.Contracts.Wallets;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetHive.Wallet.Api.Controllers
{
    [Route("api/v1/tenants/{tenantId:int}")]
    [AllowAnonymous]
    public class WalletController(ISender _mediator) : ApiController
    {
        [HttpPost("wallets")]
        public async Task<IActionResult> Create(int tenantId, CreateWalletRequest createWalletRequest)
        {
            var command = new CreateWalletCommand(tenantId, createWalletRequest.UserId);

            var result = await _mediator.Send(command);

            return result.Match(
                wallet => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { wallet.TenantId, wallet.Id },
                    value: wallet),
                Problem);
        }

        [HttpGet("wallets")]
        public async Task<IActionResult> Get(
            int tenantId,
            [FromQuery] Guid? userId)
        {
            var query = new GetWalletsQuery(tenantId, userId);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem);
        }

        [HttpGet("wallets/{id:guid}")]
        public async Task<IActionResult> GetById(int tenantId, Guid id)
        {
            var query = new GetWalletByIdQuery(tenantId, id);

            var result = await _mediator.Send(query);

            return result.Match(
                Ok,
                Problem);
        }

        [HttpPost("wallets/{walletId:guid}/deposits")]
        public async Task<IActionResult> Deposit(int tenantId, Guid walletId, DepositRequest request)
        {
            var command = new DepositCommand(tenantId, walletId, request.Amount);

            var result = await _mediator.Send(command);

            return result.Match(
                  wallet => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { tenantId = wallet.TenantId, wallet.Id },
                    value: wallet),
                  Problem);
        }

        [HttpPost("wallets/{walletId:guid}/withdraws")]
        public async Task<IActionResult> Withdraw(int tenantId, Guid walletId, WithdrawRequest request)
        {
            var command = new WithdrawCommand(tenantId, walletId, request.Token, request.Amount);

            var result = await _mediator.Send(command);

            return result.Match(
                  wallet => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { tenantId = wallet.TenantId, wallet.Id },
                    value: wallet),
                  Problem);
        }
    }
}