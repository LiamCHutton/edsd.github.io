using EDStationDatabase.Models.StationSearch;

namespace EDStationDatabase.Models.ViewModels
{
    public class StationListViewModel
    {
        public string Name { get; set; }
        public bool IsMultiSystemSearch { get; set; }
        public bool IncludesNearbyStations { get; set; }
        public int SearchRadius { get; set; }
        public int TotalStationCount { get; set; }
        public List<Station> Stations { get; set; }
    }
}