using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    public class TFL_API
    {
        public static IOrderedEnumerable<Arrival> GetArrivalListFromStopID(string id)
        {
            if (_Client == null)
                _Client = new RestClient("https://api.tfl.gov.uk");
            
            var request = new RestRequest($"/StopPoint/{id}/Arrivals");
            var response = _Client.Get<List<Arrival>>(request);
            ResponseValidator.CheckResponse(response);

            var allArrivals = response.Data;

            var sortedList = allArrivals.OrderBy(o => o.ExpectedArrival);

            return sortedList;
        }

        public static void PrintArrivalListFromStopID(string id, int howMany)
        {
            if (_Client == null)
                _Client = new RestClient("https://api.tfl.gov.uk");
            
            var request = new RestRequest($"/StopPoint/{id}/Arrivals");

            var response = _Client.Get<List<Arrival>>(request);
            ResponseValidator.CheckResponse(response);

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
        

        public static List<string> GetStopIDFromLongLat(LatitudeLongitude latitudeLongitude, int howMany)
        {
            if (_Client == null)
                _Client = new RestClient("https://api.tfl.gov.uk");
            
            var type = "bus";
            var stopTypes = "NaptanPublicBusCoachTram";
            var request = 
                new RestRequest(
                                $"/StopPoint?stopTypes={stopTypes}" +
                                $"&lat={latitudeLongitude.Latitude}" +
                                $"&lon={latitudeLongitude.Longitude}" +
                                $"&useStopPointHierarchy=false&modes={type}");

            var response = _Client.Get<List<BusStop>>(request);
            ResponseValidator.CheckResponse(response);

            var allBusStops = response.Data;

            List<string> result = new List<string>();
            
            allBusStops.First().StopPoints = allBusStops.First().StopPoints.OrderBy(o => o.Distance).ToList();

            foreach (var stop in allBusStops[0].StopPoints.Take(howMany))
            {
                result.Add(stop.NaptanId);
            }

            return result;
        }

        private static RestClient _Client;
    }
}