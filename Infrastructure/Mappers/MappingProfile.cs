using AutoMapper;
using PassCodeManager.Classified.Entities;
using PassCodeManager.DTO.RequestObjects;

namespace PassCodeManager.Infrastructure.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserObject, TblUsers>();
        }
    }
}
