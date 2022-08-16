using Loans.BL.Client.Interfaces;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Loan.Dtos;
using Loans.BL.Loan.Interfaces;
using Loans.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.WebApp.LBL
{
    public class LoanBL
    {
        private readonly ILoanService service;
        private readonly IClientsService clientsService;
        private readonly IClientBusinessesService businessService;
        private readonly ICatalogService catalogsService;
        private readonly ITypeCatalogService typeCatalogsService;

        public LoanBL(
            ITypeCatalogService typeCatalogsService,
            ICatalogService catalogsService,
            IClientsService clientsService,
            IClientBusinessesService businessService,
            ILoanService loanService
            )
        {
            this.typeCatalogsService = typeCatalogsService;
            this.catalogsService = catalogsService;
            this.clientsService = clientsService;
            this.businessService = businessService;
            service = loanService;
        }

        public Loan ToLoan(ClientLoanDto)
        {
            return new Loan
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
            };
        }

        public List<Catalog> fillCatalog(string catalogName)
        {
            var catId = typeCatalogsService.GetTypeCatalogsByNameAsync(catalogName);
            var items = catalogsService.GetCatalogsByTypeCatalogIdAsync(catId.Result.First().Id);
            return items.Result.Select(c => new Catalog { Id = c.Id, Name = c.Name }).ToList();
        }
    }
}
