using Impodatos.Domain;
using Impodatos.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Impodatos.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            HistoryConfiguration.SetEntityBuilder(builder.Entity<History>());

            base.OnModelCreating(builder);
            
        }
    }
}
