using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.tfl.gov.uk");
            var request = new RestRequest("/StopPoint/490008660N/Arrivals");

            var response = client.Get<List<Arrival>>(request);

            var allArrivals = response.Data;
            
            var sortedArrivals = allArrivals.OrderBy(o => o.ExpectedArrival);
            // allArrivals.Sort(Arrival.CompareExpectedArrival);

            foreach (var arrival in sortedArrivals.Take(5))
            {
                Console.WriteLine(arrival.LineName);
                Console.WriteLine(arrival.ExpectedArrival);
                Console.WriteLine(arrival.DestinationName);
                Console.WriteLine(arrival.StationName);
                Console.WriteLine(arrival.PlatformName);
                Console.WriteLine("\n");
            }
        }
    }
}