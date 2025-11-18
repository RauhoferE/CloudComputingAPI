using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<City> Cities { get; set; }
    }
}
