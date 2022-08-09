using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.MidName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            //builder.HasMany(x => x.Phones)
            //    .WithOne(x => x.Client)
            //    .HasForeignKey(x => x.ClientId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(x => x.Businesses)
            //    .WithOne(x => x.Client)
            //    .HasForeignKey(x => x.ClientId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(x => x.Loans)
            //    .WithOne(x => x.Client)
            //    .HasForeignKey(x => x.ClientId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

