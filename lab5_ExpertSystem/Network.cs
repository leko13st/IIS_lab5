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

        public void Createinput()
        {
            var neirons = new List<Neiron>();
            for (int i = 0; i < Opisanie.inputCount; i++)
            {
                var neuron = new Neiron(1, 1);
                neirons.Add(neuron);
            }
            var sloi = new Sloi(neirons, 1);
            slois.Add(sloi);
        }

        public void Createoutput()
        {
            var neirons = new List<Neiron>();
            var lastSloi = slois.Last();
            for (int i = 0; i < Opisanie.outputCount; i++)
            {
                var neuron = new Neiron(lastSloi.Count, 3);
                neuron.name = i.ToString();
                neirons.Add(neuron);
            }
            var sloi = new Sloi(neirons, 3);
            slois.Add(sloi);
        }

        public void CreateSloi()
        {
            for (int j = 0; j < Opisanie.sloiCount.Count; j++)
            {
                var neirons = new List<Neiron>();
                var lastSloi = slois.Last();
                for (int i = 0; i < Opisanie.sloiCount[j]; i++)
                {
                    var neuron = new Neiron(lastSloi.Count, 2);
                    neirons.Add(neuron);
                }
                var sloi = new Sloi(neirons, 2);
                slois.Add(sloi);
            }
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
