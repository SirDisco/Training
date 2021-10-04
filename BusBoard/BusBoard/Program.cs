using System;
using System.Collections.Generic;
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
            
            foreach (var arrival in response.Data)
                Console.WriteLine(arrival.LineName);
        }
    }
}