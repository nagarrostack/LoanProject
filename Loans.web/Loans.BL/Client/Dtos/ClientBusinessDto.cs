using Loans.BL.BaseDtos;

namespace Loans.BL.Client.Dtos
{
    public class ClientBusinessDto: BaseClientDto
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int TaxId{ get; set; }
    }
}
