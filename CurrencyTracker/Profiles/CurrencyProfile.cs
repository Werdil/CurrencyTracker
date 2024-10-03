using AutoMapper;
using CurrencyTracker.Domain.Entities;
using CurrencyTracker.WebApi.Dtos;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<Currency, CurrencyRateInfoDto>()
            .ForMember(dest => dest.AverageRate, opt => opt.MapFrom(src => src.GetAverageRate()))
            .ForMember(dest => dest.MinRate, opt => opt.MapFrom(src => src.GetMinRate()))
            .ForMember(dest => dest.MaxRate, opt => opt.MapFrom(src => src.GetMaxRate()))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.YesterdayValue, opt => opt.MapFrom(src => src.GetRateForYesterday()));
    }
}
