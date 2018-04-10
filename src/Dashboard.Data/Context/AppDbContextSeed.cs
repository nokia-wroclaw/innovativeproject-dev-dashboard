﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Dashboard.Core.Entities;
using Newtonsoft.Json;

namespace Dashboard.Data.Context
{
    public static class AppDbContextSeed
    {
        public static void Seed(AppDbContext ctx)
        {
            //SeedPipelines().ForEach(x => ctx.Add(x));
            ctx.AddRange(SeedProjects);
            ctx.AddRange(SeedPanels);

            ctx.SaveChanges();
        }

        private static List<Panel> _seedPanels;
        private static List<Panel> SeedPanels => _seedPanels ?? (_seedPanels = new List<Panel>()
        {
            new MemePanel()
            {
                Title = "Fancy Title 1",
                Position = new PanelPosition() {Column = 0, Row = 0, Width = 2, Height = 2},
                Project = SeedProjects.ElementAt(0),
                MemeApiToken = "JakisAPiTokenZ"
            },
            new StaticBranchPanel()
            {
                Title = "Fancy Title 2",
                Position = new PanelPosition() {Column = 2, Row = 0, Width = 4, Height = 1},
                Project = SeedProjects.ElementAt(0),
                StaticBranchName = "master",
            }
        });

        private static List<Project> _seedProjects;
        private static List<Project> SeedProjects => _seedProjects ?? (_seedProjects = new List<Project>()
        {
            new Project()
            {
                ProjectTitle = "GitLab CE",
                DataProviderName = "GitLab",
                ApiHostUrl = "https://gitlab.com",
                ApiProjectId = "13083",
                ApiAuthenticationToken = "6h-Xjym_EFy8DBxPDR9z",
                StaticPipelines = new List<Pipeline>()
                {
                    new Pipeline()
                    {
                        DataProviderId = 1901, // fakeid
                        Ref = "master",
                        Sha = "79aa00321063daf8f650683373db29832c8e13f1",
                        Status = "running"
                    }
                }
            }
        });

        private static List<Pipeline> SeedPipelines()
        {
            string htmlResponse = "";
            string uri = @"https://gitlab.com/api/v4/projects/13083/pipelines?ref=master&per_page=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("PRIVATE-TOKEN: 6h-Xjym_EFy8DBxPDR9z");
            request.Accept = "application/json";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                htmlResponse = reader.ReadToEnd();
            }

            Pipelines allPipelines = JsonConvert.DeserializeObject<Pipelines>(htmlResponse);

            return new List<Pipeline>() { allPipelines[0] };
        }
    }
}
