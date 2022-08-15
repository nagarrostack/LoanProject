using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Loan.Dtos;
using Loans.BL.Loan.Interfaces;
using Loans.Data;

namespace Loans.BL.Loan.Services
{
    public class LoanService : BaseService, ILoanService
    {
        public LoanService(DatabaseContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<bool> DeleteClientLoan(int id)
        {
            var clientLoan = context.ClientLoans.FirstOrDefault(c => c.Id == id);
            if (clientLoan != null)
            {
                context.ClientLoans.Remove(clientLoan);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Client loan with id {id} doesn't exist");
            }
        }

        public async Task<IList<ClientLoanDto>> GetAllClientLoansAsync()
        {
            var result = context.ClientLoans.ToList();
            return mapper.Map<List<ClientLoanDto>>(result);
        }

        public async Task<IList<ClientLoanDto>> GetClientLoansByClientId(int clientId)
        {
            var result = context.ClientLoans.Where(c => c.ClientId == clientId).ToList();
            return mapper.Map<List<ClientLoanDto>>(result);
        }

        public async Task<ClientLoanDto> GetClientLoansById(int id)
        {
            var result = context.ClientLoans.FirstOrDefault(c => c.Id == id);
            if (result == null)
                return null;
            else
                return mapper.Map<ClientLoanDto>(result);
        }

        public async Task<IList<ClientLoanDto>> GetClientLoansByLoanDate(DateTime dateLoan)
        {
            var result = context.ClientLoans.Where(c => c.LoanDate.ToShortDateString() == dateLoan.ToShortDateString()).ToList();
            return mapper.Map<List<ClientLoanDto>>(result);
        }

        public async Task<IList<ClientLoanDto>> GetClientLoansByRating(int rating)
        {
            var result = context.ClientLoans.Where(c => c.Rating == rating).ToList();
            return mapper.Map<List<ClientLoanDto>>(result);
        }

        public async Task<ClientLoanDto> SaveClientLoans(ClientLoanDto clientLoan)
        {
            Data.Entities.ClientLoan entity;
            if (clientLoan.Id == 0)
            {
                entity = mapper.Map<Data.Entities.ClientLoan>(clientLoan);
                var max = context.ClientLoans.Max(c => c.Id);
                entity.Id = max + 1;
                context.ClientLoans.Add(entity);
            }
            else
            {
                entity = context.ClientLoans.FirstOrDefault(c => c.Id == clientLoan.Id);
                mapper.Map<ClientLoanDto, Data.Entities.ClientLoan>(clientLoan, entity);
            }
            context.SaveChanges();
            return clientLoan;
        }
    }
}
