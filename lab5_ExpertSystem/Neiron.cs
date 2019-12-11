using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Neiron
    {
        Random r = new Random();
        public List<double> Ws { get; } //массив весов
        public int type; //тип слоя (1-входной, 2-скрытыйб 3-выходной)
        public string name; //цифра, за которую отвечает нейрон
        public double delta { get; private set; } //ошибка
        public List<double> Inputs { get; } //массив входных данных
        public double Output {get; private set;} //массив выходных данных
        public Neiron(int input, int type) 
        {
            this.type = type;
            Ws = new List<double>();
            Inputs = new List<double>();

            for (int i = 0; i < input; i++)
            {
                if (type == 1)
                {
                    Ws.Add(1); //у входного слоя веса равны 1
                }
                else
                Ws.Add(r.NextDouble()); //остальные рандомно задаём
                Inputs.Add(0);
            }
        }

        public double FeedForward (List<double> inputs) //метод подсчёта значений нейрона
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                Inputs[i] = inputs[i];
            }
            var sum = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Ws[i];
            }
            if (type != 1)
            {
                Output = Sigmoid(sum); //функция у
            }
            else
            {
                Output = sum;
            }

            return Output;
        }

        private double Sigmoid (double x) //
        {
            double Sigm = 1 / (1 + Math.Exp(-x));
            return Sigm;
        }

        private double SigmoidDx (double x) //произаодная от функции
        {
            var Sigm = Sigmoid(x);
            var result = Sigm / (1 - Sigm);
            return result;
        }

        public void Obuchenie (double error, double n) //метод пересчёта весов нейрона (подаём ошибку и скорость обучения)
        {
            if (type == 1)
            {
                return;
            }
            var d = error * SigmoidDx(Output);
            for (int i = 0; i < Ws.Count; i++)
            {
                var ws = Ws[i];
                var input = Inputs[i];

                var newWs = ws - input * d * n;
                Ws[i] = newWs;
            }
            delta = d;
        }
    }
}
