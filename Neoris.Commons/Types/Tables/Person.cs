using Neoris.Abstractions.Types.Persons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neoris.Commons.Types.Tables
{
    #nullable disable
    public class Person : IPerson
    {
        public Guid PersonId { get; set; }
        [Column(TypeName = "nvarchar")]
        public string PersonName { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Gender { get; set; }
        public int Years { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Identification { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Phone { get; set; }
    }
}
