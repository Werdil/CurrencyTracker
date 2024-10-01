namespace CurrencyTracker.WebApi.Dtos;
public class CurrencyHistoryDto
{
    public string CurrencyCode { get; set; }
    public List<ExchangeRateDto> Rates { get; set; }
}