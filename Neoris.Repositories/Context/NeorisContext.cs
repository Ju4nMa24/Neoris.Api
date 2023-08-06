using Microsoft.EntityFrameworkCore;
using Neoris.Commons.Types.StoredProcedures;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().HasNoKey();
            //Person
            modelBuilder.Entity<Person>().HasKey(r => r.PersonId);
            modelBuilder.Entity<Person>().Property(r => r.PersonId).HasDefaultValue(Guid.NewGuid());
            modelBuilder.Entity<Person>().Property(r => r.PersonName).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Person>().Property(r => r.Gender).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Person>().Property(r => r.Years).HasMaxLength(3).IsRequired();
            modelBuilder.Entity<Person>().Property(r => r.Identification).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Person>().Property(r => r.Address).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Person>().Property(r => r.Phone).HasMaxLength(20).IsRequired();
            //Client
            modelBuilder.Entity<Client>().HasKey(r => r.ClientId);
            modelBuilder.Entity<Client>().Property(r => r.ClientId).HasDefaultValue(Guid.NewGuid());
            modelBuilder.Entity<Client>().HasOne<Person>().WithMany().HasForeignKey(r => r.PersonId).IsRequired();
            modelBuilder.Entity<Client>().Property(r => r.Password).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Client>().Property(r => r.Status).IsRequired();
            //Account
            modelBuilder.Entity<Account>().HasKey(r => r.AccountId);
            modelBuilder.Entity<Account>().Property(r => r.AccountId).HasDefaultValue(Guid.NewGuid());
            modelBuilder.Entity<Account>().HasOne<Client>().WithMany().HasForeignKey(r => r.ClientId).IsRequired();
            modelBuilder.Entity<Account>().Property(r => r.Status).IsRequired();
            modelBuilder.Entity<Account>().Property(r => r.AccountNumber).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Account>().Property(r => r.AccountType).HasMaxLength(15).IsRequired();
            //Transactions
            modelBuilder.Entity<Transaction>().HasKey(r => r.TransactionId);
            modelBuilder.Entity<Transaction>().Property(r => r.TransactionId).HasDefaultValue(Guid.NewGuid());
            modelBuilder.Entity<Transaction>().HasOne<Account>().WithMany().HasForeignKey(r => r.AccountId).IsRequired();
            modelBuilder.Entity<Transaction>().Property(r => r.TransactionDate).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Transaction>().Property(r => r.TransactionType).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Transaction>().Property(r => r.TransactionValue).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Transaction>().Property(r => r.Balance).HasMaxLength(30).IsRequired();
        }
    }
}
