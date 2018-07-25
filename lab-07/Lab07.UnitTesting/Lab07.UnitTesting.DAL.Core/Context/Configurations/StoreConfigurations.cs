using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.DAL.Models;

namespace Lab07.UnitTesting.DAL.Core.Context.Configurations
{
    [ExcludeFromCodeCoverage]
    public class StoreConfigurations : EntityTypeConfiguration<Store>
    {
        public StoreConfigurations()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            HasIndex(x => x.Name).IsUnique();
            Property(x => x.Promocode).HasMaxLength(20);
        }
    }
}