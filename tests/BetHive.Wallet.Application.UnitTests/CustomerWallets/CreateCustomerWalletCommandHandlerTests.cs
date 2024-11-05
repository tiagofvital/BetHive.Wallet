using AutoFixture;

using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Application.Wallets.Commands.CreateWallet;
using BetHive.Wallet.Domain.Wallets;

using Moq;

namespace BetHive.Wallet.Application.UnitTests.CustomerWallets
{
    public class CreateWalletCommandHandlerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IWalletsRepository> walletRepositoryMock;
        private readonly CreateWalletCommandHandler sut;

        public CreateWalletCommandHandlerTests()
        {
            fixture = new Fixture();
            walletRepositoryMock = new Mock<IWalletsRepository>();
            sut = new CreateWalletCommandHandler(walletRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCommandIsValidAndUserDoesntHaveACustomerWallet_CreatesCustomerWallet()
        {
            // arrange
            var command = fixture.Create<CreateWalletCommand>();

            var expected = new WalletCreatedResult(Guid.NewGuid(), command.TenantId, command.UserId);
            var ct = CancellationToken.None;

            walletRepositoryMock
                .Setup(i => i.UnitOfWork.SaveChangesAsync(ct))
                .ReturnsAsync(1);

            // act
            var result = await sut.Handle(command, ct);

            // assert
            walletRepositoryMock
                .Verify(i => i.AddAsync(It.Is<Domain.Wallets.Wallet>(w => w.UserId == command.UserId), ct), Times.Once);

            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(expected, config: cfg => cfg.Excluding(r => r.Id));
            result.Value.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_WhenUserAlreadyHasACustomerWallet_ReturnsCannotCreateWalletWhenUserAlreadyHasOne()
        {
            // arrange
            var command = fixture.Create<CreateWalletCommand>();
            var wallet = new Domain.Wallets.Wallet(command.TenantId, command.UserId);

            var wallets = new[] { wallet };

            var ct = CancellationToken.None;

            var expected = new List<Error> { WalletErrors.CannotCreateWalletWhenUserAlreadyHasOne };

            walletRepositoryMock
                .Setup(i => i.GetAsync(command.TenantId, command.UserId, ct))
                .ReturnsAsync(wallets);

            // act
            var result = await sut.Handle(command, CancellationToken.None);

            // assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(expected);
        }
    }
}
