using Neoris.Abstractions.Types.Report;

namespace Neoris.Abstractions.Repositories.Report
{
    public interface IReportRepository
    {
        /// <summary>
        /// This method is used to generate a report.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<IReport> Get(IRequestReport request);
    }
}
