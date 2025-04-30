namespace EDStationDatabase.Models.StationSearch
{
    public class StarSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EdsmId { get; set; }
        public long EdsmId64 { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation properties
        public ICollection<Station> Stations { get; set; }
    }
}
