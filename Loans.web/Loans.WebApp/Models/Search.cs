namespace Loans.WebApp.Models
{
    public class Search
    {
        public string Name { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }

        public string BusinessName { get; set; }

        public int CountryId { get; set; }

        public decimal AmountRequest { get; set; }
        public int QtyMonthsPayment { get; set; }
        public byte APR { get; set; }
        public int Rating { get; set; }
        public decimal OutstandingDebt { get; set; }
        public DateTime LoanDate { get; set; }
        public float Risk { get; set; }

        public List<Loan> Results { get; set; } = new List<Loan>();
    }
}
