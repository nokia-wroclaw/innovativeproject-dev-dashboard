using System;
using System.ComponentModel.DataAnnotations;
using Dashboard.Core.Entities;
using Dashboard.WebApi.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dashboard.WebApi.ApiModels.Requests
{
    [JsonConverter(typeof(MyCustomConverter))]
    public abstract class UpdatePanel
    {
        [Required]
        public string TypeName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public PanelPosition Position { get; set; }

        public abstract Panel MapEntity(UpdatePanel model);

        private class MyCustomConverter : JsonCreationConverter<UpdatePanel>
        {
            protected override UpdatePanel Create(Type objectType, JObject jObject)
            {
                //TODO: read the raw JSON object through jObject to identify the type
                //e.g. here I'm reading a 'TypeName' property:

                var panelTypeName = jObject.Value<string>("typeName");
                switch (panelTypeName)
                {
                    case nameof(UpdateDynamicPipelinePanel):
                        return new UpdateDynamicPipelinePanel();
                    case nameof(UpdateStaticBranchPanel):
                        return new UpdateStaticBranchPanel();
                    case nameof(UpdateMemePanel):
                        return new UpdateMemePanel();
                    default:
                        return null;
                }
            }
        }
    }

    public class UpdateDynamicPipelinePanel : UpdatePanel
    {
        [Required]
        public int HowManyLastPipelinesToRead { get; set; }
        [Required]
        public string PanelRegex { get; set; }
        [Required]
        public int ProjectId { get; set; }

        public override Panel MapEntity(UpdatePanel model)
        {
            var realModel = (UpdateDynamicPipelinePanel)model;

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
                HowManyLastPipelinesToRead = realModel.HowManyLastPipelinesToRead
            };

            return entity;
        }
    }

    public class UpdateStaticBranchPanel : UpdatePanel
    {
        [Required]
        public string StaticBranchName { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public override Panel MapEntity(UpdatePanel model)
        {
            var realModel = (UpdateStaticBranchPanel)model;

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

    public class UpdateMemePanel : UpdatePanel
    {
        [Required]
        public string MemeApiToken { get; set; }

        public override Panel MapEntity(UpdatePanel model)
        {
            var realModel = (UpdateMemePanel)model;

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
                MemeApiToken = realModel.MemeApiToken,
            };
            return entity;
        }
    }
}
