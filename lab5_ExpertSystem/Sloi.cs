using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Sloi
    {
        public List<Neiron> neirons { get; }
        public int Count => neirons.Count;

        public Sloi (List<Neiron> _neirons, int type)
        {
            neirons = _neirons;
        }

        public List<double> GetSignals()
        {
            var result = new List<double>();
            foreach(var neuron in neirons)
            {
                result.Add(neuron.Output);
            }
            return result;
        }
    }
}