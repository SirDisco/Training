using System;
using System.Net;
using RestSharp;

namespace BusBoard
{
    public class ResponseValidator
    {
        public static void CheckResponse(IRestResponse res)
        {
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"{res.ErrorMessage}");
            }
        }
    }
}