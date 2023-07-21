using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Business.Commands.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.Business.Processors.Account
{
    public class ACDeleteProcessor : IRequestHandler<ACDeleteCommand, AccountDeleteResponse>
    {
        private readonly AccountDeleteResponse _response;
        private readonly IAccountRepository _repository;
        private readonly ILogger<ACEditProcessor> _logger;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public ACDeleteProcessor(IAccountRepository repository, ILogger<ACEditProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to delete account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AccountDeleteResponse> Handle(ACDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Account: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;
                if (_repository.Delete(request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Se elimino la cuenta exitosamente...";
                }
                _logger.LogInformation($"Response: {_response.Response}", DateTimeOffset.UtcNow);
                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return _response;
            }
        }
    }
}
