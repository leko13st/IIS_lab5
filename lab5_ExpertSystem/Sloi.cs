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
        public List<Neiron> neirons { get; } //массив нейронов
        public int Count => neirons.Count; //кол-во нейронов в слое

        public Sloi (List<Neiron> _neirons, int type)
        {
            neirons = _neirons;
        }

        public List<double> GetSignals() //получение тейронов предыдущего слоя
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