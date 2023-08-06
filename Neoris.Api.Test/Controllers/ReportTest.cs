using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Neoris.Api.Controllers;
using Neoris.Business.Commands.Report;

namespace Neoris.Api.Test.Controllers
{
    [TestFixture]
    public class ReportTest
    {
        [Test]
        [TestCase("2023-08-01", "2023-08-10", "1932404293")]
        [TestCase("", "2023-08-10", "1932404293")]
        [TestCase("2023-08-01", "", "1932404293")]
        [TestCase("2023-08-01", "2023-08-10", "")]
        [TestCase("2023-08-01", "", "")]
        public void GetTest(string initialDate, string finalDate, string identificationNumber)
        {
            #region Arrange
            Mock<IMediator> mediatorMock = new();
            mediatorMock.Setup(m => m.Send(It.IsAny<ReportCommand>(), CancellationToken.None))
                        .ReturnsAsync(new ReportResponse());

            ReportController controller = new(mediatorMock.Object);
            #endregion

            #region Act
            Task<IActionResult> result = Task.Run(() => controller.Get(initialDate, finalDate, identificationNumber));
            #endregion
            #region Assert
            Assert.IsNotNull(result.Result);
            #endregion
        }
    }
}
