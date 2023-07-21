namespace Neoris.Abstractions.Types.Transaction
{
    public interface ITransaction
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public double TransactionValue { get; set; }
        public double Balance { get; set; }
    }
}
