namespace BetHive.Wallet.Infrastructure.BatchMovements.BackgroundService
{
    public class BackgroundServiceSettings
    {
        public const string Section = "BackgroundJobSettings";

        public bool Enable { get; init; }
    }
}
