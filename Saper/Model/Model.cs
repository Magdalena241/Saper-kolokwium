using Saper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    class Model : IModel
    {
        Game game;
        public bool IsFinished { get; private set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumberOfMines { get; set; }

        public Model()
        {
            Width = 10;
            Height = 10;
            NumberOfMines = 10;
            IsFinished = false;
        }

        public Square GetSquare(int i, int j)
        {
            return game[i, j];
        }

        //kliknięcie prawym na kwadrat
        public void NextStateOfSquare(int i, int j)
        {
            game.ChangeStateOfSquare(i, j);
        }

        //kliknięcie lewym na kwadrat
        public bool RevealSquare(int i, int j)
        {
            if (game.RevealSquar(i, j) == false)
            {
                game.RevealAll();
                return false;
            }
            else return true;
        }

        //wygenerowanie planszy do gry
        public void InitGame()
        {
            game = new Game(Width, Height, NumberOfMines);
        }

        //sprawdzenie czy gra wygrała
        public bool CheckedWin()
        {
            int count = 0;
            for(int i = 0; i < Width; i++)
            {
                for(int j = 0; j < Height; j++)
                {
                    if (game[i, j].Condition != Square.State.IS_REVEAL)
                        count++;
                }
            }
            return count == NumberOfMines;
        }
    }
}
