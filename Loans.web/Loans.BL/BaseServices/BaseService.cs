using AutoMapper;
using Loans.Data;

namespace Loans.BL.BaseServices
{
    public abstract class BaseService
    {
        public readonly DatabaseContext context;
        public readonly IMapper mapper;

        protected BaseService(DatabaseContext databaseContext, IMapper mapper)
        {
            context = databaseContext;
            this.mapper = mapper;
        }
    }
}
