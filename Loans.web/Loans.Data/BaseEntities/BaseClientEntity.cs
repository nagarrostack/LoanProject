using Loans.Data.Entities;

namespace Loans.Data.BaseEntities
{
    public abstract class BaseClientEntity: BaseEntity
    {
        public int ClientId { get; set; }

        public Client Client { get; set; }

    }
}
