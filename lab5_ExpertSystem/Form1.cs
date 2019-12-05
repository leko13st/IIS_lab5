using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5_ExpertSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(150, 150);
        }
        List<Point> points = new List<Point>();
        Bitmap bitmap;
        int x1, y1;
        string answer = null;
        public int[,] X = new int[15, 15];
        List<double> input = new List<double>();
        List<Neiron> Neirons = new List<Neiron>();
        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader st = File.OpenText(@"D:\Для учёбы\7 семестр\Истомин\Лаба 5\lab5_ExpertSystem\TextFile1.txt");
            string line;
            string[] s1;
            int k = 0;
            while ((line = st.ReadLine()) != null)
            {
                s1 = line.Split(' ');
                Neirons.Add(new Neiron());
                for (int i = 0; i < s1.Length; i++)
                {
                   Neirons[k].Ws[i] = Convert.ToDouble(s1[i]);
                }
                k++;
            }
            st.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            input.Clear();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    listBox1.Items.Add("");
                    int n = (bitmap.GetPixel(i*10, j*10).ToArgb());
                    if (n >= -1) n = 0;
                    else n = 1;
                    input.Add(n);
                    listBox1.Items[j] = listBox1.Items[j] + " " + Convert.ToString(n);
                    X[i, j] = n;
                }
            }
            answer = Sloi.An(Neirons, input);
            label1.Text = "Это цифра " + answer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics.FromImage(bitmap).Clear(Color.White);
            
            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen p;
            p = new Pen(Color.Red, 15);
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics g = Graphics.FromImage(bitmap);
            if (e.Button == MouseButtons.Left)
            {
           //     points.Add(new Point(e.X, e.Y));
                g.DrawLine(p, x1, y1, e.X, e.Y);
                pictureBox1.Image = bitmap;
            }
            x1 = e.X;
            y1 = e.Y;
        }
    }
}
