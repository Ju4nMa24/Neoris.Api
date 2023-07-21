namespace Neoris.Abstractions.Types.Persons
{
    public interface IPerson
    {
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public string Gender { get; set; }
        public int Years { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
