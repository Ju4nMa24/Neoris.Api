using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Business.Commands.Transaction;

namespace Neoris.Business.Processors.Transaction
{
    public class TransactionProcessor : IRequestHandler<TransactionCommand, TransactionResponse>
    {
        private readonly TransactionResponse _response;
        private readonly ITransactionsRepository _repository;
        private readonly ILogger<TransactionProcessor> _logger;

        public TransactionProcessor(ITransactionsRepository repository, ILogger<TransactionProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to create transaction.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> Handle(TransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Transaction: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud";
                if (request is null)
                    return _response;
                Commons.Types.Tables.Transaction transaction = new()
                {
                    Balance = request.Balance,
                    TransactionDate = request.TransactionDate,
                    TransactionType = request.TransactionType,
                    TransactionValue = request.TransactionValue
                };
                if (_repository.Create(transaction, request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Transaccion: {request.Identification} registrada con exito...";
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
