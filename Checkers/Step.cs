using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Step
    {
        public Checker Checker;
        public Cell Position;

        public ArrayList Kills;
        public ArrayList array;

        Random random;

        public Step(Checker Checker, Cell Position)
        {
            this.Checker = Checker;
            this.Position = Position;
            Kills = new ArrayList();
            
        }

        public Step()
        {
            array = new ArrayList();
            random = new Random();
        }

        public  Step GetStep()
        {
            if(Count < 1)
            {
                return null;
            }
            else
            {
                return this[random.Next(Count)];
            }
        }

        public void Add(Step step)
        {
            array.Add(step);
        }

        public int Count { get => array.Count; }

        public Step this[int i]
        {
            get => (Step)array[i];
            set { array[i] = value; }
        }
    }
}
