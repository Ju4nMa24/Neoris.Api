using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Neoris.Repositories.Context
{
    /// <summary>
    /// Context Factory Db.
    /// </summary>
    public class NeorisContextFactory : IDesignTimeDbContextFactory<NeorisContext>
    {
        /// <summary>
        /// Create the db contexty.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NeorisContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../../Neoris/Neoris.Api/appsettings.json")).Build();
            DbContextOptionsBuilder<NeorisContext> optionBuilder = new();
            optionBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new NeorisContext(optionBuilder.Options);
        }
    }
}
