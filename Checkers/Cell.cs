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

            j = 8 - n;
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

            Checker role;

            if (dir == 1)
            {
                while((_note != Notes.A) && (_n != 8))
                {
                    _note = _note - 1;
                    _n = _n + 1;

                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role == null)
                    {
                        array.Add(new Cell(_note, _n));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if(dir == 2)
            {
                while((_note != Notes.H) && (_n != 8))
                {
                    _note = _note + 1;
                    _n = _n + 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if(role == null)
                    {
                        array.Add(new Cell(_note, _n));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if(dir == 3)
            {
                while ((_note != Notes.A) && (_n != 1))
                {
                    _note = _note - 1;
                    _n = _n - 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role == null)
                    {
                        array.Add(new Cell(_note, _n));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (dir == 4)
            {
                while ((_note != Notes.H) && (_n != 1))
                {
                    _note = _note + 1;
                    _n = _n - 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role == null)
                    {
                        array.Add(new Cell(_note, _n));
                    }
                    else
                    {
                        break;
                    }
                }
            }


            Cell[] cells = null;
            if(array.Count > 0)
            {
                cells = new Cell[array.Count];

                for(int i = 0; i < array.Count; i++)
                {
                    cells[i] = (Cell)array[i];
                }
            }

            return cells;
        }

        public Cell NearDames(int dir, GameField gameField)
        {
            Notes _note = Note;
            int _n = n;
            Checker role;

            if(dir == 1)
            {
                while((_note != Notes.A) && (_n != 8))
                {
                    _note = _note - 1;
                    _n = _n + 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if(role != null)
                    {
                        return new Cell(_note, _n);
                    }
                }
            }

            if(dir == 2)
            {
                while ((_note != Notes.H) && (_n != 8))
                {
                    _note = _note + 1;
                    _n = _n + 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role != null)
                    {
                        return new Cell(_note, _n);
                    }
                }
            }

            if(dir == 3)
            {
                while ((_note != Notes.A) && (_n != 1))
                {
                    _note = _note - 1;
                    _n = _n - 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role != null)
                    {
                        return new Cell(_note, _n);
                    }
                }
            }

            if(dir == 4)
            {
                while ((_note != Notes.H) && (_n != 1))
                {
                    _note = _note + 1;
                    _n = _n - 1;
                    gameField.CheckRole(new Cell(_note, _n), out role);
                    if (role != null)
                    {
                        return new Cell(_note, _n);
                    }
                }
            }

            return null;
        }

        public Cell Near(int dir)
        {
            if(dir == 1)
            {
                if((Note == Notes.A) || (n == 8))
                {
                    return null;
                }
                return new Cell((Notes)Note - 1, n + 1);
            }
            if(dir == 2)
            {
                if((Note == Notes.H) || (n == 8))
                {
                    return null;
                }
                return new Cell((Notes)Note + 1, n + 1);
            }
            if (dir == 3)
            {
                if ((Note == Notes.A) || (n == 1))
                {
                    return null;
                }
                return new Cell((Notes)Note - 1, n - 1);
            }
            if (dir == 4)
            {
                if ((Note == Notes.H) || (n == 1))
                {
                    return null;
                }
                return new Cell((Notes)Note + 1, n - 1);
            }

            return null;
        }


        public string BoardValues()
        {
            return Note.ToString() + n.ToString();
        }
    }
}
