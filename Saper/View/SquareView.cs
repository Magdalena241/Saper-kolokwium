using Saper.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Saper.View
{
    class SquareView : TextBox
    {
        public int IndexI { get; private set; }
        public int IndexJ { get; private set; }
        Square square;
        public SquareView(int i, int j): base()
        {
            IndexI = i;
            IndexJ = j;
            ReadOnly = true;
            Show();
            //wyłaczenie context menu
            ShortcutsEnabled = false;
        }
        public void SetData(Square square)
        {
            this.square = square;
        }

        public void UpdateView()
        {
            if (square == null)
                return;
            BackColor = Color.Green;
            switch (square.Condition)
            {
                case Square.State.NULL:
                    Text = "";
                    break;
                case Square.State.QUESTION_MARK:
                    Text = "?";
                    break;
                case Square.State.FLAG:
                    Text = "\u06E9";
                    break;
                case Square.State.IS_REVEAL:
                    if (square.IsMine)
                    {
                        Text = "*";
                        BackColor = Color.Red;
                    }
                    else
                    {
                        if(square.NumberOfNeighboringMines != 0)
                            Text = "" + square.NumberOfNeighboringMines;
                        BackColor = Color.LightGray;
                    }
                    break;
            }
            Update();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SquareView
            // 
            this.ResumeLayout(false);

        }
    }
}
