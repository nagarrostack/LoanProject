using Loans.Data.BaseEntities;

namespace Loans.Data.Entities
{
    public class ClientLoan : BaseClientEntity
    {
        public decimal AmountRequest { get; set; }
        public int QtyMonthsPayment { get; set; }
        public byte APR { get; set; }
        public int Rating { get; set; }
        public int LateLoans { get; set; }
        public decimal OutstandingDebt { get; set; }
        public DateTime LoanDate { get; set; }

        public float Risk
        {
            get => (float)((LateLoans / QtyMonthsPayment) * (OutstandingDebt / AmountRequest)) * 100f;
        }

        //public IList<LoanPayment> LoanPayments { get; private set; } = new List<LoanPayment>();
    }
}
