using System.Text.Json.Serialization;

namespace Neoris.Business.Commands.Person
{
    public class PersonCommand : Base.CommandRequest<PersonResponse>
    {
        [JsonPropertyName("Name")]
        public string PersonName { get; set; }
        public string Gender { get; set; }
        public int Years { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class PersonResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        public dynamic Response { get; set; }
    }
}
