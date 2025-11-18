using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class WeatherData
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public DateTime Date { get; set; }
        public float TemperatureCelsius { get; set; }
        public float HumidityPercent { get; set; }
        public float WindSpeedKph { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
    }
}
