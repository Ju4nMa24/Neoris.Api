using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Client
{
    public class ClientCommand : Base.CommandRequest<ClientResponse>
    {
        public string Identification { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
    public class ClientResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
