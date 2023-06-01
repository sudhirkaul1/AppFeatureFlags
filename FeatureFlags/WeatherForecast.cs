namespace FeatureFlags
{
    public class WeatherForecast
    {
        public DateTime _date { get; set; }
        public int _temperatureC { get; set; }
        public string? _summary { get; set; }
        public int _temperatureF  {get; set; }

        public string _rainExpected { get; set; }

        public WeatherForecast(DateTime Date, int TemperatureC, string RainExpected, string? Summary)
        {
            _date = Date;
            _temperatureC = TemperatureC;
            _rainExpected = RainExpected;
            _summary = Summary;
            _temperatureF = 32 + (int)(_temperatureC / 0.5556);

        }

    }
}
