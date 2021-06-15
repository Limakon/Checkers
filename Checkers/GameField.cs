using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace Checkers
{
    public class GameField
    {
        Canvas gameField;

        public Checker WhiteChecks, BlackChecks;

        public GameField(Canvas gameField)
        {
            this.gameField = gameField;

            DrawCell();

            WhiteChecks = new Checker(CheckersColors.White, gameField);
            BlackChecks = new Checker(CheckersColors.Black, gameField);

            DrawCheckers();
        }

        public void GameOver(CheckersColors color)
        {
            string message;
            if(color == CheckersColors.White)
            {
                message = "Черные";
            }
            else
            {
                message = "Белые";
            }

            MessageBox.Show(message + " выиграли!");
        }

        public Cell GetCell(Point point)
        {
            double L = gameField.Width;
            double L8 = L / 8.0;

            double x = point.X;
            double y = point.Y;

            if ((x < 0) || (x > L) || (y < 0) || (y > L))
            {
                MessageBox.Show("Выбери шашку!");
            }

            int a = Convert.ToInt16(Math.Ceiling(x / L8));

            Notes note = (Notes)(a - 1);

            a = Convert.ToInt16(Math.Ceiling(y / L8));

            int n = 9 - a;

            return new Cell(note, n);
        }

        Ellipse DrawChecker(Cell position, CheckersColors color)
        {
            double L = gameField.Width;
            double L8 = L / 8.0;
            double coefficient = 0.8;

            Ellipse checker = new Ellipse();

            if (color == CheckersColors.White)
            {
                checker.Fill = Brushes.White;
            }
            else
            {
                checker.Fill = Brushes.Black;
            }
            checker.Width = coefficient * L8;
            checker.Height = coefficient * L8;
            checker.Margin = new Thickness(position.i * L8 + L8 * (1 - coefficient) / 2, position.j * L8 + L8 * (1 - coefficient) / 2, 0, 0);

            gameField.Children.Add(checker);
            return checker;
        }

        public void DrawCheckers()
        {
            for (int k = 0; k < WhiteChecks.Count; k++)
            {
                WhiteChecks[k].checker[0] = DrawChecker(WhiteChecks[k].Position, CheckersColors.White);
            }
            for(int k = 0; k < BlackChecks.Count; k++)
            {
                BlackChecks[k].checker[0] = DrawChecker(BlackChecks[k].Position, CheckersColors.Black);
            }
        }

        public void DrawCell()
        {
            double L = gameField.Width;
            double L8 = L / 8.0;

            bool White = false;

            for(int i = 0; i < 8; i++)
            {
                White = !White;

                for(int j = 0; j < 8; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    if (White)
                    {
                        rectangle.Fill = Brushes.White;
                        White = false;
                    }
                    else
                    {
                        rectangle.Fill = Brushes.Brown;
                        White = true;
                    }

                    rectangle.Width = L8;
                    rectangle.Height = L8;
                    rectangle.Margin = new Thickness(i * L8, j * L8, 0, 0);
                    gameField.Children.Add(rectangle);
                }
            }

            Rectangle rectangle1 = new Rectangle();
            rectangle1.Width = L;
            rectangle1.Height = L;
            rectangle1.Stroke = Brushes.Black;
            rectangle1.Margin = new Thickness(0);
            gameField.Children.Add(rectangle1);
        }

        public bool CheckRole(Cell position, out Checker role)
        {
            role = null;

            if(position == null)
            {
                return false;
            }

            for(int k = 0; k < WhiteChecks.Count; k++)
            {
                if (WhiteChecks[k].Position.Equal(position))
                {
                    role = WhiteChecks[k];
                    return true;
                }
            }

            for (int k = 0; k < BlackChecks.Count; k++)
            {
                if (BlackChecks[k].Position.Equal(position))
                {
                    role = BlackChecks[k];
                    return true;
                }
            }
            return true;
        }
    }
}
