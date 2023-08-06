using System.Text.Json.Serialization;
#nullable disable
namespace Neoris.Business.Commands.Client
{
    public class EditCommand : Base.CommandRequest<EditResponse>
    {
        public string Identification { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
    public class EditResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
