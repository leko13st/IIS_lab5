using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Network
    {
        public List<Sloi> slois { get; } //массив слоёв
        public Opisanie Opisanie { get; } //класс, отвечающий за параметры нейронной сети
        public Network(Opisanie opisanie)
        {
            Opisanie = opisanie;
         
            slois = new List<Sloi>(); //создаём массив слоёв
            Createinput(); //создаём слой входных нейронов
            CreateSloi(); //и скрытые слои
            Createoutput(); //слой выходных нейронов
        }

        public void Createinput() //метод создания входного слоя
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

        public void Createoutput() //метод создания выходного слоя
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

        public void CreateSloi() //метод создания скрытых (промежуточных) слоёв
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


        public Neiron FeedForvard(params double[] inputSignals) //метод подсчёта значения нейросети
        {
            for (int i = 0; i < inputSignals.Length; i++) //подсчёт для входного слоя
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = slois[0].neirons[i];

                neuron.FeedForward(signal); 
            }

            for (int i = 1; i < slois.Count; i++) //подсчёт для остальных слоёв
            {
                var predSloi = slois[i - 1].GetSignals();
                var sloi = slois[i];

                foreach (var neuron in sloi.neirons)
                {
                    neuron.FeedForward(predSloi);
                }
            }

            return slois.Last().neirons.OrderByDescending(n => n.Output).First(); //выбор нейрона с наибольшим выходным значением
        }

        public double Learn(List <Tuple<string, double[]>> dataset, int k) //метод обучения
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

        private double MethodORO (string s, params double[] inputs) //метод обратного распространения ошибки
        {
            var y = 0;
            var yy = FeedForvard(inputs);
            double delt = 0;
            int k1 = 0;
            foreach (var neuron in slois.Last().neirons) //пересчитываем значения весов для последнего слоя (выходного)
            {
                if (neuron.name == s) y = 1;
                else y = 0;
                var dif = neuron.Output - y; //вычисляем отклонение от требуемого значения
                delt += dif;
                k1++;
                neuron.Obuchenie(dif, Opisanie.n);//пересчитываем веса для нейрона
            }
            delt = delt / k1;
            for (int j = slois.Count - 2; j >= 0; j--) //для остальных слоёв
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
