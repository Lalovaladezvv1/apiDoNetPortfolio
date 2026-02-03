using AutoMapper;
using PortfolioWebApi.Models;
using System;
using System.Globalization;

public class ExperienciaLaboralProfile : Profile
{
    public ExperienciaLaboralProfile()
    {
        CreateMap<ExperienciaLaboralCreateDto, ExperienciaLaboral>()
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => ParseDate(src.FechaInicio)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => ParseDate(src.FechaFin)));

        CreateMap<ExperienciaLaboralUpdateDto, ExperienciaLaboral>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore())
            .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => ParseDate(src.FechaInicio)))
            .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => ParseDate(src.FechaFin)));

        CreateMap<ExperienciaLaboral, ExperienciaLaboralReadDto>();
    }

    private static DateOnly? ParseDate(string? date)
    {
        if (string.IsNullOrWhiteSpace(date)) return null;

        return DateOnly.TryParseExact(
            date,
            "yyyy-MM-dd", 
            CultureInfo.InvariantCulture, 
            DateTimeStyles.None, 
            out var parsedDate
        ) ? parsedDate : null;
    }
}
