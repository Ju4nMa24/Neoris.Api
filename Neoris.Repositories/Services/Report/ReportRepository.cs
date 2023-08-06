using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Report;
using Neoris.Abstractions.Types.Report;
using Neoris.Repositories.Context;
using Entities = Neoris.Commons.Types.StoredProcedures;
using SP = Neoris.Commons.Constants;

namespace Neoris.Repositories.Services.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly NeorisContext _context;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(NeorisContext context, ILogger<ReportRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<IReport> Get(IRequestReport request)
        {
			try
			{
                _logger.LogInformation($"Request for report: {request.IdentificationNumber}", DateTimeOffset.UtcNow);

                List<Entities.Report> results = _context.Set<Entities.Report>()
                    .FromSqlRaw($"{SP.StoredProcedure.SpReport} @InitialDate='{request.InitialDate}'" +
                    $",@FinalDate = '{request.InitialDate}',@IdentificationNumber = '{request.IdentificationNumber}'").ToList();
                return results;
            }

            catch (Exception ex)
			{
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return new List<Entities.Report>();
            }
        }
    }
}
