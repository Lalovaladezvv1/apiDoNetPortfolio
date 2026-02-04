using AutoMapper;
using PortfolioWebApi.Models;
public class ExperienciaLaboralProfile : Profile
{
    public ExperienciaLaboralProfile()
    {
        CreateMap<ExperienciaLaboralCreateDto, ExperienciaLaboral>();

        CreateMap<ExperienciaLaboralUpdateDto, ExperienciaLaboral>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore());

        CreateMap<ExperienciaLaboral, ExperienciaLaboralReadDto>();
    }
}
