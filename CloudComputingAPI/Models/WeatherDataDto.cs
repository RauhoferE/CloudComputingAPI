namespace CloudComputingAPI.Models
{
    public class WeatherDataDto
    {
        public int Id { get; set; }

        public IdNameDto City { get; set; }

        public DateTime Date { get; set; }

        public float TemperatureCelsius { get; set; }
        public float HumidityPercent { get; set; }
        public float WindSpeedKph { get; set; }

        public ConditionDto Condition { get; set; }
    }
}
