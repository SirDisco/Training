using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    public class TFL_API
    {
        public static List<BusStop> GetStopFromLongLat(LatitudeLongitude latitudeLongitude, int howMany)
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

            var response = _Client.Get<List<BusStopsWithinArea>>(request);
            ResponseValidator.CheckResponse(response);

            var allBusStops = response.Data.First().StopPoints;
            
            return allBusStops.OrderBy(o => o.Distance).Take(howMany).ToList();
        }
        
        public static IEnumerable<Arrival> GetArrivalListFromStop(BusStop stop, int howMany)
        {
            if (_Client == null)
                _Client = new RestClient("https://api.tfl.gov.uk");
            
            var request = new RestRequest($"/StopPoint/{stop.NaptanId}/Arrivals");
            var response = _Client.Get<List<Arrival>>(request);

            var allArrivals = response.Data;

            var sortedList = allArrivals.OrderBy(o => o.ExpectedArrival);

            return sortedList.Take(howMany);
        }

        private static RestClient _Client;
    }
}