using Loans.BL.Client.Dtos;

namespace Loans.BL.Client.Interfaces
{
    public interface IClientBusinessesService
    {
        Task<IList<ClientBusinessDto>> GetAllClientBusinessesAsync();
        Task<IList<ClientBusinessDto>> GetClientBusinessesByClientIdAsync(int clientId);
        Task<ClientBusinessDto> GetClientBusinessByIdAsync(int id);
        Task<IList<ClientBusinessDto>> GetClientBusinessByNameAsync(string name);

        Task<bool> DeleteClientBusinessAsync(int id);
        Task<ClientBusinessDto> SaveClientBusinessAsync(ClientBusinessDto clientBusiness);
    }
}
