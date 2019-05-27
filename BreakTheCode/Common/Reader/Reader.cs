using BreakTheCode.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Xml;
using BreakTheCode.Models;

namespace BreakTheCode.Reader
{
    public class Reader : IReader<Model>
    {
        IFormatProvider provider = CultureInfo.InvariantCulture.NumberFormat;
        public Model Read(string filename)
        {
            Model model = new Model();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlElement xRoot = doc.DocumentElement;

            model.RootX = float.Parse(xRoot.GetAttribute("rootX"), provider);
            model.RootY = float.Parse(xRoot.GetAttribute("rootY"), provider);
           
            model.OriginalDocumentWidth = int.Parse(xRoot.GetAttribute("originalDocumentWidth"));
            model.OriginalDocumentHeight = int.Parse(xRoot.GetAttribute("originalDocumentHeight"));

            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Name == "panels")
                    SetPanels(xnode, model.Panels);
            }
            return model;
        }

        private void SetPanels(XmlNode xnode, List<Panel> panels)
        {
            foreach (XmlNode childnode in xnode.ChildNodes)
            {
                Panel panel = null;
                if (childnode.Name == "item")
                {
                    panel = new Panel();
                    SetItemAttribute(panel, childnode);
                    panels.Add(panel);
                }
                foreach (XmlNode attachNode in childnode.ChildNodes)
                {
                    if (attachNode.Name == "attachedPanels")
                    {
                        List<Panel> childPanel = panels.Find(p => p.PanelId == panel.PanelId).СhildPanel;
                        SetPanels(attachNode, childPanel);
                    }
                }
            }
        }

        private void SetItemAttribute(Panel panel, XmlNode childnode)
        {
            panel.PanelId = childnode.Attributes.GetNamedItem("panelId").Value;
            panel.PanelName = childnode.Attributes.GetNamedItem("panelName").Value;
            panel.HingeOffset = float.Parse(childnode.Attributes.GetNamedItem("hingeOffset").Value, provider);
            panel.PanelWidth = float.Parse(childnode.Attributes.GetNamedItem("panelWidth").Value, provider);
            panel.PanelHeight = float.Parse(childnode.Attributes.GetNamedItem("panelHeight").Value, provider);
            panel.AttachedToSide = int.Parse(childnode.Attributes.GetNamedItem("attachedToSide").Value);
        }
    }
}
