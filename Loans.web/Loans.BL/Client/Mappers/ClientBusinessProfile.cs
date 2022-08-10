using Loans.BL.Client.Dtos;
using AutoMapper;
using Loans.Data.Entities;

namespace Loans.BL.Client.Mappers
{
    public class ClientBusinessProfile : Profile
    {
        public ClientBusinessProfile()
        {
            CreateMap<ClientBusinessInfo, ClientBusinessDto>();
            CreateMap<ClientBusinessDto, ClientBusinessInfo>();
        }
    }
}
