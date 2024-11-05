using ErrorOr;

namespace BetHive.Wallet.Domain.Wallets
{
    public static class WalletErrors
    {
        public static Error CannotCreateWalletWhenUserAlreadyHasOne { get; } = Error.Conflict(
            code: "CustomerWallets.CannotCreateWalletWhenUserAlreadyHasOne",
            description: "Cannot create a wallet when user already has one.");

        public static Error CannotHaveNegativeBalance { get; } = Error.Forbidden(
            code: "CustomerWallets.CannotRemoveFundsWhenBalanceBecomesNegative",
            description: "Cannot remove funds when balance becomes negative.");

        public static Error NotFound { get; } = Error.NotFound(
            code: "CustomerWallets.WalletNotFound",
            description: "Wallet not found.");
        public static Error InvalidToken { get; } = Error.Conflict(
            code: "CustomerWallets.InvalidToken",
            description: "Cannot proceed with invalid token.");
    }
}
