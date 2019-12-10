using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_ExpertSystem
{
    class Opisanie
    {
        public int inputCount { get; }
        public int outputCount { get; }
        public List<int> sloiCount { get; }
        public Opisanie(int input, int output, params int[] slois)
        {
            inputCount = input;
            outputCount = output;
            sloiCount = new List<int>();
            sloiCount.AddRange(slois);
        }
    }
}
