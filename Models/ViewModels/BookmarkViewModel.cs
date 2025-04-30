using EDStationDatabase.Models.Category;

namespace EDStationDatabase.Models.ViewModels
{
    public class BookmarkViewModel
    {
        public List<StationBookmark> StationBookmark { get; set; } = new List<StationBookmark>();
        public List<Superpower> Superpower { get; set; } = new List<Superpower>();
        public List<Economy> Economy { get; set; } = new List<Economy>();
        public List<Allegiance> Allegiance { get; set; } = new List<Allegiance>();
        public List<StationType> StationType { get; set; } = new List<StationType>();


        // A dictionary to match Allegiances with their respective Superpowers
        public Dictionary<int, string> AllegianceWithSuperpower { get; set; } = new Dictionary<int, string>();


        // Pagination properties
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}