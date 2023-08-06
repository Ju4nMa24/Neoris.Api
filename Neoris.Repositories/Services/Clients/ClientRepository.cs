using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Clients;
using Neoris.Abstractions.Types.Clients;
using Neoris.Commons.Types.Tables;
using Neoris.Repositories.Context;

namespace Neoris.Repositories.Services.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly NeorisContext _context;
        private readonly ILogger<ClientRepository> _logger;
        public ClientRepository(NeorisContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to create Client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool Create(IClient client, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Client Create: {client.PersonId}", DateTimeOffset.UtcNow);
                Person? data = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if(data == null)
                    return false;
                if (_context.Client.FirstOrDefault(p => p.PersonId == data.PersonId) is null)
                {
                    _context.Client.Add(new()
                    {
                        PersonId = data.PersonId,
                        Password = client.Password,
                        Status = client.Status
                    });
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return false;
            }
        }
        /// <summary>
        /// This method is used to delete Client.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Client Delete: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if (personValidate is not null)
                {
                    Client? client = _context.Client.FirstOrDefault(c => c.PersonId == personValidate.PersonId);
                    if(client is not null) 
                    {
                        _context.Client.Remove(client);
                        _context.SaveChanges();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return false;
            }
        }
        /// <summary>
        /// This method is used to get Client.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IClient? Get(string identificationNumber)
        {
            Person? data = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
            return data is not null ? _context.Client.Find(data.PersonId) : new Client();
        }
        /// <summary>
        /// This method is used to modify Client.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public dynamic Modify(IClient client, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Client Modify: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if (personValidate is not null)
                {
                    Client? clientModify = _context.Client.FirstOrDefault(c => c.PersonId == personValidate.PersonId);
                    if (clientModify is not null) 
                    {
                        clientModify.Status = client.Status;
                        clientModify.Password = client.Password;
                        _context.SaveChanges();
                        return clientModify;
                    }
                    return new Client();
                }
                else
                    return new Client();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return new Client();
            }
        }
    }
}
