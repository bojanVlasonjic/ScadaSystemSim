using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Trending
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //todo: napravi beskonacnu petlju
            //dobavljaj podatke sa servera

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FakeChartForm1 chart = new FakeChartForm1();

            Application.Run(chart);
        }
    }
}
