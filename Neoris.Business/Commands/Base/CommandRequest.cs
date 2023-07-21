using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Base
{
    public abstract class CommandRequest<TRequest> : MediatR.IRequest<TRequest>
    {
        [JsonIgnore]
        public CommandContext InnerContext { get; set; } = new CommandContext();
    }

    public class CommandContext
    { }
}
