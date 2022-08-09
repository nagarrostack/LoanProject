using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientPhoneConfiguration : IEntityTypeConfiguration<ClientPhone>
    {
        public void Configure(EntityTypeBuilder<ClientPhone> builder)
        {
            builder.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

