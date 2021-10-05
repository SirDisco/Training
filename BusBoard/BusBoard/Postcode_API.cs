using System;
using System.Collections.Generic;
using RestSharp;

namespace BusBoard
{
    public class PostcodeResult
    {
        public List<LatitudeLongitude> Result { get; set; }
    }
    
    public class LatitudeLongitude
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
    
    public class Postcode_API
    {
        public static LatitudeLongitude GetLatLongFromPostcode(string postcode)
        {
            if (_Client == null)
                _Client = new RestClient("http://api.postcodes.io/");

            var request = new RestRequest($"postcodes?q={postcode}");

            var response = _Client.Get<PostcodeResult>(request).Data;

            if (response.Result.Count == 0)
            {
                Console.WriteLine("Unable to get latitude and longitude");
                return null;
            }

            return response.Result[0];
        }

        private static RestClient? _Client = null;
    }
}