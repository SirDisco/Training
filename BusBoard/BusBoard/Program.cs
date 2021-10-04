using System;
using RestSharp;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.tfl.gov.uk");
            var request = new RestRequest("/StopPoint/490008660N/Arrivals");

            var response = client.Get(request);
        }
    }
}