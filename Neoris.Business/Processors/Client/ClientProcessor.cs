using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Clients;
using Neoris.Business.Commands.Client;

namespace Neoris.Business.Processors.Client
{
    public class ClientProcessor : IRequestHandler<ClientCommand, ClientResponse>
    {
        private readonly ClientResponse _response;
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientProcessor> _logger;

        public ClientProcessor(IClientRepository repository, ILogger<ClientProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }

        public async Task<ClientResponse> Handle(ClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Client: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;
                Commons.Types.Tables.Client client = new()
                {
                    Password = request.Password,
                    Status = request.Status
                };
                if (_repository.Create(client, request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Cliente: {request.Identification} registrada con exito...";
                }
                _logger.LogInformation($"Response: {_response.Response?.ToString()}", DateTimeOffset.UtcNow);
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
