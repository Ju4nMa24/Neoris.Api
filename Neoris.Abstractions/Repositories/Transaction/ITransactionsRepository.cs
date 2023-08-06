using Neoris.Abstractions.Types.Transaction;

namespace Neoris.Abstractions.Repositories.Transaction
{
    public interface ITransactionsRepository
    {
        /// <summary>
        /// This method is used to create Transaction.
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public bool Create(ITransaction transaction, string identificationNumber);
        /// <summary>
        /// This method is used to modify Transaction.
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public dynamic? Modify(ITransaction transaction, string identificationNumber);
        /// <summary>
        /// This method is used to delete Transaction.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber);
        /// <summary>
        /// This method is used to get Transaction.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public ITransaction? Get(string identificationNumber);
    }
}
