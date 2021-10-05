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
            var tfl = new TFL_API();
            tfl.PrintArrivalListFromStopID("490008660N", 5);

        }
    }
}