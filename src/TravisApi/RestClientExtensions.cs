using System.Threading.Tasks;
using Dashboard.Core.Exceptions;
using RestSharp;

namespace TravisApi
{
    public static class RestClientExtensions
    {
        public static async Task<T> EnsureSuccess<T>(this Task<IRestResponse<T>> requestTask)
        {
            var response = await requestTask;
            if (!response.IsSuccessful)
                throw new ApplicationHttpRequestException(response);

            return response.Data;
        }
    }
}
