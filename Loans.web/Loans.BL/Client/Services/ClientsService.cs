using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Client.Dtos;
using Loans.BL.Client.Interfaces;
using Loans.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Loans.BL.Client.Services
{
    public class ClientsService : BaseService, IClientsService
    {
        public ClientsService(DatabaseContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = context.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                if (!context.ClientBusinessInfos.Any(c => c.ClientId == id) &&
                    !context.ClientLoans.Any(c => c.ClientId == id) &&
                        !context.ClientPhones.Any(c => c.ClientId == id))
                {
                    context.Clients.Remove(client);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            else
            {
                throw new InvalidOperationException($"Client with id {id} doesn't exist");
            }
        }

        public async Task<IList<ClientDto>> GetAllClientsAsync()
        {
            var result = context.Clients
                .Include(c => c.CountryCatalog)
                .Include(c => c.GenderCatalog)
                .Include(c => c.TitleCatalog)
                .ToList();
            return mapper.Map<List<ClientDto>>(result);
        }

        public async Task<IList<ClientDto>> GetClientByCountryIdAsync(int id)
        {
            var result = context.Clients
                .Include(c => c.CountryCatalog)
                .Include(c => c.GenderCatalog)
                .Include(c => c.TitleCatalog)
                .Where(c => c.CountryId == id).ToList();
            return mapper.Map<List<ClientDto>>(result);
        }

        public async Task<IList<ClientDto>> GetClientByGenderIdAsync(int id)
        {
            var result = context.Clients
                .Include(c => c.CountryCatalog)
                .Include(c => c.GenderCatalog)
                .Include(c => c.TitleCatalog)
                .Where(c => c.GenderId == id).ToList();
            return mapper.Map<List<ClientDto>>(result);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var result = context.Clients
                .Include(c => c.CountryCatalog)
                .Include(c => c.GenderCatalog)
                .Include(c => c.TitleCatalog)
                .FirstOrDefault(c => c.Id == id);
            if (result == null)
                return null;
            else
                return mapper.Map<ClientDto>(result);
        }

        public async Task<IList<ClientDto>> GetClientByNameAsync(string name)
        {
            var result = context.Clients
                .Include(c => c.CountryCatalog)
                .Include(c => c.GenderCatalog)
                .Include(c => c.TitleCatalog)
                .Where(c => c.Name.ToUpper().Contains(name.ToUpper())).ToList();
            return mapper.Map<List<ClientDto>>(result);
        }

        public async Task<ClientDto> SaveClientAsync(ClientDto client)
        {
            Data.Entities.Client entity;
            Data.Entities.Catalog 
                catGender = context.Catalogs.Find(client.GenderId),
                catCountry = context.Catalogs.Find(client.CountryId),
                catTitle = context.Catalogs.Find(client.TitleId);

            if (client.Id == 0)
            {
                entity = mapper.Map<Data.Entities.Client>(client);
                var max = context.Clients.Max(c => c.Id);
                entity.Id = max + 1;
                context.Clients.Add(entity);
            }
            else
            {
                entity = context.Clients
                    .FirstOrDefault(c => c.Id == client.Id);
                mapper.Map<ClientDto,Data.Entities.Client>(client, entity);
            }

            entity.TitleCatalog = catTitle;
            entity.CountryCatalog = catCountry;
            entity.GenderCatalog = catGender;
            await context.SaveChangesAsync();

            var result = await GetClientByIdAsync(entity.Id);
            return result;
        }
    }
}
