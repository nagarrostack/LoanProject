using AutoMapper;
using Loans.BL.BaseServices;
using Loans.BL.Client.Dtos;
using Loans.BL.Client.Interfaces;
using Loans.Data;

namespace Loans.BL.Client.Services
{
    public class ClientPhonesService : BaseService, IClientPhonesService
    {
        public ClientPhonesService(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<bool> DeleteClientPhoneAsync(int id)
        {
            var clientPhone = context.ClientPhones.FirstOrDefault(c => c.Id == id);
            if (clientPhone != null)
            {
                context.ClientPhones.Remove(clientPhone);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Client's phone with id {id} doesn't exist");
            }
        }

        public async Task<IList<ClientPhoneDto>> GetAllClientPhonesAsync()
        {
            var result = context.ClientPhones.ToList();
            return mapper.Map<List<ClientPhoneDto>>(result);
        }

        public async Task<IList<ClientPhoneDto>> GetClientPhonesByClientIdAsync(int id)
        {
            var result = context.ClientPhones.Where(c => c.ClientId == id).ToList();
            return mapper.Map<List<ClientPhoneDto>>(result);
        }

        public async Task<ClientPhoneDto> GetClientPhonesByIdAsync(int id)
        {
            var result = context.ClientPhones.FirstOrDefault(c => c.Id == id);
            if (result == null)
                return null;
            else
                return mapper.Map<ClientPhoneDto>(result);
        }

        public async Task<ClientPhoneDto> GetClientPhonesByNumberAsync(string number)
        {
            var result = context.ClientPhones.FirstOrDefault(c => c.Number == number);
            if (result == null)
                return null;
            else
                return mapper.Map<ClientPhoneDto>(result);
        }

        public async Task<ClientPhoneDto> SaveClientPhoneAsync(ClientPhoneDto clientPhone)
        {
            Data.Entities.ClientPhone entity;
            if (clientPhone.Id == 0)
            {
                entity = mapper.Map<Data.Entities.ClientPhone>(clientPhone);
                var max = context.Clients.Max(c => c.Id);
                entity.Id = max + 1;
                context.ClientPhones.Add(entity);
            }
            else
            {
                entity = context.ClientPhones.FirstOrDefault(c => c.Id == clientPhone.Id);
                mapper.Map<ClientPhoneDto, Data.Entities.ClientPhone>(clientPhone, entity);
            }
            context.SaveChanges();
            return clientPhone;
        }
    }
}
