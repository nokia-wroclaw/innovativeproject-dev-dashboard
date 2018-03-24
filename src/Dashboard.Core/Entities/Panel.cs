using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public abstract class Panel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PanelPosition Position { get; set; } = new PanelPosition();

        /// <summary>
        /// If panel may contain more than one card in itself
        /// </summary>
        public abstract bool IsDynamic { get; }

        public abstract string Discriminator { get; }

        public int ProjectId { get; private set; }

        private Project _project { get; set; }
        public Project Project {
            get => _project;
            set
            {
                _project = value;
                ProjectId = _project.Id;
            }
        }

        public bool ShouldSerializeProject()
        {
            return false;
        }
    }

    public class MemePanel : Panel
    {
        public override bool IsDynamic => false;
        public override string Discriminator => nameof(MemePanel);

        public string MemeApiToken { get; set; }
    }

    public class StaticBranchPanel : Panel
    {
        public override bool IsDynamic => false;
        public override string Discriminator => nameof(StaticBranchPanel);

        public IEnumerable<BranchName> StaticBranchNames { get; set; } = new List<BranchName>();
    }

    public class DynamicPipelinesPanel : Panel
    {
        public override bool IsDynamic => true;
        public override string Discriminator => nameof(DynamicPipelinesPanel);

        public int HowManyLastPipelinesToRead { get; set; }
    }

    public class PanelPosition
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
