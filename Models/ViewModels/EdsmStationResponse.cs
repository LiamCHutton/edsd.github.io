using EDStationDatabase.Models.StationSearch;

namespace EDStationDatabase.Models.ViewModels
{
    public class EdsmStationResponse
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Id64 { get; set; }
        public List<Station> Stations { get; set; }
    }
}
