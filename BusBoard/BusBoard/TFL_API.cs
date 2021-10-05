using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    public class TFL_API
    {
        private RestClient client;

        public TFL_API()
        {
            ClientCheck();
        }

        public IOrderedEnumerable<Arrival> GetArrivalListFromStopID(string id)
        {
            var request = new RestRequest($"/StopPoint/{id}/Arrivals");
            var response = client.Get<List<Arrival>>(request);

            var allArrivals = response.Data;

            var sortedList = allArrivals.OrderBy(o => o.ExpectedArrival);

            return sortedList;
        }

        public void PrintArrivalListFromStopID(string id, int howMany)
        {
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
        

        public List<string> GetStopIDFromLongLat(decimal latitude, decimal longitude, int howMany)
        {
            var type = "bus";
            var stopTypes = "NaptanPublicBusCoachTram";
            var request = 
                new RestRequest(
                                $"/StopPoint?stopTypes={stopTypes}" +
                                $"&lat={latitude}" +
                                $"&lon={longitude}" +
                                $"&useStopPointHierarchy=false&modes={type}");

            var response = client.Get<List<BusStop>>(request);

            var allBusStops = response.Data;

            List<string> result = new List<string>();
            
            //allBusStops.First().StopPoints = allBusStops.First().StopPoints.OrderBy(o => o.Distance).ToList();

            foreach (var stop in allBusStops[0].StopPoints)
            {
                Console.WriteLine(stop.NaptanId);
                result.Add(stop.NaptanId);
            }

            return result;



        }

        private void ClientCheck()
        {
            if (client == null)
            {
                client = new RestClient("https://api.tfl.gov.uk");
            }
        }


        
        
    }
}