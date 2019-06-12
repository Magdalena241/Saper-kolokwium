using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Data
{
    class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int NumberOfMines { get; private set; }
        Square[,] board;
        public Game(int width = 10, int height = 10, int numberOfMines = 10)
        {
            Width = width;
            Height = height;
            NumberOfMines = numberOfMines;

            generate();
        }

        //indexer do dostępu do danych jak do tablicy
        //zwraca wartości o indeksach zwiększonych o 1, aby pominąć brzeg
        public Square this[int i, int j]
        {
            get { return board[i + 1, j + 1]; }
        }

        //wygenerowanie planszy
        private void generate()
        {
            //wymiar jest większy, żeby można bez problemu sprawdzać brzeg
            board = new Square[Width + 2, Height + 2];
            
            //stworzenie pustej planszy
            for (int i = 0; i < Width + 2; i++)
            {
                for (int j = 0; j < Height + 2; j++)
                {
                    bool isMine = false;
                    board[i, j] = new Square(isMine);
                }
            }

            //wylosowanie pozycji min
            int[] indices = MinesIndices();
            int k = 0;
            //ustawienie min na odpowiednich pozycjach
            for (int i = 1; i < Width + 1; i++)
            {
                for (int j = 1; j < Height + 1; j++)
                {
                    if (k < NumberOfMines && (i - 1) * Height + (j - 1) == indices[k])
                    {
                        k++;
                        board[i,j].IsMine = true;
                    }
                }
            }
            //obliczenie liczby min dookoła pola
            CalcualteMines();
        }

        //Losowanie pozycji min
        private int[] MinesIndices()
        {
            List<int> indices = new List<int>();
            int i = 0;
            Random random = new Random();
            int range = Width * Height;
            indices.Capacity = NumberOfMines;
            while (i < NumberOfMines)
            {
                int a = random.Next(0, range);
                if(!indices.Contains(a))
                {
                    indices.Add(a);
                    i++;
                }
            }
            indices.Sort();
            return indices.ToArray();
        }

        //Obliczenie min dookoła pola
        private void CalcualteMines()
        {
            for(int i = 1; i < Width + 1; i++)
            {
                for(int j = 1; j < Height + 1; j++)
                {
                    int count = 0;
                    for(int k = -1; k < 2; k++)
                    {
                        for( int l = -1; l < 2; l++)
                        {
                            if (k == 0 && l == 0)
                                continue;
                            if (board[i + k, j + l].IsMine)
                                count++;
                        }
                    }
                    
                    // jeśli nie jest bombą, to ustawiamy liczbę bomb dookołą
                    if (!board[i, j].IsMine)
                        board[i, j].NumberOfNeighboringMines = count;
                    //jeśli jest bombą, to ustawiamy dowolną dodatnią liczbę - do odsłaniania
                    else
                        board[i, j].NumberOfNeighboringMines = 10;
                }
            }

            //na dodatkowych brzegach też ustawiamy dowolną dodatnią liczbę - do odsłaniania
            for (int i = 0; i < Width + 2; i++)
            {
                board[i, 0].NumberOfNeighboringMines = 10;
                board[i, Height + 1].NumberOfNeighboringMines = 10;
            }
            for (int i = 0; i < Height + 2; i++)
            {
                board[0, i].NumberOfNeighboringMines = 10;
                board[Width + 1, i].NumberOfNeighboringMines = 10;
            }
        }

        //kliknięcie prawym przyciskiem na kwadrat
        public void ChangeStateOfSquare(int i, int j)
        {
            board[i + 1, j + 1].NextState();
        }

        //kliknięcie lewym przyciskiem na kwadrat
        //zwraca true, jeśli pole nie było miną
        //zwraca false, jeśli pole było miną
        public bool RevealSquar(int i, int j)
        {
            //sprawdzamy czy pole można odsłonić
            if (board[i + 1, j + 1].Condition == Square.State.NULL)
            {
                //odsłonięcie pola
                board[i + 1, j + 1].Condition = Square.State.IS_REVEAL;

                //sprawdzamy czy kliknięte pole ma miny obok
                //jeśli nie, to odsłaniamy wszystkie polo dookoła niego
                if(board[i + 1, j + 1].NumberOfNeighboringMines == 0)
                {
                    for(int k = -1; k < 2; k++)
                        for(int l = -1; l < 2; l++)
                        {
                            if (k == 0 && l == 0)
                                continue;
                            else
                                RevealSquar(i + k, j + l);
                        }
                }
                return !board[i + 1, j + 1].IsMine;
            }
            else return true;
        }


        //odsłonięcie wszystkich pól
        public void RevealAll()
        {
            for(int i = 1; i < Width + 1; i++)
            {
                for(int j = 1;j < Height + 1; j++)
                {
                    board[i, j].Condition = Square.State.IS_REVEAL;
                }
            }
        }
    }
}
