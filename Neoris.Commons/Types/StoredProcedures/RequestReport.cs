using Neoris.Abstractions.Types.Report;
#nullable disable
namespace Neoris.Commons.Types.StoredProcedures
{
    public class RequestReport : IRequestReport
    {
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string IdentificationNumber { get; set; }
    }
}
