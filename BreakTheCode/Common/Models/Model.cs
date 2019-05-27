using System.Collections.Generic;

namespace BreakTheCode.Models
{
    public class Model
    {
        public Model()
        {
            Panels = new List<Panel>();
        }

        public float RootX { get; set; }
        public float RootY { get; set; }
        public int OriginalDocumentHeight { get; set; }
        public int OriginalDocumentWidth { get; set; }
        public List<Panel> Panels { get; set; }
    }
}
