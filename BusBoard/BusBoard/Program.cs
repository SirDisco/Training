using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var howManyStops = 2;
            var howManyArrivals = 5;

            var UI = new UserInterface(howManyStops, howManyArrivals);
            UI.Run();
            
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