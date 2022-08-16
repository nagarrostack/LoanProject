using System.ComponentModel.DataAnnotations;

namespace Loans.WebApp.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        [Required,
            DataType(DataType.Currency),
            Range(0, 10000000),
            Display(Name = "Amount requested")]
        public decimal AmountRequest { get; set; }

        [Required,
            Range(0, 48),
            Display(Name = "Quantity of months to pay")]
        public int QtyMonthsPayment { get; set; }

        [Required,
            Range(4,12)]
        public byte APR { get; set; }

        [Required,
            Range(600, 750)]
        public int Rating { get; set; }
        public int LateLoans { get; set; }

        [DataType(DataType.Currency),
            Range(25000, 1000000),
            Display(Name = "Outstanding debt")]
        public decimal OutstandingDebt { get; set; }

        [Required,
            DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }

        public float Risk { get; set; }
    }
}