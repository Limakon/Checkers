using System;
using System.Collections;
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
using System.Windows.Threading;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameField field;
        Rand rand;

        public MainWindow()
        {
            InitializeComponent();

            field = new GameField(gameField);

            rand = new Rand(64); 
        }

        private void cmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        bool isWhite = true;
        bool isGame;

        DispatcherTimer timer;
        private void cmAutoRun(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(onTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            isGame = true;

            timer.Start();
        }

        void onTick(object sender, EventArgs e)
        {
            if (isGame)
            {
                cmRun(null, null);
            }
            else
            {
                timer.Stop();
            }
        }

        private void cmRun(object sender, RoutedEventArgs e)
        {
            if (isWhite)
            {
                RunCheckers(field.WhiteChecks);
            }
            else
            {
                RunCheckers(field.BlackChecks);
            }
        }

        void RunCheckers(Checker checker)
        {
            Step steps;
            Step stepsKill;

            rand.Clear();

            for(int i = 0; i < checker.Count; i++)
            {
                stepsKill = checker[i].GetStepsKill(field);

                if(stepsKill.Count > 0)
                {
                    rand.Add(i);
                }
            }

            if(rand.Count > 0)
            {
                checker[rand.Get].Run(field, null);
                return;
            }

            rand.Clear();

            for(int i = 0; i < checker.Count; i++)
            {
                steps = checker[i].GetSteps(field);

                if(steps.Count > 0)
                {
                    rand.Add(i);
                }
            }

            if (rand.Count > 0)
            {
                checker[rand.Get].Run(field, null);
            }
            else
            {
                isGame = false;

                CheckersColors colors;
                if (isWhite)
                {
                    colors = CheckersColors.White;
                }
                else
                {
                    colors = CheckersColors.Black;
                }

                field.GameOver(colors);

                field = new GameField(gameField);
            }
        }

        Checker checkers = null;

        private void cmClick(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(gameField);

            Cell cell = field.GetCell(point);

            if(checkers == null)
            {
                field.CheckRole(cell, out checkers);

                if(checkers == null)
                {
                    return;
                }

                if(checkers.Color != CheckersColors.White)
                {
                    checkers = null;
                    return;
                }
            }
            else
            {
                ArrayList array = new ArrayList();

                Step step = new Step(checkers, cell);

                Step steps;
                Step stepsKill;

                for(int i =0; i < field.WhiteChecks.Count; i++)
                {
                    stepsKill = field.WhiteChecks[i].GetStepsKill(field);

                    for (int k = 0; k < stepsKill.Count; k++)
                    {
                        array.Add(stepsKill[k]);
                    }
                }

                if (array.Count > 0)
                {
                    for (int k = 0; k < array.Count; k++)
                    {
                        Step _step = (Step)array[k];

                        if ((step.Checker.Position.Equal(_step.Checker.Position) && (step.Position.Equal(_step.Position))))
                        {
                            isWhite = !isWhite;

                            cmRun(null, null);
                            checkers = null;

                            return;
                        }
                    }
                    checkers = null;
                    return;
                }
                else
                {
                    array.Clear();

                    for (int i = 0; i < field.WhiteChecks.Count; i++)
                    {
                        steps = field.WhiteChecks[i].GetSteps(field);

                        for(int k = 0; k < steps.Count; k++)
                        {
                            array.Add(steps[k]);
                        }
                    }

                    if (array.Count > 0)
                    {
                        for (int k = 0; k < array.Count; k++)
                        {
                            Step _step = (Step)array[k];

                            if((step.Checker.Position.Equal(_step.Checker.Position) &&
                                (step.Position.Equal(_step.Position))))
                            {
                                _step.Checker.Run(field, _step);

                                isWhite = !isWhite;

                                cmRun(null, null);
                                checkers = null;
                                return;
                            }
                        }

                        checkers = null;
                        return;
                    }
                    else
                    {
                        isGame = false;

                        CheckersColors colors;

                        if (isWhite)
                        {
                            colors = CheckersColors.White;
                        }
                        else
                        {
                            colors = CheckersColors.Black;
                        }

                        field.GameOver(colors);

                        field = new GameField(gameField);
                    }
                }
            }
        }
    }
}
