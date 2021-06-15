using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Node
    {
        Node Root;

        Node Parent;

        Cell Position;

        Checker Killed;

        public int dir;

        GameField GameField;

        public ArrayList List;

        public int Count;

        public Node(Node Root, GameField GameField)
        {
            this.Root = Root;
            this.GameField = GameField;

            List = new ArrayList(12);

            Count = 0;
        }

        public Node(Node Parent, Cell Position, Checker Killed, int dir)
        {
            this.Parent = Parent;
            this.Position = Position;
            this.Killed = Killed;
            this.dir = dir;
        }
        
        public ArrayList Up(Node list)
        {
            ArrayList array = new ArrayList(6);

            Node current = list;

            array.Add(current);

            while (current.Parent != null)
            {
                current = current.Parent;
                array.Add(current);
            }

            return array;
        }


        public void AddSteps(Checker checker, ref Step steps)
        {
            for (int i = 0; i < List.Count; i++)
            {
                Node node = (Node)List[i];

                Step step = new Step(checker, node.Position);

                ArrayList array = Up(node);

                for (int j = 0; j < array.Count; j++)
                {
                    step.Kills.Add(((Node)array[j]).Killed);
                }

                steps.Add(step);

                Cell[] Addition = node.Position.NearDame(node.dir, GameField);

                for (int k = 1; k < Addition.Count(); k++)
                {
                    Step _step = new Step(checker, Addition[k]);

                    for (int j = 0; j < step.Kills.Count; j++)
                    {
                        _step.Kills.Add(step.Kills[j]);
                    }

                    steps.Add(_step);
                }
            }
        }
    }
}