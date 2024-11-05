namespace BetHive.Wallet.Application.Common.Security.Permissions
{
    public static class Permission
    {
        public static class Wallet
        {
            public const string Create = "create:wallet";
            public const string Read = "read:wallet";
            public const string Deposit = "deposit:wallet";
            public const string Withdraw = "withdraw:wallet";
        }

        public static class Batch
        {
            public const string Create = "create:batch";
        }
    }
}