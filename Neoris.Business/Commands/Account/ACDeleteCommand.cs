using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Account
{
    public class ACDeleteCommand : Base.CommandRequest<AccountDeleteResponse>
    {
        public string Identification { get; set; }
    }
    public class AccountDeleteResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
