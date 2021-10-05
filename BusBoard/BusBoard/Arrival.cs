using System;

namespace BusBoard
{
    public class Arrival
    {
        public string LineName { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string DestinationName { get; set; }
        public string StationName { get; set; }
        public string PlatformName { get; set; }
    }
}