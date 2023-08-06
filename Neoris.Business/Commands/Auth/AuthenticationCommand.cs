using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
#nullable disable
namespace Neoris.Business.Commands.Auth
{
    public class AuthenticationCommand : Base.CommandRequest<AuthenticationResponse>
    {
        [Required]
        [JsonPropertyName("TokenId")]
        public Guid TokenId { get; set; }
    }

    public class AuthenticationResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}
