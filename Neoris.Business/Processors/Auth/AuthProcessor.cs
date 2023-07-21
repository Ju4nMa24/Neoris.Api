using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Auth;
using Neoris.Business.Commands.Auth;
using Newtonsoft.Json;
using System.Net;

namespace Neoris.Business.Processors.Auth
{
    public class AuthProcessor : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        private readonly ILogger<AuthProcessor> _logger;
        private readonly AuthenticationResponse _authenticationResponse;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthProcessor(IAuthenticationRepository authenticationRepository, ILogger<AuthProcessor> logger)
        {
            _logger = logger;
            _authenticationResponse = new();
            _authenticationRepository = authenticationRepository;
        }
        public async Task<AuthenticationResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            _authenticationResponse.InnerContext.Result.Success = false;
            try
            {
                //The jwt is generated.
                string token = await Task.Run(() => _authenticationRepository.Generate(request.TokenId.ToString()).Result);
                if (!string.IsNullOrEmpty(token))
                {
                    _authenticationResponse.InnerContext.Result.Success = true;
                    _authenticationResponse.Token = token;
                    _authenticationResponse.StatusCode = HttpStatusCode.OK.ToString();
                    string detail = JsonConvert.SerializeObject(_authenticationResponse.InnerContext);
                    _logger.LogInformation(detail);
                    return _authenticationResponse;
                }
            }
            catch (Exception ex)
            {
                _authenticationResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _authenticationResponse;
            }
            return _authenticationResponse;
        }

    }
}
