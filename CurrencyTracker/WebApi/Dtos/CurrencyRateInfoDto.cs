namespace CurrencyTracker.WebApi.Dtos
{
    public class CurrencyRateInfoDto
    {
        public decimal? AverageRate { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public string Code { get; internal set; }
    }
}
