using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public ClientController(IMediator mediator)
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
        public async Task<IActionResult> Post([FromBody]ClientCommand request)
        {
            ClientResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        /// <summary>
        /// This method is used to client edit.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]EditCommand request)
        {
            EditResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
        [AllowAnonymous]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteCommand request)
        {
            DeleteResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
    }
}
