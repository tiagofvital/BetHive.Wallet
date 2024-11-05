using BetHive.Wallet.Domain.Common;
using BetHive.Wallet.Domain.Wallets.Events;

using ErrorOr;

namespace BetHive.Wallet.Domain.Wallets
{
    public class Wallet : AuditableEntity
    {
        public Wallet(int tenantId, Guid userId)
            : this(Guid.NewGuid(), tenantId, userId, 0, Array.Empty<byte>())
        {
            this.CreatedAt = DateTime.UtcNow;
            this.ModifiedAt = DateTime.UtcNow;
        }

        public Wallet(Guid id, int tenantId, Guid userId, float balance, byte[] version)
            : base(id)
        {
            TenantId = tenantId;
            UserId = userId;
            Balance = balance;
            this.Token = version;
        }

        public int TenantId { get; }

        public Guid UserId { get; }

        /// <summary>
        /// In a real-world scenario we should have an immutable representation of money, with
        /// its value and currency.
        /// </summary>
        public float Balance { get; private set; }

        public byte[] Token { get; set; }

        public ErrorOr<Wallet> Deposit(float funds)
        {
            this.Balance += funds;

            this.ModifiedAt = DateTime.UtcNow;

            _domainEvents.Add(new DepositAddedEvent(this, funds));

            return this;
        }

        public void SetToken(byte[] token)
        {
            this.Token = token;
        }

        public ErrorOr<Wallet> Withdraw(byte[] token, float funds)
        {
            if (!this.Token.SequenceEqual(token))
            {
                return WalletErrors.InvalidToken;
            }

            var result = this.Balance - funds;

            if (result < 0)
            {
                return WalletErrors.CannotHaveNegativeBalance;
            }

            this.Balance = result;

            this.ModifiedAt = DateTime.UtcNow;

            _domainEvents.Add(new WithdrawAddedEvent(this, funds));

            return this;
        }
    }
}
