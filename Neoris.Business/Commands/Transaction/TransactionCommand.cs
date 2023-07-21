using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Transaction
{
    public class TransactionCommand : Base.CommandRequest<TransactionResponse>
    {
        public string Identification { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public double TransactionValue { get; set; }
        public double Balance { get; set; }
    }
    public class TransactionResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
