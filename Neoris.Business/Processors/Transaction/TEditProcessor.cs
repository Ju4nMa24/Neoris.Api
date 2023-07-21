using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Business.Commands.Transaction;

namespace Neoris.Business.Processors.Transaction
{
    public class TEditProcessor : IRequestHandler<TEditCommand, TEditResponse>
    {
        private readonly TEditResponse _response;
        private readonly ITransactionsRepository _repository;
        private readonly ILogger<TEditProcessor> _logger;

        public TEditProcessor(ITransactionsRepository repository, ILogger<TEditProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to modify transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TEditResponse> Handle(TEditCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Client: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;

                dynamic edit = _repository.Modify(new Commons.Types.Tables.Transaction()
                {
                    TransactionValue = request.TransactionValue,
                    TransactionType = request.TransactionType,
                    TransactionDate = request.TransactionDate,
                    Balance = request.Balance
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
