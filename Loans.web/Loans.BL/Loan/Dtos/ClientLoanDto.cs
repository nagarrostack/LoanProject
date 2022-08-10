using Loans.BL.BaseDtos;

namespace Loans.BL.Loan.Dtos
{
    public class ClientLoanDto : BaseClientDto
    {
        public decimal AmountRequest { get; set; }
        public int QtyMonthsPayment { get; set; }
        public byte APR { get; set; }
        public int Rating { get; set; }
        public int LateLoans { get; set; }
        public decimal OutstandingDebt { get; set; }
        public DateTime LoanDate { get; set; }
        public float Risk { get; set; }

    }
}
