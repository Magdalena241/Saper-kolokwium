using Saper.Data;
using Saper.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        public event Action<int, int> ChangeStateSquare;
        public event Action<int, int> RevealSquare;

        SquareView[,] squareViews;
        public Form1(int width = 10, int height = 10, int numbersOfMines = 10)
        {
            squareViews = new SquareView[width, height];
            Size = new Size(40 + width * 24, 40 + height * 26);

            for(int i = 0; i < squareViews.GetLength(0); i++)
            {
                for(int j = 0; j < squareViews.GetLength(1); j++)
                {
                    squareViews[i, j] = new SquareView(i, j);
                    squareViews[i, j].Location = new Point(20 + i * 23, 20 + j * 23);
                    squareViews[i, j].Size = new Size(20, 20);
                    squareViews[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.SquareView_MouseClick);
                    Controls.Add(squareViews[i, j]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //połączenie danych
        public void AddSquareData(int i, int j, Square square)
        {
            squareViews[i, j].SetData(square);
            squareViews[i, j].UpdateView();
        }

        //zaaktualizowanie widoku
        public void Update()
        {
            for(int i = 0; i < squareViews.GetLength(0); i++)
            {
                for(int j = 0; j < squareViews.GetLength(1); j++)
                {
                    squareViews[i, j].UpdateView();
                }
            }
        }

        //kliknięcie na kwadracik
        private void SquareView_MouseClick(object sender, MouseEventArgs e)
        {
            SquareView a = (SquareView)sender;
            if (e.Button == MouseButtons.Left)
            {
                RevealSquare?.Invoke(a.IndexI, a.IndexJ);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ChangeStateSquare?.Invoke(a.IndexI, a.IndexJ);
            }

        }
    }
}
