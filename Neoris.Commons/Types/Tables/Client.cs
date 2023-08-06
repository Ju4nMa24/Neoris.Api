using Neoris.Abstractions.Types.Clients;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neoris.Commons.Types.Tables
{
    #nullable disable
    public class Client : IClient
    {
        public Guid ClientId { get; set; }
        public Guid PersonId { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
