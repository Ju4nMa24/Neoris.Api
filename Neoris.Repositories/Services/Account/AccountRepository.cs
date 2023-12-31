﻿using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Account;
using Neoris.Abstractions.Types.Account;
using Neoris.Commons.Types.Tables;
using Neoris.Repositories.Context;

namespace Neoris.Repositories.Services.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly NeorisContext _context;
        private readonly ILogger<AccountRepository> _logger;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public AccountRepository(NeorisContext context, ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to created account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Create(IAccount account, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Account Create: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? data = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if (data is not null)
                {
                    Client? client = _context.Client.FirstOrDefault(p => p.PersonId == data.PersonId);
                    if (data is null || client is null)
                        return false;
                    _context.Account.Add(new()
                    {
                        ClientId = client.ClientId,
                        AccountNumber = account.AccountNumber,
                        AccountType = account.AccountType,
                        OpeningBalance = account.OpeningBalance,
                        Status = client.Status
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
        /// This method is used to delete account.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public bool Delete(string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Account Delete: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                if (personValidate is not null)
                {
                    Guid? client = _context.Client.FirstOrDefault(p => p.PersonId == personValidate.PersonId)?.ClientId;
                    if (client.ToString() != string.Empty)
                    {
                        Commons.Types.Tables.Account? account = _context.Account.FirstOrDefault(c => c.ClientId == client);
                        if (account is not null)
                        {
                            _context.Account.Remove(account);
                            _context.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                    else
                        return false;
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
        /// This method is used to get account.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public IAccount? Get(string identificationNumber)
        {
            Person? data = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
            Client? client = new();
            if (data is not null)
                client = _context.Client.FirstOrDefault(p => p.PersonId == data.PersonId);
            return client is not null ? _context.Account.FirstOrDefault(p => p.ClientId == client.ClientId) : new Commons.Types.Tables.Account();
        }
        /// <summary>
        /// This method is used to modify account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public dynamic? Modify(IAccount account, string identificationNumber)
        {
            try
            {
                _logger.LogInformation($"Account Modify: {identificationNumber}", DateTimeOffset.UtcNow);
                Person? personValidate = _context.Person.FirstOrDefault(p => p.Identification == identificationNumber);
                Client? client = new();
                if (personValidate is not null)
                    client = _context.Client.FirstOrDefault(p => p.PersonId == personValidate.PersonId);
                if (client is not null)
                {
                    Commons.Types.Tables.Account? modify = _context.Account.FirstOrDefault(c => c.ClientId == client.ClientId);
                    if (modify is not null)
                    {
                        modify.Status = account.Status;
                        modify.AccountNumber = account.AccountNumber;
                        modify.OpeningBalance = account.OpeningBalance;
                        modify.AccountType = account.AccountType;
                        _context.SaveChanges();
                    }
                    return modify;
                }
                else
                    return new Commons.Types.Tables.Account();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return new Commons.Types.Tables.Account();
            }
        }
    }
}
