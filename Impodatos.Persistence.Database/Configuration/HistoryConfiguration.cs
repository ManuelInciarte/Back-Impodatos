using Impodatos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Impodatos.Persistence.Database.Configuration
{
    public class HistoryConfiguration
    {
        public static void SetEntityBuilder(EntityTypeBuilder<History> entityBuilder)
        {
            entityBuilder.HasIndex(x => x.Id);
            entityBuilder.Property(x => x.Programsid).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.JsonSet).IsRequired();
            entityBuilder.Property(x => x.State).IsRequired();
            entityBuilder.Property(x => x.UserLogin).IsRequired().HasMaxLength(300);
            entityBuilder.Property(x => x.Fecha).IsRequired();
        }
    }
}
