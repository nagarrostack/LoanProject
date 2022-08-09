using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class TypeCatalogConfiguration : IEntityTypeConfiguration<TypeCatalog>
    {
        public void Configure(EntityTypeBuilder<TypeCatalog> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(x => x.Catalogs)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.TypeCatalogId);
        }
    }
}
