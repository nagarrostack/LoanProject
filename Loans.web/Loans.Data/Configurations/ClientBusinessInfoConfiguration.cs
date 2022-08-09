using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientBusinessInfoConfiguration : IEntityTypeConfiguration<ClientBusinessInfo>
    {
        public void Configure(EntityTypeBuilder<ClientBusinessInfo> builder)
        {
        }
    }
}
