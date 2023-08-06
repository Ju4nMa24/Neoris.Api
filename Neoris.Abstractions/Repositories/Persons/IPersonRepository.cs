using Neoris.Abstractions.Types.Persons;

namespace Neoris.Abstractions.Repositories.Persons
{
    public interface IPersonRepository
    {
        /// <summary>
        /// This method is used to create person.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool Create(IPerson person);
        /// <summary>
        /// This method is used to modify person.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public dynamic Modify(IPerson person);
        /// <summary>
        /// This method is used to delete person.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber);
        /// <summary>
        /// This method is used to get person.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IPerson? Get(string identificationNumber);
    }
}
