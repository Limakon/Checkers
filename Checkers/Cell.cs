using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Cell
    {
        public int i, j; // координаты клетки
        public Notes Note; // буква на поле

        public int n;

        public Cell(int i, int j)
        {
            this.i = i;
            this.j = j;

            n = i + 1;
            Note = (Notes)j;
        }

        public Cell(Notes Note, int n)
        {
            this.Note = Note;
            this.n = n;

            i = (int)Note;

            j = 9 - n;
        }

        public void Set(Cell position)
        {
            this.i = position.i;
            this.j = position.j;
            this.Note = position.Note;
            this.n = position.n;
        }

        public bool Equal(Cell position)
        {
            if((Note == position.Note) && (n == position.n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cell[] NearDame(int dir, GameField gameField)
        {
            ArrayList array = new ArrayList(8);

            Notes _note = Note;
            int _n = n;

            array.Add(new Cell(_note, n));

            Checks role;

            if (dir == 1)
            {
                while((_note != Notes.A) && (_n != 8))
                {
                    _note = _note - 1;
                    _n = _n + 1;
                }
            }
        }

    }
}
