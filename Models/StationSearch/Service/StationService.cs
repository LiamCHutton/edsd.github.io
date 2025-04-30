namespace EDStationDatabase.Models.StationSearch.Service
{
    public class StationService
    {
        public int StationId { get; set; }
        public Station Station { get; set; }

        public int ServiceId { get; set; }
        public Services Service { get; set; }

        public DateTime LastVerified { get; set; }
    }
}
