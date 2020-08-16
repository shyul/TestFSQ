using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFSQ
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnTestFindResources_Click(object sender, EventArgs e)
        {
            var list = VisaClient.FindResources();
            foreach(string s in list) 
            {
                Console.WriteLine(s);
            }
        }

        private void BtnTestFSQ_Click(object sender, EventArgs e)
        {

            //Console.WriteLine(Fsq.Client.Query("FREQ:CENT?\n"));

            SpecAnUpdateTask = new Task(() => { SpecAnUpdateWorker(); });
            SpecAnUpdateTask.Start();
        }

        FSQ Fsq { get; set; }

        Task SpecAnUpdateTask { get; set; }

        CancellationTokenSource CancelSpecAnUpdate { get; } = new CancellationTokenSource();

        private void SpecAnUpdateWorker() 
        {
            while (!CancelSpecAnUpdate.IsCancellationRequested) 
            {
                Fsq.GetTraceData(Program.SpectrumTable, 1);
                Thread.Sleep(200);
                // Add wait OPC? here/
            }
        
        }

        private void BtnAutoFindFSQ_Click(object sender, EventArgs e)
        {
            Fsq = new FSQ("TCPIP0::192.168.18.31::inst0::INSTR");
            Console.WriteLine(Fsq.ToString());


            Fsq.SelectScreen(FSQScreen.A);
            Console.WriteLine("Center = " + Fsq.SetCenterFreq(3602236584.2145587487));
            Console.WriteLine("Span = " + Fsq.SetSpanFreq(43256489.85445522266));

            Thread.Sleep(500);


            Fsq.GetTraceData(Program.SpectrumTable, 1);
            /*
            Fsq.GetTraceData();
            Fsq.GetTraceData();

            double freq = Fsq.GetStartFreq();
            double stopFreq = Fsq.GetStopFreq();
            double delta = Math.Abs(stopFreq - freq);
        
            var list = Fsq.GetTraceData().ToList();
            double space = delta / (list.Count - 1);

            List<(double freq, double value)> result = new List<(double freq, double value)>();
            for(int i = 0; i < list.Count; i++) 
            {
                result.Add((freq, list[i]));
                freq += space;
            }


            result.ForEach(n => Console.WriteLine(n.freq + " | " + n.value));*/

        }
    }
}
