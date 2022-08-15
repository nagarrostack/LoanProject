using Loans.BL.Loan.Dtos;

namespace Loans.BL.Loan.Interfaces
{
    public interface ILoanService
    {
        Task<IList<ClientLoanDto>> GetAllClientLoansAsync();
        Task<IList<ClientLoanDto>> GetClientLoansByClientId(int clientId);
        Task<ClientLoanDto> GetClientLoansById(int id);
        Task<IList<ClientLoanDto>> GetClientLoansByRating(int rating);
        Task<IList<ClientLoanDto>> GetClientLoansByLoanDate(DateTime dateLoan);

        Task<bool> DeleteClientLoan(int id);
        Task<ClientLoanDto> SaveClientLoans(ClientLoanDto clientLoan);
    }
}
