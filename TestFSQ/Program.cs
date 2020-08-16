using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xu.WindowsNativeMethods;

namespace TestFSQ
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpectrumTable = new SpectrumTable();
            SpectrumChart = new SpectrumChart("Spectrum", SpectrumTable);
            ChartForm = new ChartForm();
            ChartForm.AddForm(SpectrumChart);
            ChartForm.Show();
            Application.Run(new MainForm());


        }

        public static SpectrumTable SpectrumTable { get; private set; }

        public static SpectrumChart SpectrumChart { get; private set; }

        public static ChartForm ChartForm { get; private set; }

        public static readonly int SHOW_FSQ = User32.RegisterWindowMessage("SHOW_FSQ");
    }
}
