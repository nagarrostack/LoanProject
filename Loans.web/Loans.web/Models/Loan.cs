namespace Loans.web.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
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