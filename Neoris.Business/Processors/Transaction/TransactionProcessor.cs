using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Business.Commands.Transaction;

namespace Neoris.Business.Processors.Transaction
{
    public class TransactionProcessor : IRequestHandler<TransactionCommand, TransactionResponse>
    {
        private readonly TransactionResponse _response;
        private readonly ITransactionsRepository _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<TransactionProcessor> _logger;

        public TransactionProcessor(ITransactionsRepository repository, ILogger<TransactionProcessor> logger, IAccountRepository accountRepository)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
            _accountRepository = accountRepository;
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
                _response.InnerContext.Result.ResponseCode = "400";
                _response.InnerContext.Result.Response = "No se pudo procesar la solicitud";
                if (request is null)
                    return _response;
                Commons.Types.Tables.Transaction transaction = new()
                {
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = request.TransactionType,
                    TransactionValue = request.TransactionValue
                };
                double? initialBalance = _accountRepository.Get(request.Identification)?.OpeningBalance;
                initialBalance = initialBalance != null ? initialBalance : 0;
                Abstractions.Types.Transaction.ITransaction? lastTransaction = _repository.Get(request.Identification);
                transaction.Balance = (lastTransaction != null)  ? lastTransaction.Balance + request.TransactionValue : (double)initialBalance + request.TransactionValue;
                if((lastTransaction != null && lastTransaction.Balance <= 0) || (lastTransaction == null && transaction.Balance < 0))
                    _response.InnerContext.Result.Response = "Saldo no disponible.";

                else if (_repository.Create(transaction, request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Transaccion: {request.Identification} registrada con exito...";
                }
                await Task.Run(() => _logger.LogInformation($"Response: {_response.Response?.ToString()}", DateTimeOffset.UtcNow));
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
