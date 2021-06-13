using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Checkers
{
    public class GameField
    {
        Canvas gameField;

        public Checks WhiteChecks, BlackChecks;
            
        public GameField(Canvas gameField)
        {
            this.gameField = gameField;

            DrawCell();
            DrawCheckers();
        }

        public void DrawCheckers()
        {

        }

        public void DrawCell()
        {

        }

        public bool CheckRole(Cell position, out Checks role)
        {
            role = null;

            if(position == null)
            {
                return false;
            }

            for(int k = 0; k < WhiteChecks.Count; k++)
            {
                
            }
            return false;
        }
    }
}
