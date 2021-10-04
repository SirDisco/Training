using System;

namespace BusBoard
{
    // lineName
    // destinationName
    // expectedArrival
    // stationName
    
    public class Arrival
    {
        public string LineName { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string DestinationName { get; set; }
        public string StationName { get; set; }
        public string PlatformName { get; set; }
    }
}

/*
    public static int CompareExpectedArrival(Arrival x, Arrival y)
    {
        return y.ExpectedArrival.CompareTo(x.ExpectedArrival);
    }
*/