using Saper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.View
{
    interface IView
    {
        event Action<int, int> ChangeStateSquar;
        event Action<int, int> RevealSquar;
        event Action<int, int, int> StartGame;
        void Run();
        void SetSquareData(int i, int j, Square square);
        void Update();
        void Lost();
        void RunGame(int width, int height, int numbersOfMines);
        void Win();
    }
}
