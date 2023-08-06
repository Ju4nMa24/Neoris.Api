using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Transaction;
using Neoris.Abstractions.Types.Transaction;
using Neoris.Commons.Types.Tables;
using Neoris.Repositories.Context;
using Neoris.Repositories.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neoris.Repositories.Services.Transaction
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly NeorisContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public TransactionsRepository(NeorisContext context, ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to create Transaction.
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public bool Create(ITransaction transaction, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Transaction Create: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? data = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                Client? client = new();
                if (data is not null)
                    client = _context.Client.FirstOrDefault(p => p.PersonId == data.PersonId);
                Commons.Types.Tables.Account? account = new();
                if (client is not null)
                    account = _context.Account.FirstOrDefault(p => p.ClientId == client.ClientId);
                if (data is null || client is null)
                    return false;
                if(account is not null)
                {
                    _context.Transaction.Add(new()
                    {
                        AccountId = account.AccountId,
                        Balance = transaction.Balance,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.TransactionType,
                        TransactionValue = transaction.TransactionValue
                    });
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return false;
            }
        }
        /// <summary>
        /// This method is used to delete Transaction.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Transaction Delete: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                Client? client = new();
                if (personValidate is not null)
                    client = _context.Client.FirstOrDefault(p => p.PersonId == personValidate.PersonId);
                Commons.Types.Tables.Account? account = new();
                if (client is not null)
                    account = _context.Account?.FirstOrDefault(p => p.ClientId == client.ClientId);
                if (account is not null)
                {
                    Commons.Types.Tables.Transaction? trx = _context.Transaction.FirstOrDefault(c => c.AccountId == account.AccountId);
                    if (trx is not null)
                    {
                        _context.Transaction.Remove(trx);
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
        /// This method is used to get Transaction.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public ITransaction? Get(string identificationNumber)
        {
            Person? data = _context.Person?.FirstOrDefault(p => p.Identification == identificationNumber);
            if (data is not null)
            {
                Client? client = _context.Client?.FirstOrDefault(p => p.PersonId == data.PersonId);
                if(client is not null)
                {
                    Commons.Types.Tables.Account? account = _context.Account?.FirstOrDefault(p => p.ClientId == client.ClientId);
                    if(account is not null && _context.Transaction.Count() > 0)
                        return _context.Transaction?.Where(p => p.AccountId == account.AccountId).AsEnumerable().LastOrDefault();
                }
            }
            return null;
        }
        /// <summary>
        /// This method is used to modify Transaction.
        /// </summary>
        /// <param name="Transaction"></param>
        /// <returns></returns>
        public dynamic? Modify(ITransaction transaction, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Account Modify: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                Client? client = new();
                if (personValidate is not null)
                    client = _context.Client.FirstOrDefault(p => p.PersonId == personValidate.PersonId);
                Commons.Types.Tables.Account? account = new();
                if (client is not null)
                    account = _context.Account.FirstOrDefault(p => p.ClientId == client.ClientId);
                if (account is not null)
                {
                    Commons.Types.Tables.Transaction? modify = _context.Transaction.FirstOrDefault(c => c.AccountId == account.AccountId);
                    if (modify is not null)
                    {
                        modify.TransactionValue = transaction.TransactionValue;
                        modify.TransactionDate = transaction.TransactionDate;
                        modify.Balance = transaction.Balance;
                        modify.TransactionType = transaction.TransactionType;
                        _context.SaveChanges();
                    }
                    return modify;
                }
                else
                    return new Commons.Types.Tables.Transaction();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return new Commons.Types.Tables.Transaction();
            }
        }
    }
}
