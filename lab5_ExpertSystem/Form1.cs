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
            bitmap = new Bitmap(150, 150); //создаём область рисования
            opisanie = new Opisanie(100, 2, 0.01, 16); //задаём параметры нейросети
            newNetwork = new Network(opisanie); //создаём нейросеть
        }

        Opisanie opisanie;
        Network newNetwork;
        Bitmap bitmap;
        int x1, y1;
        string answer = null;
        List<double> input = new List<double>(); //массив входных данных
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e) //распознавание рисунка
        {
            listBox1.Items.Clear();
            input.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    listBox1.Items.Add("");
                    int n = (bitmap.GetPixel(j*15, i*15).ToArgb()); //сжимаем картинку и определяем закрашен ли пиксель
                    if (n >= -1) n = 0;
                    else n = 1;
                    input.Add(n);//добавляем в массив входные данные
                    listBox1.Items[i] = listBox1.Items[i] + " " + Convert.ToString(n);
                }
            }
            answer = newNetwork.FeedForvard(input.ToArray()).name; //распознаём
            label1.Text = "Это цифра " + answer.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //очищаем рисунок
        {
            Graphics.FromImage(bitmap).Clear(Color.White);
            
            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e)//обучаем
        {
            var dataset = new List<Tuple<string, double[]>> //обучающая выборка (оч. маленькая)
            {
                new Tuple<string, double[]>("0", new double[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }),
                new Tuple<string, double[]>("0", new double[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }),
                new Tuple<string, double[]>("0", new double[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }),
                new Tuple<string, double[]>("1", new double[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 }),
                new Tuple<string, double[]>("1", new double[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 })
            };
            var res = newNetwork.Learn(dataset, 100); //обучаем (массив данных + количество проходов по массиву)
            textBox1.Text = res.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //метод отрисовки
        {
            Pen p;
            p = new Pen(Color.Red, 30);
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
