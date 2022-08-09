using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class ClientLoanConfiguration: IEntityTypeConfiguration<ClientLoan>
    {
        public void Configure(EntityTypeBuilder<ClientLoan> builder)
        {
            builder.Property(x => x.AmountRequest).IsRequired();
            builder.Property(x => x.APR).IsRequired();
            builder.Property(x => x.LateLoans).IsRequired();
            builder.Property(x => x.OutstandingDebt).IsRequired();
            builder.Property(x => x.QtyMonthsPayment).IsRequired();
            builder.Property(x => x.Rating).IsRequired();

        }
    }
}
