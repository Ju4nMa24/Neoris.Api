using System.Text.Json.Serialization;
#nullable disable
namespace Neoris.Business.Commands.Report
{
    public class ReportCommand : Base.CommandRequest<ReportResponse>
    {
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string IdentificationNumber { get; set; }
    }
    public class ReportResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
    public class EntityResponse
    {
        [JsonPropertyName("Fecha")]
        public string Date { get; set; }
        [JsonPropertyName("Cliente")]
        public string Client { get; set; }
        [JsonPropertyName("NumeroCuenta")]
        public string AccountNumber { get; set; }
        [JsonPropertyName("Tipo")]
        public string AccountType { get; set; }
        [JsonPropertyName("Saldo Inicial")]
        public double InitialBalance { get; set; }
        [JsonPropertyName("Estado")]
        public string Status { get; set; }
        [JsonPropertyName("Movimiento")]
        public double TransactionValue { get; set; }
        [JsonPropertyName("SaldoDisponible")]
        public double Balance { get; set; }
    }
}
