using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Rand
    {
        int[] A;
        int count;
        Random random;

        public Rand(int Max)
        {
            random = new Random();
            A = new int[Max];
            count = 0;
        }

        public void Clear()
        {
            count = 0;
        }

        public int Get
        {
            get
            {
                if(count < 1)
                {
                    return -1;
                }

                return A[random.Next(count)];
            }
        }
        public void Add(int a)
        {
            if(count == A.Count())
            {
                return;
            }

            A[count] = a;
            count++;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public int this[int index]
        {
            get
            {
                return A[index];
            }
        }
    }
}
