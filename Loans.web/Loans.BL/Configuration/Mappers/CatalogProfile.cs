using AutoMapper;
using Loans.BL.Configuration.Dtos;
using Loans.Data.Entities;

namespace Loans.BL.Configuration.Mappers
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Catalog, CatalogDto>();
            CreateMap<CatalogDto, Catalog>();
        }
    }
}
