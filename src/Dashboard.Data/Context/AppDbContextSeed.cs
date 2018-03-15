using System.Collections.Generic;
using System.IO;
using System.Net;
using Dashboard.Core.Entities;
using Newtonsoft.Json;

namespace Dashboard.Data.Context
{
    public static class AppDbContextSeed
    {
        public static void Seed(AppDbContext ctx)
        {
            SeedItems.ForEach(x => ctx.Add(x));
            foreach (var pipe in SeedPipelines())
            {
                ctx.Add(pipe);
            }

            ctx.SaveChanges();
        }

        private static List<ToDoItem> SeedItems => new List<ToDoItem>()
        {
            new ToDoItem() {Id = 1, Text = "Hello"},
            new ToDoItem() {Id = 2, Text = "Cyka"},
            new ToDoItem() {Id = 3, Text = "Ta"},
        };

        private static List<Pipeline> SeedPipelines()
        {
            string htmlResponse = "";
            string uri = @"https://gitlab.com/api/v4/projects/13083/pipelines?ref=master&per_page=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("PRIVATE-TOKEN: 6h-Xjym_EFy8DBxPDR9z");
            request.Accept = "application/json";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
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
