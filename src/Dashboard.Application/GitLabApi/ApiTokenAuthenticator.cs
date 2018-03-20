using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace Dashboard.Application.GitLabApi
{
    public class ApiTokenAuthenticator : IAuthenticator
    {
        private readonly string _apiKey;

        public ApiTokenAuthenticator(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("PRIVATE-TOKEN", _apiKey);
        }
    }
}
