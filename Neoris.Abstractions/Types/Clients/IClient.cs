namespace Neoris.Abstractions.Types.Clients
{
    public interface IClient
    {
        public Guid ClientId { get; set; }
        public Guid PersonId { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
