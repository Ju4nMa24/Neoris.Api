namespace Neoris.Abstractions.Types.Report
{
    public interface IReport
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double InitialBalance { get; set; }
        public string Status { get; set; }
        public double TransactionValue { get; set; }
        public double Balance { get; set; }
    }
}
