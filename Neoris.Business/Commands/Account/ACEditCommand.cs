using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Account
{
    public class ACEditCommand : Base.CommandRequest<AccountEditResponse>
    {
        public string Identification { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double OpeningBalance { get; set; }
        public bool Status { get; set; }
    }
    public class AccountEditResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
