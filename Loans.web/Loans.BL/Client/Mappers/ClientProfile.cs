using Loans.BL.Client.Dtos;
using AutoMapper;

namespace Loans.BL.Client.Mappers
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Data.Entities.Client, ClientDto>();
            CreateMap<ClientDto, Data.Entities.Client>();
        }
    }
}
