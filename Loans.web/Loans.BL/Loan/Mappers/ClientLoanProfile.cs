using AutoMapper;
using Loans.Data.Entities;
using Loans.BL.Loan.Dtos;

namespace Loans.BL.Loan.Mappers
{
    public class ClientLoanProfile : Profile
    {
        public ClientLoanProfile()
        {
            CreateMap<ClientLoan, ClientLoanDto>();
            CreateMap<ClientLoanDto, ClientLoan>();
        }
    }
}
