using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Account;
using Neoris.Business.Commands.Client;
using Neoris.Business.Commands.Transaction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/movimientos")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// This method is used to Transaction create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TransactionCommand request)
        {
            TransactionResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        /// <summary>
        /// This method is used to Transaction edit.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TEditCommand request)
        {
            TEditResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        /// <summary>
        /// This method is used to delete Transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] TDeleteCommand request)
        {
            TDeleteResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
    }
}
