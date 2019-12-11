using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Opisanie
    {
        public int inputCount { get; } //кол-во нейронов во входном слое
        public int outputCount { get; } //кол-во нейронов в выходном слое
        public double n { get; } //скорость обучения
        public List<int> sloiCount { get; } //кол-во слоёв в скрытом слое
        public Opisanie(int input, int output, double _n, params int[] slois)
        {
            inputCount = input;
            outputCount = output;
            n = _n;
            sloiCount = new List<int>();
            sloiCount.AddRange(slois);
        }
    }
}
