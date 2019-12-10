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

        public Neiron FeedForvard(List<double> inputSignals)
        {
            for (int i = 0; i < inputSignals.Count; i++)
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
    }
}
