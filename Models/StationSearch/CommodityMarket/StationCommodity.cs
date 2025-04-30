namespace EDStationDatabase.Models.StationSearch.CommodityMarket
{
    public class StationCommodity
    {
        public int StationId { get; set; }
        public Station Station { get; set; }

        public int CommodityId { get; set; }
        public Commodity Commodity { get; set; }

        public int Supply { get; set; }
        public int Demand { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
