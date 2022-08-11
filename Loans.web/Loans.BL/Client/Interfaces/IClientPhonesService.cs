using Loans.BL.Client.Dtos;
using Loans.Data.Entities;

namespace Loans.BL.Client.Interfaces
{
    public interface IClientPhonesService
    {
        Task<IList<ClientPhoneDto>> GetAllClientPhonesAsync();
        Task<IList<ClientPhoneDto>> GetClientPhonesByClientIdAsync(int clientId);
        Task<ClientPhoneDto> GetClientPhonesByIdAsync(int id);
        Task<ClientPhoneDto> GetClientPhonesByNumberAsync(string number);

        Task DeleteClientPhoneAsync(int id);
        Task<ClientPhoneDto> SaveClientPhoneAsync(ClientPhoneDto clientPhone);
    }
}
