using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Persons;
using Neoris.Abstractions.Types.Persons;
using Neoris.Commons.Types.Tables;
using Neoris.Repositories.Context;

namespace Neoris.Repositories.Services.Persons
{
    public class PersonRepository : IPersonRepository
    {
        private readonly NeorisContext _context;
        private readonly ILogger<PersonRepository> _logger;
        public PersonRepository(NeorisContext context, ILogger<PersonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to create person.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool Create(IPerson person)
        {
            try
            {
                _logger.LogInformation($"Person Create: {person.Identification}", DateTimeOffset.UtcNow);
                if (_context.Person.FirstOrDefault(p => p.Identification == person.Identification) is null)
                {
                    _context.Person.Add(new()
                    {
                        Identification = person.Identification,
                        Address = person.Address,
                        Gender = person.Gender,
                        PersonId = person.PersonId,
                        PersonName = person.PersonName,
                        Phone = person.Phone,
                        Years = person.Years
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
        /// This method is used to delete person.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber)
        {
            try
            {
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if (personValidate is not null)
                {
                    _context.Person.Remove(personValidate);
                    _context.SaveChanges();
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
        /// This method is used to get person.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IPerson? Get(string identificationNumber) => _context.Person?.FirstOrDefault(p => p.Identification == identificationNumber);
        /// <summary>
        /// This method is used to modify person.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public dynamic Modify(IPerson person)
        {
            try
            {
                _logger.LogInformation($"Person Modify: {person.Identification}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == person.Identification);
                if (personValidate is not null)
                {
                    personValidate.Address = person.Address;
                    personValidate.Identification = person.Identification;
                    personValidate.Years = person.Years;
                    personValidate.Phone = person.Phone;
                    personValidate.PersonName = person.PersonName;
                    _context.SaveChanges();
                    return personValidate;
                }
                else
                    return new Person();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return new Person();
            }
        }
    }
}
