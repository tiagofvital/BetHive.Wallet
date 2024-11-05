using AutoFixture;

using BetHive.Wallet.Domain.Wallets;

namespace BetHive.Wallet.Domain.UnitTests.Wallets
{
    public class WalletTests
    {
        [Fact]
        public void New_WhenNoAmountIsPassed_ShouldCreateWalletWithZeroBalance()
        {
            // arrange
            var expected = 0;

            // act
            var sut = new Domain.Wallets.Wallet(tenantId: 1, userId: Guid.NewGuid());

            // assert
            sut.Balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(10, 25)]
        public void Deposit_AnAmount_AddsAmount(float balance, float amount)
        {
            // arrange
            var sut = new Domain.Wallets.Wallet(id: Guid.NewGuid(), tenantId: 1, userId: Guid.NewGuid(), balance: balance, Array.Empty<byte>());
            var expected = sut.Balance + amount;

            // act
            var result = sut.Deposit(amount);

            // assert
            result.IsError.Should().BeFalse();
            sut.Balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(500, 10)]
        [InlineData(10, 10)]
        public void Withdraw_WhenPositiveFinalAmount_RemovesFunds(float balance, float funds)
        {
            // arrange
            var sut = new Domain.Wallets.Wallet(id: Guid.NewGuid(), tenantId: 1, userId: Guid.NewGuid(), balance: balance, Array.Empty<byte>());
            var expected = sut.Balance - funds;

            // act
            var result = sut.Withdraw(sut.Token, funds);

            // assert
            result.IsError.Should().BeFalse();
            sut.Balance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(10, 11)]
        public void Withdraw_WhenNegativeFinalAmount_ReturnsCannotRemoveFundsError(float balance, float funds)
        {
            // arrange
            var sut = new Domain.Wallets.Wallet(id: Guid.NewGuid(), tenantId: 1, userId: Guid.NewGuid(), balance: balance, Array.Empty<byte>());
            var expected = new List<Error> { WalletErrors.CannotHaveNegativeBalance };

            // act
            var result = sut.Withdraw(sut.Token, funds);

            // assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expected);
            sut.Balance.Should().Be(balance);
        }

        [Fact]
        public void Withdraw_WhenInvalidToken_ReturnsInvalidTokenError()
        {
            // arrange
            var fixture = new Fixture();

            var token = fixture.Create<byte[]>();
            var amount = fixture.Create<float>();

            var sut = fixture.Create<Domain.Wallets.Wallet>();

            var expected = new List<Error> { WalletErrors.InvalidToken };

            // act
            var result = sut.Withdraw(token, amount);

            // assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expected);
        }
    }
}
