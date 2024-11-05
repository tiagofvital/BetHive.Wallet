namespace BetHive.Wallet.Contracts.Tokens
{
    public record GenerateTokenRequest(
        Guid? Id,
        string Name,
        List<string> Permissions,
        List<string> Roles);
}