using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animated
{
    public partial class Form1 : Form
    {
        Point ratioLine;
        Point[] positions;
        int positionIndex = 0;
        int fibon1, fibon2;
        Rectangle container;
        string writeString = "";
        int n;
        public Form1()
        {
            InitializeComponent();
        }

        private Point[] Slide(Point start, Point end, int steps)
        {
            Point[] output = new Point[steps];
            int distX, distY;

            distX = end.X - start.X; distY = end.Y - start.Y;
            for (int i = 0; i < steps; i++)
            {
                int x, y;
                double t = i / Convert.ToDouble(steps);

                x = Convert.ToInt32(start.X + -Math.Cos(Math.PI * t) * (distX / 2) + (distX / 2));
                y = Convert.ToInt32(start.Y + -Math.Cos(Math.PI * t) * (distY / 2) + (distY / 2));

                output[i] = new Point(x, y);
            }
            return output;
        }

        private void TIM_main_Tick(object sender, EventArgs e)
        {
            GetNextFibon(ref fibon1, ref fibon2);
            double ratio = Convert.ToDouble(fibon1) / (fibon1 + fibon2);
            writeString = "n = " + n.ToString() + ", " + (n - 1).ToString() + ": " + fibon1.ToString() + " & " + fibon2.ToString() + ", ratio = " + ratio.ToString()
                + "\nphi = 0.61803398875";
            Point next = new Point(Convert.ToInt32(container.Width * ratio + container.Left), container.Top);
            positions = Slide(ratioLine, next, 100);
            positionIndex = 0;
            TIM_animate.Enabled = true;
            n++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fibon1 = 1; fibon2 = 1;
            container = new Rectangle(100, 100, PB_main.Width - 200, PB_main.Height - 200);
            ratioLine = new Point(PB_main.Width / 2, container.Top);
            n = 2;
            writeString = "n = " + n.ToString() + ", " + (n - 1).ToString() + ": " + fibon1.ToString() + " & " + fibon2.ToString() + ", ratio = 0.5"
                + "\nphi = 0.61803398875";
        }

        private void TIM_animate_Tick(object sender, EventArgs e)
        {
            ratioLine = positions[positionIndex];
            positionIndex++;
            if (positionIndex >= positions.Length)
            {
                TIM_animate.Enabled = false;
            }
            PB_main.Refresh();
        }

        private void PB_main_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(writeString, SystemFonts.DefaultFont, Brushes.White, 10, 10);
            e.Graphics.DrawRectangle(Pens.White, container);
            e.Graphics.DrawLine(Pens.White, ratioLine, new Point(ratioLine.X, container.Bottom));
        }

        private void GetNextFibon(ref int f1, ref int f2)
        {
            int temp = f1;
            f1 = f1 + f2;
            f2 = temp;
        }
    }
}
