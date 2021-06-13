using System;
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

        public Checker(Canvas canvas, CheckersColors Color, Cell Position)
        {
            this.canvas = canvas;
            this.Color = Color;
            this.Position = Position;
            checker = new Ellipse[8];
            checker[0] = null;
            checker[1] = null;
        }

        public void SetPosition(Cell Position)
        {
            double width = canvas.Width;
            double L8 = width / 8.0;
            double kappa = 0.8;

            this.Position.Set(Position);

            if(((Position.n == 1) && (Color == CheckersColors.Black) || (Position.n == 8) && (Color == CheckersColors.White)))
            {
                Dame = true;

                checker[1] = new Ellipse();
                checker[1].Stroke = Brushes.Red;
                checker[1].StrokeThickness = 3;
                checker[1].Width = kappa * L8 / 2;
                checker[1].Height = kappa * L8 / 2;
                canvas.Children.Add(checker[1]);
            }

            checker[1].Margin = new Thickness(Position.i * L8 + L8 * (1 - kappa) / 2, Position.j * L8 + L8 * (1 - kappa) / 2, 0, 0);
        }
    }
}
