using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Report;
using Neoris.Abstractions.Types.Report;
using Neoris.Business.Commands.Report;
using Neoris.Commons.Types.StoredProcedures;

namespace Neoris.Business.Processors.Report
{
    /// <summary>
    /// Report Processor.
    /// </summary>
    public class ReportProcessor : IRequestHandler<ReportCommand, ReportResponse>
    {
        public readonly IReportRepository _repository;
        private readonly ILogger<ReportProcessor> _logger;
        private readonly ReportResponse _response;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public ReportProcessor(IReportRepository repository, ILogger<ReportProcessor> logger)
        {
            _repository = repository;
            _logger = logger;
            _response = new();
        }
        /// <summary>
        /// This method is used to  generate a report.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ReportResponse> Handle(ReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Report: {request.IdentificationNumber}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.InnerContext.Result.ResponseCode = "400";
                _response.InnerContext.Result.Response = "No se pudo procesar la solicitud";
                IEnumerable<IReport>? result = _repository.Get(new RequestReport()
                {
                    InitialDate = request.InitialDate,
                    FinalDate = request.FinalDate,
                    IdentificationNumber = request.IdentificationNumber,
                });
                if(result.Any())
                {
                    result = result.OrderByDescending(o => o.Date);
                    List<EntityResponse> responses = new();
                    Parallel.ForEach(result, item =>
                    {
                        responses.Add(new()
                        {
                            AccountNumber = item.AccountNumber,
                            AccountType = item.AccountType,
                            Balance = item.Balance,
                            Client = item.Client,
                            Date = item.Date.ToString("dd-MM-yyyy"),
                            InitialBalance = item.InitialBalance,
                            Status = item.Status,
                            TransactionValue = item.TransactionValue
                        });
                    });
                    if (result is not null)
                    {
                        _response.StatusCode = "200";
                        _response.InnerContext.Result.Success = true;
                        _response.Response = responses;
                    }
                    else
                        _response.InnerContext.Result.Response = "The query does not yield results.";
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
