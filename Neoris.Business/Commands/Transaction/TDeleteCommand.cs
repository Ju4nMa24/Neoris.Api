using System.Text.Json.Serialization;
#nullable disable
namespace Neoris.Business.Commands.Transaction
{
    public class TDeleteCommand : Base.CommandRequest<TDeleteResponse>
    {
        public string Identification { get; set; }
    }
    public class TDeleteResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
