using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Condition
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public HashSet<WeatherData> WeatherData { get; set; }
    }
}
