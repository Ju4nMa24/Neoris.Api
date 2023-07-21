using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Clients;
using Neoris.Business.Commands.Client;
using Newtonsoft.Json;

namespace Neoris.Business.Processors.Client
{
    public class EditProcessor : IRequestHandler<EditCommand, EditResponse>
    {
        private readonly EditResponse _response;
        private readonly IClientRepository _repository;
        private readonly ILogger<EditProcessor> _logger;

        public EditProcessor(IClientRepository repository, ILogger<EditProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to modify client.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EditResponse> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Client: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;

                dynamic edit = _repository.Modify(new Commons.Types.Tables.Client()
                {
                    Password = request.Password,
                    Status = request.Status
                }, request.Identification);
                if (edit is not null)
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = edit;
                }
                _logger.LogInformation($"Registro actualizado con exito...", DateTimeOffset.UtcNow);
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
