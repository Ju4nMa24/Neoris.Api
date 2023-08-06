using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Business.Commands.Account;

namespace Neoris.Business.Processors.Account
{
    public class ACEditProcessor : IRequestHandler<ACEditCommand, AccountEditResponse>
    {
        private readonly AccountEditResponse _response;
        private readonly IAccountRepository _repository;
        private readonly ILogger<ACEditProcessor> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public ACEditProcessor(IAccountRepository repository, ILogger<ACEditProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to edit account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AccountEditResponse> Handle(ACEditCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Account: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.StatusCode = "400";
                _response.Response = "No se pudo procesar la solicitud...";
                if (request is null)
                    return _response;
                dynamic? edit = _repository.Modify(new Commons.Types.Tables.Account()
                {
                    AccountNumber = request.AccountNumber,
                    AccountType = request.AccountType,
                    OpeningBalance = request.OpeningBalance,
                    Status = request.Status,
                }, request.Identification);
                if (edit is not null)
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = edit;
                }
                await Task.Run(() => _logger.LogInformation($"Response: {request.Identification}", DateTimeOffset.UtcNow));
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
