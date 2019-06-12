using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saper.Data;

namespace Saper.View
{
    class View : IView
    {
        //forma gry
        Form1 form;
        //forma ekranu startowego
        Form2 form2;

        public View()
        {
            form2 = new Form2();
            form2.StartGame += startGame;
        }

        public event Action<int, int> ChangeStateSquar;
        public event Action<int, int> RevealSquar;
        public event Action<int, int, int> StartGame;

        //uruchomienie aplikacji
        public void Run()
        {
            Application.Run(form2);
        }

        //podłączenie danych do widoku
        public void SetSquareData(int i, int j, Square square)
        {
            form.AddSquareData(i, j, square);
        }

        //kliknięcie lewym przyciskiem myszy
        private void revealSquare(int i, int j)
        {
            RevealSquar?.Invoke(i, j);
        }

        //kliknięcie prawym przyciskiem myszy
        private void changeStateSquare(int i, int j)
        {
            ChangeStateSquar?.Invoke(i, j);
        }


        //zaaktualizowanie widoku względem danych
        public void Update()
        {
            form.Update();
        }

        //przegranie gry
        public void Lost()
        {
            MessageBox.Show("Przegrałeś!!! Spróbuj jeszcze raz ;)", "Przegrana", MessageBoxButtons.OK, MessageBoxIcon.Information);
            form.Close();
        }

        //wygranie gry
        public void Win()
        {
            MessageBox.Show("Gratulacje!!! Wygrałeś!!!", "Wygrana", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            form.Close();
        }

        //wygenerowanie gry o zadanych parametrach
        private void startGame(int width, int height, int numbersOfMines)
        {
            StartGame?.Invoke(width, height, numbersOfMines);
        }

        //wyświetlenie wygenerowanej gry
        public void RunGame(int width, int height, int numbersOfMines)
        {
            form2.Hide();
            form = new Form1(width, height, numbersOfMines);

            //laczenie warstw
            form.ChangeStateSquare += changeStateSquare;
            form.RevealSquare += revealSquare;

            form.Show();

            // jak zostanie zamknieta forma z gra, to otworzy sie forma z wyborem gry
            form.Closed += (s, args) => form2.Show();
        }
    }
}
