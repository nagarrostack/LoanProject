using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            //builder.HasMany(x => x.ClientTitles)
            //    .WithOne(x => x.TitleCatalog)
            //    .HasForeignKey(x => x.TitleId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(x => x.ClientGenders)
            //    .WithOne(x => x.GenderCatalog)
            //    .HasForeignKey(x => x.GenderId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(x => x.CountryCodes)
            //    .WithOne(x => x.CountryCodeCatalog)
            //    .HasForeignKey(x => x.CountryCodeId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(x => x.ClientPhoneTypes)
            //    .WithOne(x => x.TypePhoneCatalog)
            //    .HasForeignKey(x => x.TypePhoneId)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
