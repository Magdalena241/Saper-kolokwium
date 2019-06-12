using Saper.Model;
using Saper.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Presenter
{
    class Presenter
    {
        IView view;
        IModel model;

        public Presenter(IView view, IModel model)
        {
            this.view = view;
            this.model = model;

            view.RevealSquar += revealSquare;
            view.ChangeStateSquar += changeStateSquare;
            view.StartGame += startGame;
        }

        public void Run()
        {
            view.Run();
        }

        //podłączenie danych do widoku
        private void Connect()
        {
            for(int i = 0; i < model.Width; i++)
            {
                for(int j = 0; j < model.Height; j++)
                {
                    view.SetSquareData(i, j, model.GetSquare(i, j));
                }
            }
        }

        //kliknięcie prawym przyciskiem na kwadrat
        private void changeStateSquare(int i, int j)
        {
            model.NextStateOfSquare(i, j);
            view.Update();
        }

        //kliknięcie lewym przyciskiem na kwadrat
        private void revealSquare(int i, int j)
        {
            bool pass = model.RevealSquare(i, j);
            view.Update();
            if (pass == false)
            {
                view.Lost();
            }
            if(model.CheckedWin())
            {
                view.Win();
            }
        }

        //kliknięcie start w formie wyboru
        private void startGame(int width, int height, int numbersOfMines)
        {
            model.Width = width;
            model.Height = height;
            model.NumberOfMines = numbersOfMines;
            model.InitGame();
            view.RunGame(width, height, numbersOfMines);
            Connect();
        }
    }
}
