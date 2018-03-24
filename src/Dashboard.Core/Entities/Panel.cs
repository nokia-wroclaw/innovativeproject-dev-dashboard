using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core.Entities
{
    public class Panel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Dynamic { get; set; }
        public PanelType Type { get; set; }
        public PanelPosition Position { get; set; } = new PanelPosition();
        public string Data { get; set; }

        public Project Project { get; set; }
    }

    public class PanelPosition
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }

    public enum PanelType
    {
        EmptyPanel, RandomMemePanel
    }
}
