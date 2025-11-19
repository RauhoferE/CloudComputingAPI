namespace CloudComputingAPI.Models
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IdNameDto> Citys { get; set; }
    }
}
