using Neoris.Abstractions.Types.Clients;

namespace Neoris.Abstractions.Repositories.Clients
{
    public interface IClientRepository
    {
        /// <summary>
        /// This method is used to create Client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool Create(IClient client, string identificationNumber);
        /// <summary>
        /// This method is used to modify Client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public dynamic Modify(IClient client, string identificationNumber);
        /// <summary>
        /// This method is used to delete Client.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber);
        /// <summary>
        /// This method is used to get Client.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IClient? Get(string identificationNumber);
    }
}
