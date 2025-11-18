using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }   

        public Region Region { get; set; }

        public HashSet<WeatherData> WeatherData { get; set; }
    }
}
