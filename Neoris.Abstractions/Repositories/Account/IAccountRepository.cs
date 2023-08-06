using Neoris.Abstractions.Types.Account;

namespace Neoris.Abstractions.Repositories.Account
{
    public interface IAccountRepository
    {
        /// <summary>
        /// This method is used to create account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool Create(IAccount account, string identificationNumber);
        /// <summary>
        /// This method is used to modify account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public dynamic? Modify(IAccount account, string identificationNumber);
        /// <summary>
        /// This method is used to delete account.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber);
        /// <summary>
        /// This method is used to get account.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IAccount? Get(string identificationNumber);
    }
}
