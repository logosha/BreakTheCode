using System.Collections.Generic;

namespace BreakTheCode.Models
{
    public class Panel
    {
        public Panel()
        {
            СhildPanel = new List<Panel>();
        }

        public string PanelId { get; set; }
        public string PanelName { get; set; }
        public float HingeOffset { get; set; }
        public float PanelWidth { get; set; }
        public float PanelHeight { get; set; }
        public int AttachedToSide { get; set; }
        public float PointNullX { get; set; }
        public float PointNullY { get; set; }
        public int RouteRotate { get; set; }
        public bool Rotate { get; set; }
        public List<Panel> СhildPanel;
    }
}
