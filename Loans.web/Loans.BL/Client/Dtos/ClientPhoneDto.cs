using Loans.BL.BaseDtos;

namespace Loans.BL.Client.Dtos
{
    public class ClientPhoneDto: BaseClientDto
    {
        public int TypePhoneId { get; set; }
        public string Number { get; set; }
        public int CountryCodeId { get; set; }
    }
}
