using System.Text.Json.Serialization;
#nullable disable
namespace Neoris.Business.Commands.Client
{
    public class DeleteCommand : Base.CommandRequest<DeleteResponse>
    {
        public string Identification { get; set; }
    }
    public class DeleteResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
