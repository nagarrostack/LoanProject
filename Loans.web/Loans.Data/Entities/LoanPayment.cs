using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class LoanPayment : BaseEntity
    {
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public ClientLoan ClientLoan { get; set; }
    }
}
