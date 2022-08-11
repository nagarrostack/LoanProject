using Loans.BL.Client.Dtos;

namespace Loans.BL.Client.Interfaces
{
    public interface IClientsService
    {
        Task<IList<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<IList<ClientDto>> GetClientByNameAsync(string name);
        Task<IList<ClientDto>> GetClientByGenderIdAsync(int id);
        Task<IList<ClientDto>> GetClientByCountryIdAsync(int id);

        Task DeleteClientAsync(int id);
        Task <ClientDto> SaveClientAsync(ClientDto client);
    }
}
