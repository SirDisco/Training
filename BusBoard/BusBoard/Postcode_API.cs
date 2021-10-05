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
        public static Tuple<decimal, decimal> GetLatLongFromPostcode(string postcode)
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
            
            var latitude = response.Result[0].Latitude;
            var longitude = response.Result[0].Longitude;
            
            return new Tuple<decimal, decimal>(latitude, longitude);
        }

        private static RestClient? _Client = null;
    }
}