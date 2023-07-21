using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;
        /// <summary>
        /// Exposed method for creating of the jwt.
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthenticationCommand authentication)
        {
            AuthenticationResponse response = await _mediator.Send(authentication);
            return !response.InnerContext.Result.Success ? BadRequest(response.InnerContext.Result) : Ok(response.Token);
        }
    }
}
