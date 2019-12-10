using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Neiron
    {
        public List<double> Ws { get; }
        public int type;
        public double Output {get; private set;}
        public Neiron(int input, int type)
        {
            this.type = type;
            Ws = new List<double>();

            for (int i = 0; i < input; i++)
            {
                Ws.Add(1);
            }
        }

        public double FeedForward (List<double> inputs)
        {
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
    }
}
