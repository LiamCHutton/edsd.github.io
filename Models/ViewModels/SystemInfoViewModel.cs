using EDStationDatabase.Controllers;
using EDStationDatabase.Models.StationSearch;

namespace EDStationDatabase.Models.ViewModels
{
    // Example model - customize to match the EDSM JSON fields
    public class SystemInfoViewModel
    {
        public string Name { get; set; }
        public Coordinates Coords { get; set; }
        public string Allegiance { get; set; }
        public string Government { get; set; }
        public string Faction { get; set; }
    }
}
