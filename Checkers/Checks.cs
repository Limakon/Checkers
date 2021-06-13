using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Checkers
{
    public class Checks
    {
        ArrayList array;
        public Checks(CheckersColors color, Canvas gameField)
        {
            array = new ArrayList(12);

            if(color == CheckersColors.White)
            {

            }
            else
            {

            }
        }

        public Checks this[int n]
        {
            get => (Checks)array[n];
        }

        public int Count
        {
            get => array.Count;
        }
    }
}
