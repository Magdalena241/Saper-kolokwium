using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Data
{
    public class Square
    {
        public enum State
        {
            NULL,
            FLAG,
            QUESTION_MARK,
            IS_REVEAL
        }

        public Square(bool isMine) 
        {
            IsMine = isMine;
            Condition = State.NULL;
        }

        public bool IsMine { get; set; }
        public State Condition { get; set; }
        public int NumberOfNeighboringMines { get; set; }

        //zmiana stanu po kliknięciu prawym przyciskiem
        public void NextState()
        {
            switch (Condition)
            {
                case State.NULL:
                    Condition = State.FLAG;
                    break;
                case State.FLAG:
                    Condition = State.QUESTION_MARK;
                    break;
                case State.QUESTION_MARK:
                    Condition = State.NULL;
                    break;
            }
        }
    }
}
