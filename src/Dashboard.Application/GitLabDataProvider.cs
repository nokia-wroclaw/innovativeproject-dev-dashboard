using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces;
using Dashboard.Core.Interfaces.Repositories;
using Newtonsoft.Json;

namespace Dashboard.Application
{
    public class GitLabDataProvider : ICIDataProvider
    {
        public string Name => "GitLab";

        private readonly IPipelineRepository _pipelineRepository;

        public GitLabDataProvider(IPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
        }

        public Task<Pipeline> GetMasterAsync()
        {
            return _pipelineRepository.FindOneByAsync(pipe => pipe.Ref.Equals("master"));
        }

        /// <summary>
        /// Returns few latest pipelines
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Pipeline>> GetAllAsync(string host, string projectId, string apiKey)
        {
            string htmlResponse = "";
            //Prepare API call
            string uri = $@"{host}/api/v4/projects/{projectId}/pipelines";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add($"PRIVATE-TOKEN: {apiKey}");
            request.Accept = "application/json";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //GET response
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                htmlResponse = reader.ReadToEnd();
            }

            //return response in JSON
            return JsonConvert.DeserializeObject<List<Pipeline>>(htmlResponse);
        }
    }
}
