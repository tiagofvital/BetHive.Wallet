﻿using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Contracts.Wallets;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Queries;

[Authorize(Permissions = Permission.Wallet.Read, Policies = Policy.SelfOrAdmin)]
public record GetWalletByIdQuery(int TenantId, Guid Id) : IRequest<ErrorOr<WalletResult>>;
