using System;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "NW51TL";
            
            var tflApi = new TFL_API();
            var coordinates = Postcode_API.GetLatLongFromPostcode(input);
            var nearestStops = tflApi.GetStopIDFromLongLat(coordinates.Item1, coordinates.Item2, 2);

            foreach (var stopId in nearestStops)
            {
                tflApi.PrintArrivalListFromStopID(stopId, 5);
            }

            //490008660N
        }
    }
}