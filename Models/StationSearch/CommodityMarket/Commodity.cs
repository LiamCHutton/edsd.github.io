namespace EDStationDatabase.Models.StationSearch.CommodityMarket
{
    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public ICollection<StationCommodity> StationCommodities { get; set; }
    }
}
