using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    public class TFL_API
    {
        public IOrderedEnumerable<Arrival> GetArrivalListFromStopID(string id)
        {
            var client = new RestClient("https://api.tfl.go.uk");
            var request = new RestRequest($"/StopPoint/{id}/Arrivals");
            var response = client.Get<List<Arrival>>(request);

            var allArrivals = response.Data;

            var sortedList = allArrivals.OrderBy(o => o.ExpectedArrival);

            return sortedList;
        }

        public void PrintArrivalListFromStopID(string id, int howMany)
        {
            var client = new RestClient("https://api.tfl.gov.uk");
            var request = new RestRequest($"/StopPoint/{id}/Arrivals");

            var response = client.Get<List<Arrival>>(request);

            var allArrivals = response.Data;
            
            var sortedArrivals = allArrivals.OrderBy(o => o.ExpectedArrival);
            // allArrivals.Sort(Arrival.CompareExpectedArrival);

            foreach (var arrival in sortedArrivals.Take(howMany))
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