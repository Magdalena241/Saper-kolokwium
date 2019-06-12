using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            View.IView view = new View.View();
            Model.IModel model = new Model.Model();
            Presenter.Presenter presenter = new Presenter.Presenter(view, model);
            presenter.Run();
        }
    }
}
