using Saper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    interface IModel
    {
        bool CheckedWin();
        void NextStateOfSquare(int i, int j);
        //zwraca false jestli w polu byla bomba
        bool RevealSquare(int i, int j);
        Square GetSquare(int i, int j);

        int Width { get; set; }
        int Height { get; set; }
        int NumberOfMines { get; set; }
        bool IsFinished { get; }
        void InitGame();
    }
}
