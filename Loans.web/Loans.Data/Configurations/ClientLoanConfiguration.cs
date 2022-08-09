using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientLoanConfiguration: IEntityTypeConfiguration<ClientLoan>
    {
        public void Configure(EntityTypeBuilder<ClientLoan> builder)
        {
        }
    }
}
