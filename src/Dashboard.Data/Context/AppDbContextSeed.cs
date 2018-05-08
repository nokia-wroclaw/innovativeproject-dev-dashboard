using System.Collections.Generic;
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
                MemeApiToken = "JakisAPiTokenZ"
            },
            new StaticBranchPanel()
            {
                Title = "Fancy Title 2",
                Position = new PanelPosition() {Column = 2, Row = 0, Width = 4, Height = 1},
                Project = SeedProjects.ElementAt(0),
                StaticBranchName = "master",
            },
            new DynamicPipelinesPanel() {
                Title = "Fancy Title Dynamic",
                Position = new PanelPosition() {Column = 1, Row = 2, Width = 2, Height = 3},
                Project = SeedProjects.ElementAt(0),
                HowManyLastPipelinesToRead = 2,
                PanelRegex = ".*"
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
                CiDataUpdateCronExpression = "*/4 * * * *",
                //StaticPipelines = new List<Pipeline>()
                PipelinesNumber = 100,
                Pipelines = new List<Pipeline>()
                {
                    new Pipeline()
                    {
                        DataProviderId = 21584362, // fakeid
                        Ref = "master",
                        Sha = "927a9b13f083b7610d7ab31fa4204c1991668ddb",
                        Status = "passed"
                    }
                }
            }
        });
    }
}
