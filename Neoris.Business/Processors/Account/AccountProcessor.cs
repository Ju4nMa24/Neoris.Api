using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Business.Commands.Account;

namespace Neoris.Business.Processors.Account
{
    public class AccountProcessor : IRequestHandler<AccountCommand, AccountResponse>
    {
        private readonly AccountResponse _response;
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountProcessor> _logger;
        public AccountProcessor(IAccountRepository repository, ILogger<AccountProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to create account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AccountResponse> Handle(AccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Account: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;
                Commons.Types.Tables.Account account = new()
                {
                    AccountNumber = request.AccountNumber,
                    AccountType = request.AccountType,
                    OpeningBalance = request.OpeningBalance,
                    Status = request.Status,
                };
                if (_repository.Create(account, request.Identification))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Cuenta del cliente identificado con No.: {request.Identification} registrada con exito...";
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
