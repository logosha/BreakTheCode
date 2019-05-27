using BreakTheCode.Interfaces;
using BreakTheCode.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BreakTheCode
{
    public partial class MainForm : Form
    {
        string projectDirectory = Directory.
                                  GetParent(Directory.
                                            GetParent(Application.StartupPath).
                                            FullName).
                                  FullName;

        Bitmap bitmap;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IDrawer drawer;
            Model model;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = projectDirectory + @"\Source",
                Filter = "xml files|*.xml",
                DefaultExt = "xml"
            };

            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IReader<Model> reader = new Reader.Reader();
                    model = reader.Read(openFileDialog.FileName);

                    pictureBox.Width = model.OriginalDocumentWidth;
                    pictureBox.Height = model.OriginalDocumentHeight;

                    bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

                    drawer = new Drawer.Drawer(model, Graphics.FromImage(bitmap));
                    drawer.Paint();

                    pictureBox.Image = bitmap;

                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not readed: " + ex.Message);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = projectDirectory,
                Filter = "jpg files|*.jpg",
                DefaultExt = "jpg"
            };

            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.DrawToBitmap(bitmap, pictureBox.ClientRectangle);
                    bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File not saved: " + ex.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            bitmap?.Dispose();
        }
    }
}
