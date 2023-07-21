namespace Neoris.Abstractions.Types.Account
{
    public interface IAccount
    {
        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public bool Status { get; set; }
    }
}
