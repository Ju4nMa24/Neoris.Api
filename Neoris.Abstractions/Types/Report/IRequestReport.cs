namespace Neoris.Abstractions.Types.Report
{
    public interface IRequestReport
    {
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string IdentificationNumber { get; set; }
    }
}
