using Loans.BL.Client.Interfaces;
using Loans.BL.Configuration.Interfaces;
using Loans.BL.Loan.Dtos;
using Loans.BL.Loan.Interfaces;
using Loans.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public Loan ToLoan(ClientLoanDto c)
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

        public void IgnoreSectionModelState(string section, ModelStateDictionary modelState)
        {
            modelState.Remove("ClientInfo.Title");
            modelState.Remove("ClientInfo.Country");
            modelState.Remove("ClientInfo.Gender");
            modelState.Remove("BusinessInfo.Client");
            modelState.Remove("TitleCatalog");
            modelState.Remove("GenderCatalog");
            modelState.Remove("CountryCatalog");

            switch (section)
            {
                case "ClientInfo":
                    modelState.Remove("ClientInfo.Id");
                    modelState.Remove("ClientInfo.Name");
                    modelState.Remove("ClientInfo.Title");
                    modelState.Remove("ClientInfo.Gender");
                    modelState.Remove("ClientInfo.Country");
                    modelState.Remove("ClientInfo.TitleId");
                    modelState.Remove("ClientInfo.GenderId");
                    modelState.Remove("ClientInfo.CountryId");
                    modelState.Remove("ClientInfo.MidName");
                    modelState.Remove("ClientInfo.LastName");
                    break;
                case "BusinessInfo":
                    modelState.Remove("BusinessInfo.Id");
                    modelState.Remove("BusinessInfo.Name");
                    modelState.Remove("BusinessInfo.TaxId");
                    modelState.Remove("BusinessInfo.Client");
                    modelState.Remove("BusinessInfo.Address");
                    modelState.Remove("BusinessInfo.ClientId");
                    modelState.Remove("BusinessInfo.PhoneNumber");
                    break;
                case "LoanInfo":
                    modelState.Remove("LoanInfo.Id");
                    modelState.Remove("LoanInfo.APR");
                    modelState.Remove("LoanInfo.Risk");
                    modelState.Remove("LoanInfo.Rating");
                    modelState.Remove("LoanInfo.ClientId");
                    modelState.Remove("LoanInfo.LoanDate");
                    modelState.Remove("LoanInfo.LateLoans");
                    modelState.Remove("LoanInfo.AmountRequest");
                    modelState.Remove("LoanInfo.OutstandingDebt");
                    modelState.Remove("LoanInfo.QtyMonthsPayment");
                    break;
                default:
                    break;
            }
        }
    }
}
