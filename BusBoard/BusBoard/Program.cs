using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "NW51TL";
            var howManyStops = 2;
            var howManyArrivals = 5;
            
            var coordinates = Postcode_API.GetLatLongFromPostcode(input);
            var nearestStops = TFL_API.GetStopFromLongLat(coordinates, howManyStops);

            foreach (var busStop in nearestStops)
            {
                PrintBusStopHeader(busStop);
                PrintListOfArrivals(TFL_API.GetArrivalListFromStop(busStop, howManyArrivals));
            }
        }

        static void PrintBusStopHeader(BusStop stop)
        {
            Console.WriteLine($"{stop.CommonName} nearest arrivals are:");
        }
        
        static void PrintListOfArrivals(IEnumerable<Arrival> arrivals)
        {
            foreach (var arrival in arrivals)
            {
                Console.WriteLine($"The {arrival.LineName} arriving at {arrival.ExpectedArrival}\n" +
                                  $"Destination: {arrival.DestinationName}\n");
            }
            Console.WriteLine();
        }
    }
}