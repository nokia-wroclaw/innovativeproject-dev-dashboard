using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Dashboard.Application.Interfaces.Services;
using Dashboard.Core.Entities;
using Dashboard.Core.Interfaces.Repositories;
using Newtonsoft.Json;

namespace Dashboard.Application.Services
{
    public class GitLabDataProvider : ICIDataProvider
    {
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
        public async Task<IEnumerable<Pipeline>> GetAllAsync()
        {
            string htmlResponse = "";
            //Prepare API call
            string uri = @"https://gitlab.com/api/v4/projects/13083/pipelines";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("PRIVATE-TOKEN: 6h-Xjym_EFy8DBxPDR9z");
            request.Accept = "application/json";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            //GET response
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
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
