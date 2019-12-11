using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Network
    {
        public List<Sloi> slois { get; }
        public Opisanie Opisanie { get; }
        public Network(Opisanie opisanie)
        {
            Opisanie = opisanie;
         
            slois = new List<Sloi>();
            Createinput();
            CreateSloi();
            Createoutput();
        }

       

        public Neiron FeedForvard(params double[] inputSignals)
        {
            for (int i = 0; i < inputSignals.Length; i++)
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = slois[0].neirons[i];

                neuron.FeedForward(signal);
            }

            for (int i = 1; i < slois.Count; i++)
            {
                var predSloi = slois[i - 1].GetSignals();
                var sloi = slois[i];

                foreach (var neuron in sloi.neirons)
                {
                    neuron.FeedForward(predSloi);
                }
            }

            return slois.Last().neirons.OrderByDescending(n => n.Output).First();
        }

        public double Learn(List <Tuple<string, double[]>> dataset, int k)
        {
            var error = 0.0;
            for (int i = 0; i < k; i++)
            {
                foreach (var data in dataset)
                    error += MethodORO(data.Item1, data.Item2);
            }
            var result = error / k;
            return result;
        }

        private double MethodORO (string s, params double[] inputs)
        {
            var y = 0;
            var yy = FeedForvard(inputs);
            double delt = 0;
            int k1 = 0;
            foreach (var neuron in slois.Last().neirons)
            {
                if (neuron.name == s) y = 1;
                else y = 0;
                var dif = neuron.Output - y;
                delt += dif;
                k1++;
                neuron.Obuchenie(dif, Opisanie.n);
            }
            delt = delt / k1;
            for (int j = slois.Count - 2; j >= 0; j--)
            {
                var sloi = slois[j];
                var prsloi = slois[j + 1];

                for (int i = 0; i < sloi.Count; i++)
                {
                    var neuron = sloi.neirons[i];
                    for (int k = 0; k < prsloi.Count; k++)
                    {
                        var prneuron = prsloi.neirons[k];
                        var error = prneuron.Ws[i] * prneuron.delta;
                        neuron.Obuchenie(error, Opisanie.n);
                    }
                }
            }
            return delt*delt;
        }
    }
}
