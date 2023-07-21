using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Clients;
using Neoris.Business.Commands.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.Business.Processors.Client
{
    public class DeleteProcessor : IRequestHandler<DeleteCommand, DeleteResponse>
    {
        private readonly DeleteResponse _response;
        private readonly IClientRepository _repository;
        private readonly ILogger<DeleteProcessor> _logger;

        public DeleteProcessor(IClientRepository repository, ILogger<DeleteProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to delete client.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Client: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;

                if (_repository.Delete(request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Registro eliminado con extio...";
                }
                _logger.LogInformation($"{_response.Response}", DateTimeOffset.UtcNow);
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
