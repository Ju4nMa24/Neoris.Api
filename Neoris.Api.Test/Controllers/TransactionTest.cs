using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Neoris.Business.Commands.Transaction;

namespace Neoris.Api.Controllers.Tests
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void PostTest()
        {
            // Arrange
            Mock<IMediator> mediatorMock = new();
            mediatorMock.Setup(m => m.Send(It.IsAny<TransactionCommand>(), CancellationToken.None))
                        .ReturnsAsync(new TransactionResponse());

            TransactionController controller = new(mediatorMock.Object);
            TransactionCommand request = new()
            {
                Identification = "1932404293",
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Ahorros",
                TransactionValue = -100
            };
            // Act
            Task<IActionResult> result = Task.Run(() => controller.Post(request));
            // Assert
            Assert.IsNotNull(result.Result);
        }
    }
}
