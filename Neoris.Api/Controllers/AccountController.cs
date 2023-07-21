using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// This method is used to client create.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AccountCommand request)
        {
            AccountResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        /// <summary>
        /// This method is used to client edit.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ACEditCommand request)
        {
            AccountEditResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        /// <summary>
        /// This method is used to delete account.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]ACDeleteCommand request)
        {
            AccountDeleteResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
    }
}
