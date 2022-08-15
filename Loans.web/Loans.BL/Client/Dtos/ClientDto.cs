using Loans.BL.BaseDtos;

namespace Loans.BL.Client.Dtos
{
    public class ClientDto : BaseDto
    {
        public string Name { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        //public string FullName
        //{
        //    get
        //    {
        //        return $"{Name} {MidName} {LastName}";
        //    }
        //}
        public int TitleId { get; set; }
        public int GenderId { get; set; }
        public int CountryId { get; set; }
    }
}
