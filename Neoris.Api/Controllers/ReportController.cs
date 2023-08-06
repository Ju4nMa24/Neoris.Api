using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Report;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/reportes")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// This method is used to Transaction create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string initialDate, string finalDate, string? identificationNumber)
        {
            ReportCommand request = new()
            {
                FinalDate = finalDate,
                IdentificationNumber = string.IsNullOrEmpty(identificationNumber) ? string.Empty : identificationNumber,
                InitialDate = initialDate
            };
            ReportResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
    }
}
