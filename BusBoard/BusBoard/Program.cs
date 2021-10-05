using System;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "NW51TL";
            
            var coordinates = Postcode_API.GetLatLongFromPostcode(input);
            var nearestStops = TFL_API.GetStopIDFromLongLat(coordinates.Item1, coordinates.Item2, 2);

            foreach (var stopId in nearestStops)
                TFL_API.PrintArrivalListFromStopID(stopId, 5);
        }
    }
}