using System;
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
            ctx.AddRange(SeedMemeImages);
            ctx.AddRange(SeedProjects);
            ctx.AddRange(SeedPanels);

            ctx.SaveChanges();
        }

        private static List<MemeImage> _seedMemeImages;
        private static List<MemeImage> SeedMemeImages => _seedMemeImages ?? (_seedMemeImages = JsonConvert.DeserializeObject<List<MemeImage>>(File.ReadAllText("MemeSeed.json")));

        private static List<Panel> _seedPanels;
        private static List<Panel> SeedPanels => _seedPanels ?? (_seedPanels = new List<Panel>()
        {
            new MemePanel()
            {
                Title = "Fancy Title 1",
                Position = new PanelPosition() {Column = 6, Row = 0, Width = 4, Height = 2},
                StaticMemeUrl = SeedMemeImages[6].ImageUrl
            },
            new StaticBranchPanel()
            {
                Title = "Fancy Title 2",
                Position = new PanelPosition() {Column = 0, Row = 0, Width = 6, Height = 1},
                Project = SeedProjects.ElementAt(0),
                StaticBranchName = "master",
            },
            new DynamicPipelinesPanel() {
                Title = "Fancy Title Dynamic",
                Position = new PanelPosition() {Column = 0, Row = 1, Width = 6, Height = 2},
                Project = SeedProjects.ElementAt(0),
                HowManyLastPipelinesToRead = 2,
                PanelRegex = ".*"
            },
            new StaticBranchPanel()
            {
                Title = "Fancy Title Ember",
                Position = new PanelPosition() {Column = 10, Row = 0, Width = 6, Height = 1},
                Project = SeedProjects.ElementAt(1),
                StaticBranchName = "master",
            },
            new DynamicPipelinesPanel() {
                Title = "Fancy Title Dynamic Ember",
                Position = new PanelPosition() {Column = 10, Row = 1, Width = 6, Height = 2},
                Project = SeedProjects.ElementAt(1),
                HowManyLastPipelinesToRead = 2,
                PanelRegex = ".*"
            },
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
                ApiAuthenticationToken = "_3xydmKE2tpyWFVWgw7c",
                CiDataUpdateCronExpression = "*/40 * * * *",
                PipelinesNumber = 10,
                Pipelines = new List<Pipeline>()
                {
                    new Pipeline()
                    {
                        DataProviderPipelineId = 21584362, // fakeid
                        Ref = "master",
                        Sha = "927a9b13f083b7610d7ab31fa4204c1991668ddb",
                        CommitTitle = "Forth Commit Title",
                        CommiterName = "F Name",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "Build",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Prepare",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Test",
                                StageStatus = Status.Failed
                            },
                            new Stage()
                            {
                                StageName = "Post-test",
                                StageStatus = Status.Created
                            }
                        }
                    },
                    new Pipeline()
                    {
                        DataProviderPipelineId = 21584362, // fakeid
                        Ref = "someBrnach",
                        Sha = "927a9b13f083b7610d7ab31fa4204c1991668ddb",
                        CommitTitle = "Fifth Commit Title",
                        CommiterName = "Fe Name",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "Build",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Prepare",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Test",
                                StageStatus = Status.Failed
                            },
                            new Stage()
                            {
                                StageName = "Post-test",
                                StageStatus = Status.Created
                            }
                        }
                    },
                    new Pipeline()
                    {
                        DataProviderPipelineId = 21584362, // fakeid
                        Ref = "someanotherbranch",
                        Sha = "927a9b13f083b7610d7ab31fa4204c1991668ddb",
                        CommitTitle = "Wyje Commit Title",
                        CommiterName = "Bame",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "Build",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "Prepare",
                                StageStatus = Status.Running
                            },
                            new Stage()
                            {
                                StageName = "Test",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Post-test",
                                StageStatus = Status.Created
                            }
                        }
                    },

                }
            },
            new Project()
            {
                ProjectTitle = "Emberjs",
                DataProviderName = "Travis",
                ApiProjectId = "emberjs/ember.js",
                ApiHostUrl = "https://api.travis-ci.org",
                ApiAuthenticationToken = "DrIZnsWaqOgyJzMrNQnQkA",
                CiDataUpdateCronExpression = "*/50 * * * *",
                PipelinesNumber = 10,
                Pipelines = new List<Pipeline>()
                {
                    new Pipeline()
                    {
                        DataProviderPipelineId = 378215175, // fakeid
                        Ref = "master",
                        Sha = "927a9b13f083b7610d7ab31fa4204c1991668ddb",
                        CommitTitle = "First Commit Title",
                        CommiterName = "First Name",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "Basic test",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "Additional tests",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "Deploy",
                                StageStatus = Status.Success
                            }
                        }
                    },
                    new Pipeline()
                    {
                        DataProviderPipelineId = 378215175, // fakeid
                        Ref = "beta",
                        Sha = "f5126d3fad92215d15d9a1d5151ded2cd81a594e",
                        CommitTitle = "Second Commit Title",
                        CommiterName = "Second Name",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "26719.1",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "26719.2",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "26719.3",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "26719.4",
                                StageStatus = Status.Failed
                            },
                        }
                    },
                    new Pipeline()
                    {
                        DataProviderPipelineId = 378215175, // fakeid
                        Ref = "fancybranchname",
                        Sha = "f5126d3fad92215d15d9a1d5151ded2cd81a594e",
                        CommitTitle = "Third Commit Title",
                        CommiterName = "Third Name",
                        CreatedAt = DateTime.UtcNow,
                        StartedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FinishedAt = null,
                        Status = Status.Running,
                        Stages = new List<Stage>()
                        {
                            new Stage()
                            {
                                StageName = "26719.1",
                                StageStatus = Status.Created
                            },
                            new Stage()
                            {
                                StageName = "26719.2",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "26719.3",
                                StageStatus = Status.Success
                            },
                            new Stage()
                            {
                                StageName = "26719.4",
                                StageStatus = Status.Failed
                            },
                        }
                    }
                }
            }
        });
    }
}
