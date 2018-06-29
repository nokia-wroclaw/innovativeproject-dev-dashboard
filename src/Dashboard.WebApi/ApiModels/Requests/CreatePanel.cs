using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Dashboard.Core.Entities;
using Dashboard.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Examples;

namespace Dashboard.WebApi.ApiModels.Requests
{
    [JsonConverter(typeof(MyCustomConverter))]
    public class CreatePanel
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public PanelPosition Position { get; set; }

        public virtual Panel MapEntity(CreatePanel model) => null;

        public class MyCustomConverter : JsonCreationConverter<CreatePanel>
        {
            protected override CreatePanel Create(Type objectType, JObject jObject)
            {
                //TODO: read the raw JSON object through jObject to identify the type
                //e.g. here I'm reading a 'TypeName' property:

                var panelTypeName = jObject.Value<string>("typeName");
                switch (panelTypeName)
                {
                    case nameof(CreateDynamicPipelinePanel):
                        return new CreateDynamicPipelinePanel();
                    case nameof(CreateStaticBranchPanel):
                        return new CreateStaticBranchPanel();
                    case nameof(CreateMemePanel):
                        return new CreateMemePanel();
                    default:
                        return null;
                }
            }
        }
    }

    public class CreateDynamicPipelinePanel : CreatePanel
    {
        [Required]
        public int HowManyLastPipelinesToRead { get; set; }
        [Required]
        public string PanelRegex { get; set; }
        [Required]
        public int ProjectId { get; set; }

        public override Panel MapEntity(CreatePanel model)
        {
            var realModel = (CreateDynamicPipelinePanel) model;

            //TODO: change when automapper
            var entity = new DynamicPipelinesPanel()
            {
                Title = realModel.Title,
                Position = new PanelPosition()
                {
                    Column = realModel.Position.Column,
                    Row = realModel.Position.Row,
                    Width = realModel.Position.Width,
                    Height = realModel.Position.Height
                },
                ProjectId = realModel.ProjectId,
                HowManyLastPipelinesToRead = realModel.HowManyLastPipelinesToRead,
                PanelRegex = realModel.PanelRegex
            };

            return entity;
        }
    }

    public class CreateStaticBranchPanel : CreatePanel
    {
        [Required]
        public string StaticBranchName { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public override Panel MapEntity(CreatePanel model)
        {
            var realModel = (CreateStaticBranchPanel) model;

            //TODO: change when automapper
            var entity = new StaticBranchPanel()
            {
                Title = realModel.Title,
                Position = new PanelPosition()
                {
                    Column = realModel.Position.Column,
                    Row = realModel.Position.Row,
                    Width = realModel.Position.Width,
                    Height = realModel.Position.Height
                },
                ProjectId = realModel.ProjectId,
                StaticBranchName = realModel.StaticBranchName,
            };
            return entity;
        }
    }

    public class CreateMemePanel : CreatePanel
    {
        public override Panel MapEntity(CreatePanel model)
        {
            var realModel = (CreateMemePanel)model;

            //TODO: change when automapper
            var entity = new MemePanel()
            {
                Title = realModel.Title,
                Position = new PanelPosition()
                {
                    Column = realModel.Position.Column,
                    Row = realModel.Position.Row,
                    Width = realModel.Position.Width,
                    Height = realModel.Position.Height
                },
            };
            return entity;
        }
    }


    public class CreateDynamicPipelinePanelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CreateDynamicPipelinePanel()
            {
                TypeName = nameof(CreateDynamicPipelinePanel),
                Title = "Dynami Pipelines Title",
                ProjectId = 1,
                HowManyLastPipelinesToRead = 3,
                PanelRegex = ".*",
                Position = new PanelPosition()
                {
                    Width = 1,
                    Height = 2,
                    Column = 3,
                    Row = 4,
                }
            };
        }
    }
}
