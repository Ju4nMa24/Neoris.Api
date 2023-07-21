using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Business.Commands.Transaction;

namespace Neoris.Business.Processors.Transaction
{
    public class TDeleteProcessor : IRequestHandler<TDeleteCommand, TDeleteResponse>
    {
        private readonly TDeleteResponse _response;
        private readonly ITransactionsRepository _repository;
        private readonly ILogger<TDeleteProcessor> _logger;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public TDeleteProcessor(ITransactionsRepository repository, ILogger<TDeleteProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to delete trx.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TDeleteResponse> Handle(TDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Transaction: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;
                if (_repository.Delete(request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Se elimino la Transaccion exitosamente...";
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
