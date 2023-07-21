using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neoris.Business.Commands.Person;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Neoris.Api.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PersonCommand request)
        {
            PersonResponse response = await _mediator.Send(request);
            return !response.InnerContext.Result.Success ? BadRequest(response?.InnerContext?.Result) : Ok(response);
        }
    }
}
