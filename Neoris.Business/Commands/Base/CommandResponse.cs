using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Base
{
    public abstract class CommandResponse
    {
        [JsonIgnore]
        public CommandResponseContext InnerContext { get; set; } = new CommandResponseContext();
    }

    public class CommandResponseContext
    {
        [JsonPropertyName("Resultado")]
        public Result Result { get; set; } = new Result();
    }

    public class Result
    {
        [JsonPropertyName("Success")]
        public bool Success { get; set; }
        [JsonPropertyName("ResponseCode")]
        public string ResponseCode { get; set; }
        [JsonPropertyName("Response")]
        public string Response { get; set; }
    }
}
