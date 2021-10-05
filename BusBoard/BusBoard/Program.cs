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
            var tflApi = new TFL_API();
            var nearestStops = tflApi.GetStopIDFromLongLat((decimal)51.553935, (decimal)-0.144754, 2);


            
            //490008660N



        }
    }
}