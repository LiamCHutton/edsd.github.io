namespace EDStationDatabase.Models.StationSearch
{
    public class Station
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Government { get; set; }
        public string Allegiance { get; set; }
        public string Economy { get; set; }
        public bool HasMarket { get; set; }
        public bool HasShipyard { get; set; }
        public bool HasOutfitting { get; set; }
        public double? DistanceToArrival { get; set; }

        // New properties
        public string SystemName { get; set; }
        public double? DistanceFromReference { get; set; }
    }
}
