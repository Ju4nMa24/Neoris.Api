using Microsoft.EntityFrameworkCore;
using Neoris.Commons.Types.Tables;

namespace Neoris.Repositories.Context
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class NeorisContext : DbContext
    {
        public NeorisContext()
        {
        }
        public NeorisContext(DbContextOptions<NeorisContext> options) : base(options) { }
        /// <summary>
        /// Database Tables.
        /// </summary>
        #region TABLES
        public DbSet<Account> Account { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder model)
        {
            //Person
            model.Entity<Person>().HasKey(r => r.PersonId);
            model.Entity<Person>().Property(r => r.PersonId).HasDefaultValue(Guid.NewGuid());
            model.Entity<Person>().Property(r => r.PersonName).HasMaxLength(50).IsRequired();
            model.Entity<Person>().Property(r => r.Gender).HasMaxLength(20).IsRequired();
            model.Entity<Person>().Property(r => r.Years).HasMaxLength(3).IsRequired();
            model.Entity<Person>().Property(r => r.Identification).HasMaxLength(20).IsRequired();
            model.Entity<Person>().Property(r => r.Address).HasMaxLength(100).IsRequired();
            model.Entity<Person>().Property(r => r.Phone).HasMaxLength(20).IsRequired();
            //Client
            model.Entity<Client>().HasKey(r => r.ClientId);
            model.Entity<Client>().Property(r => r.ClientId).HasDefaultValue(Guid.NewGuid());
            model.Entity<Client>().HasOne<Person>().WithMany().HasForeignKey(r => r.PersonId).IsRequired();
            model.Entity<Client>().Property(r => r.Password).HasMaxLength(100).IsRequired();
            model.Entity<Client>().Property(r => r.Status).IsRequired();
            //Account
            model.Entity<Account>().HasKey(r => r.AccountId);
            model.Entity<Account>().Property(r => r.AccountId).HasDefaultValue(Guid.NewGuid());
            model.Entity<Account>().HasOne<Client>().WithMany().HasForeignKey(r => r.ClientId).IsRequired();
            model.Entity<Account>().Property(r => r.Status).IsRequired();
            model.Entity<Account>().Property(r => r.AccountNumber).HasMaxLength(50).IsRequired();
            model.Entity<Account>().Property(r => r.AccountType).HasMaxLength(15).IsRequired();
            //Transactions
            model.Entity<Transaction>().HasKey(r => r.TransactionId);
            model.Entity<Transaction>().Property(r => r.TransactionId).HasDefaultValue(Guid.NewGuid());
            model.Entity<Transaction>().HasOne<Account>().WithMany().HasForeignKey(r => r.AccountId).IsRequired();
            model.Entity<Transaction>().Property(r => r.TransactionDate).HasMaxLength(20).IsRequired();
            model.Entity<Transaction>().Property(r => r.TransactionType).HasMaxLength(20).IsRequired();
            model.Entity<Transaction>().Property(r => r.TransactionValue).HasMaxLength(30).IsRequired();
            model.Entity<Transaction>().Property(r => r.Balance).HasMaxLength(30).IsRequired();
        }
    }
}
