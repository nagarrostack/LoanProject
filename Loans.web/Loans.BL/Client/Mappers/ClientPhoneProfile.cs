using Loans.BL.Client.Dtos;
using AutoMapper;
using Loans.Data.Entities;

namespace Loans.BL.Client.Mappers
{
    public class ClientPhoneProfile : Profile
    {
        public ClientPhoneProfile()
        {
            CreateMap<ClientPhone, ClientPhoneDto>();
            CreateMap<ClientPhoneDto, ClientPhone>();
        }
    }
}
