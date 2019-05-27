using BreakTheCode.Interfaces;
using BreakTheCode.Models;
using System.Drawing;

namespace BreakTheCode.Drawer
{
    public class Drawer : IDrawer
    {
        Model model;
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 5);

        public Drawer(Model model, Graphics graphics)
        {
            this.model = model;
            this.graphics = graphics;
        }

        public void Paint()
        {
            Panel panel = model.Panels.Find(p => p.PanelName == "root panel");

            panel.PointNullX = model.RootX;
            panel.PointNullY = model.RootY;

            graphics.DrawRectangle(pen, 
                                   panel.PointNullX - panel.PanelWidth / 2, 
                                   panel.PointNullY - panel.PanelHeight, 
                                   panel.PanelWidth, 
                                   panel.PanelHeight);

            PaintChildPanel(panel);
        }

        private void PaintChildPanel(Panel panel)
        {
            foreach (Panel child in panel.СhildPanel)
            {
                (float X, float Y, float Width, float Height) = GetParametersOfRectangle(panel, child);
                graphics.DrawRectangle(pen, X, Y, Width, Height);
                PaintChildPanel(child);
            }
        }

        private (float X, float Y, float Width, float Height) GetParametersOfRectangle(Panel parent, Panel child)
        {
            (float X, float Y, float Width, float Height) rectangle = (X: 0, Y: 0, Width: 0, Height: 0);

            int side = child.AttachedToSide + parent.RouteRotate;

            if (side == 4)
                side = 0;

            if (side == 1 || side == 3)
            {
                child.Rotate = true;
                rectangle.Width = child.PanelHeight;
                rectangle.Height = child.PanelWidth;
            }
            else
            {
                child.Rotate = false;
                rectangle.Width = child.PanelWidth;
                rectangle.Height = child.PanelHeight;
            }

            switch (side)
            {
                case 0:
                    {
                        child.RouteRotate = 0;
                        child.PointNullX = parent.PointNullX;
                        rectangle.X = parent.PointNullX - rectangle.Width / 2 + child.HingeOffset;
                        rectangle.Y = parent.PointNullY;
                        if (!parent.Rotate)
                            child.PointNullY = parent.PointNullY + parent.PanelHeight;
                        else
                            child.PointNullY = parent.PointNullY + parent.PanelWidth;
                        break;
                    }
                case 1:
                    {
                        child.RouteRotate = -1;
                        child.PointNullY = parent.PointNullY;
                        if (!parent.Rotate)
                        {
                            child.PointNullX = parent.PointNullX + parent.PanelWidth / 2 + rectangle.Width / 2;
                            rectangle.X = parent.PointNullX + parent.PanelWidth / 2;
                            rectangle.Y = (parent.PointNullY - parent.PanelHeight / 2) - rectangle.Height / 2 + child.HingeOffset;//+ parent.HingeOffset
                        }
                        else
                        {
                            child.PointNullX = parent.PointNullX + parent.PanelHeight / 2 + rectangle.Width / 2;
                            rectangle.X = parent.PointNullX + parent.PanelHeight / 2;
                            rectangle.Y = (parent.PointNullY - parent.PanelWidth / 2) - rectangle.Height / 2 + child.HingeOffset;//+ parent.HingeOffset
                        }
                        break;
                    }
                case 2:
                    {
                        child.RouteRotate = 0;
                        child.PointNullX = parent.PointNullX;
                        rectangle.X = parent.PointNullX - rectangle.Width / 2 + child.HingeOffset;
                        if (!parent.Rotate)
                        {
                            child.PointNullY = parent.PointNullY - parent.PanelHeight;
                            rectangle.Y = parent.PointNullY - parent.PanelHeight - rectangle.Height;
                        }
                        else
                        {
                            child.PointNullY = parent.PointNullY - parent.PanelWidth;
                            rectangle.Y = parent.PointNullY - parent.PanelWidth - rectangle.Height;
                        }
                        break;
                    }
                case 3:
                    {
                        child.RouteRotate = 1;
                        child.PointNullY = parent.PointNullY;
                        if (!parent.Rotate)
                        {
                            child.PointNullX = parent.PointNullX - parent.PanelWidth / 2 - rectangle.Width / 2;
                            rectangle.X = parent.PointNullX - parent.PanelWidth / 2 - rectangle.Width;
                            rectangle.Y = parent.PointNullY - parent.PanelHeight / 2 - rectangle.Height / 2 + child.HingeOffset;
                        }
                        else
                        {
                            child.PointNullX = parent.PointNullX - parent.PanelHeight / 2 - rectangle.Width / 2;
                            rectangle.X = parent.PointNullX - parent.PanelHeight / 2 - rectangle.Width;
                            rectangle.Y = parent.PointNullY - parent.PanelWidth / 2 - rectangle.Height / 2 + child.HingeOffset;
                        }
                        break;
                    }
            }
            return rectangle;
        }
    }
}
