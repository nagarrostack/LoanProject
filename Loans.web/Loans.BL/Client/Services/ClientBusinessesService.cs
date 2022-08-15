using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Client.Dtos;
using Loans.BL.Client.Interfaces;
using Loans.Data;
using Microsoft.EntityFrameworkCore;

namespace Loans.BL.Client.Services
{
    public class ClientBusinessesService : BaseService, IClientBusinessesService
    {
        public ClientBusinessesService(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<bool> DeleteClientBusinessAsync(int id)
        {
            var client = context.ClientBusinessInfos.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                context.ClientBusinessInfos.Remove(client);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Client's business with id {id} doesn't exist");
            }
        }

        public async Task<IList<ClientBusinessDto>> GetAllClientBusinessesAsync()
        {
            var result = context.ClientBusinessInfos
                .Include(c => c.Client)
                .ThenInclude(c => c.TitleCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.CountryCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.GenderCatalog)
                .ToList();
            return mapper.Map<List<ClientBusinessDto>>(result);
        }

        public async Task<IList<ClientBusinessDto>> GetClientBusinessesByClientIdAsync(int clientId)
        {
            var result = context.ClientBusinessInfos
                .Where(c => c.ClientId == clientId)
                .Include(c => c.Client)
                .ThenInclude(c => c.TitleCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.CountryCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.GenderCatalog)
                .ToList();
            return mapper.Map<List<ClientBusinessDto>>(result);
        }

        public async Task<ClientBusinessDto> GetClientBusinessByIdAsync(int id)
        {
            var result = context.ClientBusinessInfos
                .Include(c => c.Client)
                .ThenInclude(c => c.TitleCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.CountryCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.GenderCatalog)
                .FirstOrDefault(c => c.Id == id);
            if (result == null)
                return null;
            else
                return mapper.Map<ClientBusinessDto>(result);
        }

        public async Task<IList<ClientBusinessDto>> GetClientBusinessByNameAsync(string name)
        {
            var result = context.ClientBusinessInfos
                .Include(c => c.Client)
                .ThenInclude(c => c.TitleCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.CountryCatalog)
                .Include(c => c.Client)
                .ThenInclude(c => c.GenderCatalog)
                .Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();
            return mapper.Map<List<ClientBusinessDto>>(result);
        }

        public async Task<ClientBusinessDto> SaveClientBusinessAsync(ClientBusinessDto client)
        {
            Data.Entities.ClientBusinessInfo entity;
            if (client.Id == 0)
            {
                entity = mapper.Map<Data.Entities.ClientBusinessInfo>(client);
                var max = context.ClientBusinessInfos.Max(c => c.Id);
                entity.Id = max + 1;
                context.ClientBusinessInfos.Add(entity);
            }
            else
            {
                entity = context.ClientBusinessInfos.FirstOrDefault(c => c.Id == client.Id);
                mapper.Map<ClientBusinessDto, Data.Entities.ClientBusinessInfo>(client, entity);
            }
            context.SaveChanges();
            return await GetClientBusinessByIdAsync(entity.Id);
        }
    }
}
