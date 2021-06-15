using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Checkers
{
    public class Checker
    {
        GameField gameField;

        public CheckersColors Color; // цвет шашки

        public Cell Position;

        public bool Dame = false;

        public Ellipse[] checker;

        Canvas canvas;

        Node nodeTree;

        ArrayList array;

        public int Count
        {
            get => array.Count;
        }

        public Checker(Canvas canvas, CheckersColors Color, Cell Position)
        {
            this.canvas = canvas;
            this.Color = Color;
            this.Position = Position;
            checker = new Ellipse[8];
            checker[0] = null;
            checker[1] = null;
        }
        
        // отрисовка шашек
        public Checker(CheckersColors color, Canvas gameField)
        {
            array = new ArrayList(12);

            if (color == CheckersColors.White)
            {
                
            }
            else
            {

            }
        }

        public Checker this[int n]
        {
            get => (Checker)array[n];
        }

        public void SetPosition(Cell Position)
        {
            double width = canvas.Width;
            double L8 = width / 8.0;
            double coefficient = 0.8;

            this.Position.Set(Position);

            if(((Position.n == 1) && (Color == CheckersColors.Black) || (Position.n == 8) && (Color == CheckersColors.White)))
            {
                Dame = true;

                checker[1] = new Ellipse();
                checker[1].Stroke = Brushes.Red;
                checker[1].StrokeThickness = 3;
                checker[1].Width = coefficient * L8 / 2;
                checker[1].Height = coefficient * L8 / 2;
                canvas.Children.Add(checker[1]);
            }

            checker[0].Margin = new Thickness(Position.i * L8 + L8 * (1 - coefficient) / 2, Position.j * L8 + L8 * (1 - coefficient) / 2, 0, 0);

            if(checker[1] != null)
            {
                checker[1].Margin = new Thickness(Position.i * L8 + (L8 - checker[1].Width) / 2, Position.j * L8 + (L8 - checker[1].Height) / 2, 0, 0);
            }
        }

        public void Delete()
        {
            canvas.Children.Remove(checker[0]);
            canvas.Children.Remove(checker[1]);
        }

        public void AddStep(Step step, int dir)
        {
            Cell cell;
            Checker checker1;
            Checker checker2;
            Step step1;

            cell = Position.Near(dir);
            if(gameField.CheckRole(cell, out checker1))
            {
                step.Add(new Step(this, Position));
            }
            else
            {
                if(checker1.Color != Color)
                {
                    Position = checker1.Position.Near(dir);
                    if (gameField.CheckRole(Position, out checker2))
                    {
                        if (checker2 == null)
                        {
                            step1 = new Step(this, Position);
                            step1.Kills.Add(checker1);
                            step.Add(step1);
                        }
                    }
                }
            }
        }

        public void AddStepDame(Step step, int dir)
        {
            Cell[] cells = Position.NearDame(dir, gameField);
            for (int i = 0; i < cells.Count(); i++)
            {
                step.Add(new Step(this, cells[i]));
            }
        }

        public void AddStepKillDame(Cell startPosition, int dir, Node paranet)
        {
            ArrayList array = new ArrayList();

            Cell cell;

            if(startPosition == null)
            {
                cell = Position.NearDames(dir, gameField);
            }
            else
            {
                cell = startPosition.NearDames(dir, gameField);
            }

            Checker role;

            gameField.CheckRole(cell, out role);

            if (role == null)
            {
                return;
            }
            if(role.Color == Color)
            {
                return;
            }

            cell = cell.Near(dir);

            Checker role2;

            if (gameField.CheckRole(cell, out role2))
            {
                if(role2 == null)
                {
                    Node current = new Node(paranet, cell, role, dir);

                    if(nodeTree == null)
                    {
                        nodeTree = new Node(current, gameField);
                    }
                    nodeTree.Count++;

                    int Count = nodeTree.Count;

                    Cell[] cells;

                    for(int i = 1; i <= 4; i++)
                    {
                        if (((dir == 1) && (i == 4)) || ((dir == 2) && (i == 3)) ||
                            ((dir == 3) && (i == 2)) || ((dir == 4) && (i == 1)))
                        {
                            continue;
                        }

                        cells = cell.NearDame(dir, gameField);

                        for(int k = 0; k < cells.Count(); k++)
                        {
                            AddStepKillDame(cells[k], i, current);
                        }
                    }

                    if(Count == nodeTree.Count)
                    {
                        nodeTree.List.Add(current);
                    }
                }
            }
        }

        public void AddStepKill(Cell startPosition, int dir, Node Parent)
        {
            Cell cell;
            Checker role;
            Checker role1;

            if(startPosition == null)
            {
                cell = Position.Near(dir);
            }
            else
            {
                cell = startPosition.Near(dir);
            }

            if (gameField.CheckRole(cell, out role))
            {
                if(role == null)
                {
                    return;
                }
                else
                {
                    if(role.Color != Color)
                    {
                        cell = role.Position.Near(dir);

                        if (gameField.CheckRole(cell, out role1))
                        {
                            Node current = new Node(Parent, cell, role, dir);

                            if(nodeTree == null)
                            {
                                nodeTree = new Node(current, gameField);
                            }
                            nodeTree.Count++;

                            int Count = nodeTree.Count;

                            for (int i = 1; i <= 4; i++)
                            {

                                if (((dir == 1) && (i == 4)) || ((dir == 2) && (i == 3)) ||
                                    ((dir == 3) && (i == 2)) || ((dir == 4) && (i == 1)))
                                {
                                    continue;
                                }

                                AddStepKill(cell, i, current);
                            }

                            if (Count == nodeTree.Count)
                            {
                                nodeTree.List.Add(current);
                            }
                        }
                    }
                }
            }
        }
    }
}
