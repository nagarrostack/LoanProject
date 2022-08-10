using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientBusinessInfoConfiguration : IEntityTypeConfiguration<ClientBusinessInfo>
    {
        public void Configure(EntityTypeBuilder<ClientBusinessInfo> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                .IsRequired();
        }
    }
}
