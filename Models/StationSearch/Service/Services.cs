namespace EDStationDatabase.Models.StationSearch.Service
{
    public class Services
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<StationService> StationServices { get; set; }
    }
}
