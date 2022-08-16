using Loans.BL.Client.Interfaces;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Loan.Interfaces;
using Loans.WebApp.LBL;
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

        private LoanBL loanBL;

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

            loanBL = new LoanBL(typeCatalogsService, catalogsService, clientsService, businessService, loanService);
        }

        public async Task<ActionResult> Index()
        {
            var clientLoans = await service.GetAllClientLoansAsync();

            var response = clientLoans.Select(c => loanBL.ToLoan(c)).ToList();

            return View(response);
        }

        public async Task<ActionResult> ClientInfo(int? id)
        {
            var loanData = await service.GetClientLoansById(id.Value);
            var clientData = await clientsService.GetClientByIdAsync(loanData.ClientId);
            var businessData = (await businessService.GetClientBusinessesByClientIdAsync(loanData.ClientId)).FirstOrDefault();

            FullEditLoan editLoan = new FullEditLoan
            {
                CountryCatalog = loanBL.fillCatalog("Country"),
                GenderCatalog = loanBL.fillCatalog("Gender"),
                TitleCatalog = loanBL.fillCatalog("Title"),
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
            loanBL.IgnoreSectionModelState("BusinessInfo", ModelState);
            loanBL.IgnoreSectionModelState("LoanInfo", ModelState);

            if (!ModelState.IsValid)
            {
                editLoan.CountryCatalog = loanBL.fillCatalog("Country");
                editLoan.GenderCatalog = loanBL.fillCatalog("Gender");
                editLoan.TitleCatalog = loanBL.fillCatalog("Title");

                return View("ClientInfo", editLoan);
            }
            editLoan.BusinessInfo.ClientId = editLoan.ClientInfo.Id;
            return View("BusinessData", editLoan);
        }

        [HttpPost]
        public async Task<ActionResult> PostBusinessData([FromForm] FullEditLoan editLoan)
        {
            loanBL.IgnoreSectionModelState("LoanInfo", ModelState);

            if (!ModelState.IsValid)
            {
                return View("BusinessData", editLoan);
            }
            return View("LoanData", editLoan);
        }

        public async Task<ActionResult> LoanData([FromForm] FullEditLoan editLoan)
        {
            return View(editLoan);
        }

        [HttpPost]
        public async Task<ActionResult> PostLoanData([FromForm] FullEditLoan editLoan)
        {
            loanBL.IgnoreSectionModelState("", ModelState);

            if (!ModelState.IsValid)
            {
                return View("LoanData", editLoan);
            }

            var resultClient = await clientsService.SaveClientAsync(new BL.Client.Dtos.ClientDto
            {
                CountryId = editLoan.ClientInfo.CountryId,
                GenderId = editLoan.ClientInfo.GenderId,
                Id = editLoan.ClientInfo.Id,
                LastName = editLoan.ClientInfo.LastName,
                MidName = editLoan.ClientInfo.MidName,
                Name = editLoan.ClientInfo.Name,
                TitleId = editLoan.ClientInfo.TitleId
            });
            var resultBusiness = await businessService.SaveClientBusinessAsync(new BL.Client.Dtos.ClientBusinessDto
            {
                Address = editLoan.BusinessInfo.Address,
                ClientId = resultClient.Id,
                Id = editLoan.BusinessInfo.Id,
                Name = editLoan.BusinessInfo.Name,
                PhoneNumber = editLoan.BusinessInfo.PhoneNumber,
                TaxId = editLoan.BusinessInfo.TaxId
            });
            var resultLoan = await service.SaveClientLoans(new BL.Loan.Dtos.ClientLoanDto
            {
                AmountRequest = editLoan.LoanInfo.AmountRequest,
                APR = editLoan.LoanInfo.APR,
                ClientId = resultClient.Id,
                Id = editLoan.LoanInfo.Id,
                LateLoans = editLoan.LoanInfo.LateLoans,
                LoanDate = editLoan.LoanInfo.LoanDate,
                OutstandingDebt = editLoan.LoanInfo.OutstandingDebt,
                QtyMonthsPayment = editLoan.LoanInfo.QtyMonthsPayment,
                Rating = editLoan.LoanInfo.Rating,
                Risk = editLoan.LoanInfo.Risk
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> BackwardClientInfo([FromForm] FullEditLoan editLoan)
        {
            if (!ModelState.IsValid)
            {
                return View("BusinessData", editLoan);
            }

            editLoan.CountryCatalog = loanBL.fillCatalog("Country");
            editLoan.GenderCatalog = loanBL.fillCatalog("Gender");
            editLoan.TitleCatalog = loanBL.fillCatalog("Title");

            return View("ClientInfo", editLoan);
        }

        public async Task<ActionResult> NewLoanInfo()
        {
            FullEditLoan newLoanData = new FullEditLoan
            {
                CountryCatalog = loanBL.fillCatalog("Country"),
                GenderCatalog = loanBL.fillCatalog("Gender"),
                TitleCatalog = loanBL.fillCatalog("Title"),
                BusinessInfo = new ClientBusiness(),
                ClientInfo = new Client(),
                LoanInfo = new Loan()
            };

            return View("ClientInfo", newLoanData);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await service.DeleteClientLoan(id.Value);
            }
            return RedirectToAction("Index");
        }


    }
}