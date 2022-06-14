using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;


namespace Lab_7
{
    public partial class Form1 : Form
    {
        BezierCurve bezier;
        Marker[] markers = new Marker[4];
        public Form1()
        {
            InitializeComponent();
            markers[0] = new Marker(100, 200);
            markers[1] = new Marker(150, 250);
            markers[2] = new Marker(200, 150);
            markers[3] = new Marker(250, 200);
            for (int index = 0; index < markers.Length; index++)
            {
                Marker marker = markers[index];
                int i = index;
                marker.OnDrag += f =>
                {
                    bezier[i] = f;
                    pictureBox1.Invalidate();
                };
                marker.OnMouseDown += f => { Cursor = Cursors.Hand; };
            }

            bezier = new BezierCurve(markers.Select(m => m.Location).ToArray());
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Gray, 1f);
            e.Graphics.DrawLines(pen, markers.Select(m => m.Location).ToArray());
            foreach (Marker marker in markers)
            {
                marker.Draw(e.Graphics);
            }
            Pen windowsFunctionPen = new Pen(Color.Red, 2f);
            if (checkBox1.Checked) e.Graphics.DrawBezier(windowsFunctionPen, markers[0].Location, markers[1].Location, markers[2].Location, markers[3].Location);
            else bezier.Draw(e.Graphics);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.BackColor == System.Drawing.SystemColors.Highlight)
            {
                checkBox1.Location = new System.Drawing.Point(241, 621);
                checkBox1.BackColor = Color.Red;
            }
            else
            {
                checkBox1.Location = new System.Drawing.Point(266, 621);
                checkBox1.BackColor = System.Drawing.SystemColors.Highlight;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Marker marker in markers)
            {
                marker.MouseDown(e);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (Marker marker in markers)
                {
                    marker.MouseMove(e);
                    Thread.Sleep(0);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Marker marker in markers)
            {
                marker.MouseUp();
            }
            Cursor = Cursors.Arrow;
        }
    }
}
