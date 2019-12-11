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
        public List<double> Ws { get; }
        public int type;
        public string name;
        public double delta { get; private set; }
        public List<double> Inputs { get; }
        public double Output {get; private set;}
        public Neiron(int input, int type)
        {
            this.type = type;
            Ws = new List<double>();
            Inputs = new List<double>();

            for (int i = 0; i < input; i++)
            {
                if (type == 1)
                {
                    Ws.Add(1);
                }
                else
                Ws.Add(r.NextDouble());
                Inputs.Add(0);
            }
        }

        public double FeedForward (List<double> inputs)
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
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }

            return Output;
        }

        private double Sigmoid (double x)
        {
            double Sigm = 1 / (1 + Math.Exp(-x));
            return Sigm;
        }

        private double SigmoidDx (double x)
        {
            var Sigm = Sigmoid(x);
            var result = Sigm / (1 - Sigm);
            return result;
        }

        public void Obuchenie (double error, double n)
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
