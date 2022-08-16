using Loans.BL.Loan.Interfaces;
using Loans.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService service;

        public LoanController(ILogger<LoanController> logger, ILoanService loanService)
        {
            _logger = logger;
            service = loanService;
        }

        [HttpGet]
        public async Task<IEnumerable<Loan>> Get()
        {
            var response = await service.GetAllClientLoansAsync();

            return response.Select(c => new Loan
            {
                Id = c.Id,
                IdClient = c.ClientId,
                AmountRequest = c.AmountRequest,
                APR = c.APR,
                LateLoans = c.LateLoans,
                LoanDate = c.LoanDate,
                OutstandingDebt = c.OutstandingDebt,
                QtyMonthsPayment = c.QtyMonthsPayment,
                Rating = c.Rating,
                Risk = c.Risk
            }).ToList();
        }

        [HttpGet]
        public async Task<Loan> GetLoan(int id)
        {
            var response = await service.GetClientLoansById(id);
            return new Loan
            {
                AmountRequest = response.AmountRequest,
                APR = response.APR,
                Id = response.Id,
                IdClient = response.ClientId,
                LateLoans = response.LateLoans,
                LoanDate = response.LoanDate,
                OutstandingDebt = response.OutstandingDebt,
                QtyMonthsPayment = response.QtyMonthsPayment,
                Risk = response.Risk,
                Rating = response.Rating
            };
        }

        [HttpPost]
        public async Task<bool> DeleteLoan(int id)
        {
            var result = await service.DeleteClientLoan(id);
            return result;
        }
    }
}