using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceDetectionApp
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "JPEG|*.jpg" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pic.Image = Image.FromFile(openFileDialog.FileName);
                    Bitmap img = new Bitmap(pic.Image);
                    Image<Bgr, byte> grayImage = new Image<Bgr, byte>(img);
                    Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.4, 0);
                    foreach (Rectangle rectangle in rectangles)
                    {
                        using (Graphics gr = Graphics.FromImage(img))
                        {
                            using (Pen pen = new Pen(Color.Red, 1))
                            {
                                gr.DrawRectangle(pen, rectangle);
                            }
                        }
                    }
                    pic.Image = img;
                }
            }
        }
    }
}
