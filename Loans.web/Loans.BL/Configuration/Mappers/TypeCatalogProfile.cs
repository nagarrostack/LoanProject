using AutoMapper;
using Loans.BL.Configuration.Dtos;
using Loans.Data.Entities;

namespace Loans.BL.Configuration.Mappers
{
    public class TypeCatalogProfile : Profile
    {
        public TypeCatalogProfile()
        {
            CreateMap<TypeCatalog, TypeCatalogDto>();
            CreateMap<TypeCatalogDto, TypeCatalog>();
        }
    }
}
