using Loans.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Data.Configurations
{
    public class LoanPaymentConfiguration: IEntityTypeConfiguration<LoanPayment>
    {
        public void Configure(EntityTypeBuilder<LoanPayment> builder)
        {
            builder.Property(x => x.Amount)
                .IsRequired();
            
            builder.Property(x => x.LoanId)
                .IsRequired();
            
            builder.Property(x => x.PaymentDate)
                .IsRequired();

        }
    }
}
