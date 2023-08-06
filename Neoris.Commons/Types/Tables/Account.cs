using Neoris.Abstractions.Types.Account;
#nullable disable
namespace Neoris.Commons.Types.Tables
{
    public class Account : IAccount
    {
        public Guid AccountId { get; set; }
        public Guid ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public bool Status { get; set; }
    }
}
    