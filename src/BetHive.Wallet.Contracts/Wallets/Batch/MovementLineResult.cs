namespace BetHive.Wallet.Contracts.Wallets.Batch
{
    public record MovementLineResult(Guid UserId, float Amount, bool Sucess, string ErrorDescription);
}