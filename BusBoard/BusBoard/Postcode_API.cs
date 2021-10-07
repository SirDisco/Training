using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using RestSharp;

namespace BusBoard
{
    public class PostcodeResult
    {
        public List<LatitudeLongitude> Result { get; set; }
    }
    
    public class LatitudeLongitude
    {
        public LatitudeLongitude() {}
        
        public LatitudeLongitude(double lat, double lon)
        {
            Latitude = (decimal)lat;
            Longitude = (decimal)lon;
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
    
    public class Postcode_API
    {
        public static LatitudeLongitude GetLatLongFromPostcode(string postcode)
        {
            // Define a regular expression for postcodes.
            Regex postcodeRx = new Regex(
                @"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase
            );
            if (!postcodeRx.Match(postcode).Success)
            {
                throw new Exception("Invalid postcode Entered");
            }
            
            if (_Client == null)
                _Client = new RestClient("http://api.postcodes.io/");

            var request = new RestRequest($"postcodes?q={postcode}");
            
            
            var response = _Client.Get<PostcodeResult>(request);
            ResponseValidator.CheckResponse(response);
            
            var data = response.Data;
            return data.Result[0];
        }

        private static RestClient? _Client = null;
    }
}