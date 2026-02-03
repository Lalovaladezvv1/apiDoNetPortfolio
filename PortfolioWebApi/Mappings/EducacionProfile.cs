using AutoMapper;
using PortfolioWebApi.Models;

public class EducacionProfile : Profile
{
    public EducacionProfile()
    {
        CreateMap<EducacionCreateDto, Educacion>()
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.FechaActualizacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore());

        CreateMap<EducacionUpdateDto, Educacion>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore());

        CreateMap<Educacion, EducacionReadDto>();
    }
}
