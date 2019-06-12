using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper.View
{
    public partial class Form2 : Form
    {
        public event Action<int, int, int> StartGame;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sprawdzenie czy gra ma sens.
            if(numericUpDown3.Value > numericUpDown2.Value * numericUpDown1.Value)
            {
                MessageBox.Show("Ilość min musi być mniejsza niż ilość pól w planszy", "Błąd",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                return;
            }
            else
            {
                StartGame?.Invoke((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            }
        }
    }
}
