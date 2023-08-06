using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Account
{
    public class AccountCommand : Base.CommandRequest<AccountResponse>
    {
        #nullable disable
        public string Identification { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public bool Status { get; set; }
    }
    public class AccountResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
