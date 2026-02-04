using AutoMapper;
using PortfolioWebApi.Models;

public class LogroProfile : Profile
{
    public LogroProfile()
    {
        CreateMap<LogroCreateDto, Logro>()
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore());

        CreateMap<LogroUpdateDto, Logro>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore());

        CreateMap<Logro, LogroReadDto>();
    }
}