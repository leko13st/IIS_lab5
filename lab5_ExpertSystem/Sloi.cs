using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Sloi
    {
        public static double Sum(double[] Ws, double[] X)
        {
            double sum = 0;
            sum = 0;
            for (int i = 0; i < Ws.Length; i++)
            {
                sum += Ws[i] * X[i];
            }
            return sum;
        }

        public static double CalculateY(double p)
        {
            double y = 0;
            y = 1 / (1 + Math.Exp(-p));
            return y;
        }

        public static string An (List<Neiron> Neirons, List<double> input)
        {
            string answer = null;
            for (int i = 0; i < Neirons.Count; i++)
            {
                Neirons[i].ans = i.ToString();
                Neirons[i].p = Sum(Neirons[i].Ws, input.ToArray());
                Neirons[i].y = CalculateY(Neirons[i].p);
            }
            double max = Neirons[0].y;
            for (int i = 0; i < Neirons.Count; i++)
            {
                if (Neirons[i].y >= max)
                {
                    max = Neirons[i].y;
                    answer = Neirons[i].ans;
                }
            }
            return answer;
        }
    }
}
