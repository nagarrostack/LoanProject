using Loans.BL.Client.Interfaces;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Loan.Interfaces;
using Loans.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.WebApp.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService service;
        private readonly IClientsService clientsService;
        private readonly IClientBusinessesService businessService;
        private readonly ICatalogService catalogsService;
        private readonly ITypeCatalogService typeCatalogsService;


        public LoanController(ILogger<LoanController> logger,
            ITypeCatalogService typeCatalogsService,
            ICatalogService catalogsService,
            IClientsService clientsService,
            IClientBusinessesService businessService,
            ILoanService loanService
            )
        {
            _logger = logger;
            this.typeCatalogsService = typeCatalogsService;
            this.catalogsService = catalogsService;
            this.clientsService = clientsService;
            this.businessService = businessService;
            service = loanService;
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

        public async Task<ActionResult> ClientInfo(int? id)
        {
            var catGenderId = await typeCatalogsService.GetTypeCatalogsByNameAsync("Gender");
            var catCountryId = await typeCatalogsService.GetTypeCatalogsByNameAsync("Country");
            var catTitleId = await typeCatalogsService.GetTypeCatalogsByNameAsync("Title");

            var countries = await catalogsService.GetCatalogsByTypeCatalogIdAsync(catCountryId.First().Id);
            var genders = await catalogsService.GetCatalogsByTypeCatalogIdAsync(catGenderId.First().Id);
            var titles = await catalogsService.GetCatalogsByTypeCatalogIdAsync(catTitleId.First().Id);

            var loanData = await service.GetClientLoansById(id.Value);
            var clientData = await clientsService.GetClientByIdAsync(loanData.ClientId);
            var businessData = (await businessService.GetClientBusinessesByClientIdAsync(loanData.ClientId)).FirstOrDefault();

            FullEditLoan editLoan = new FullEditLoan
            {
                CountryCatalog = countries.Select(c => new Catalog { Id = c.Id, Name = c.Name }).ToList(),
                GenderCatalog = genders.Select(c => new Catalog { Id = c.Id, Name = c.Name }).ToList(),
                TitleCatalog = titles.Select(c => new Catalog { Id = c.Id, Name = c.Name }).ToList(),
                ClientInfo = new Client
                {
                    CountryId = clientData.CountryId,
                    GenderId = clientData.GenderId,
                    Id = loanData.ClientId,
                    LastName = clientData.LastName,
                    MidName = clientData.MidName,
                    Name = clientData.Name,
                    TitleId = clientData.TitleId
                },
                BusinessInfo = new ClientBusiness
                {
                },
                LoanInfo = new Loan
                {
                    AmountRequest = loanData.AmountRequest,
                    APR = loanData.APR,
                    ClientId = loanData.ClientId,
                    Id = loanData.Id,
                    LateLoans = loanData.LateLoans,
                    LoanDate = loanData.LoanDate,
                    OutstandingDebt = loanData.OutstandingDebt,
                    QtyMonthsPayment = loanData.QtyMonthsPayment,
                    Rating = loanData.Rating,
                    Risk = loanData.Risk
                }
            };

            if (businessData != null)
            {
                editLoan.BusinessInfo = new ClientBusiness
                {
                    Address = businessData.Address,
                    ClientId = businessData.ClientId,
                    Id = businessData.Id,
                    Name = businessData.Name,
                    PhoneNumber = businessData.PhoneNumber,
                    TaxId = businessData.TaxId
                };
            }

            return View(editLoan);
        }

        [HttpPost]
        public async Task<ActionResult> PostClientInfo([FromForm] FullEditLoan editLoan)
        {
            return View("BusinessData", editLoan);
        }

        public async Task<ActionResult> BusinessData([FromForm] FullEditLoan editLoan)
        {
            editLoan.BusinessInfo.ClientId = editLoan.ClientInfo.Id;

            return View(editLoan);
        }

        [HttpPost]
        public async Task<ActionResult> PostBusinessData([FromForm] FullEditLoan editLoan)
        {
            return View("LoanData", editLoan);
        }

        public async Task<ActionResult> LoanData([FromForm] FullEditLoan editLoan)
        {
            return View(editLoan);
        }

        [HttpPost]
        public async Task<ActionResult> PostLoanData([FromForm] FullEditLoan editLoan)
        {
            return RedirectToAction("Index");
        }
    }
}