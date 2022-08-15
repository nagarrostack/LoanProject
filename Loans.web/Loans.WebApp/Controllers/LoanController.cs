using Loans.BL.Loan.Interfaces;
using Loans.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.WebApp.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService service;

        public LoanController(ILogger<LoanController> logger, ILoanService clientService)
        {
            _logger = logger;
            service = clientService;
        }

        public async Task<ActionResult> Index()
        {
            var clientLoans = await service.GetAllClientLoansAsync();

            var response =
                clientLoans.Select(c => new Loan
                {
                    Id = c.Id,
                    AmountRequest = c.AmountRequest,
                    APR = c.APR,
                    ClientId = c.ClientId,
                    LateLoans = c.LateLoans,
                    LoanDate = c.LoanDate,
                    OutstandingDebt = c.OutstandingDebt,
                    QtyMonthsPayment = c.QtyMonthsPayment,
                    Rating = c.Rating,
                    Risk = c.Risk
                }).ToList();

            return View(response);
        }
    }
}