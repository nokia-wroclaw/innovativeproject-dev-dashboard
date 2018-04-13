using System;
using RestSharp;

namespace Dashboard.Core.Exceptions
{
    public class ApplicationHttpRequestException : Exception
    {
        public IRestResponse Response { get; }

        public ApplicationHttpRequestException(IRestResponse response)
        {
            Response = response;
        }
    }
}
