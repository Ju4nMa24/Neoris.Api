using Neoris.Abstractions.Types.Transaction;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neoris.Commons.Types.Tables
{
    public class Transaction : ITransaction
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        [Column(TypeName = "nvarchar")]
        public string TransactionType { get; set; }
        public double TransactionValue { get; set; }
        public double Balance { get; set; }
    }
}
