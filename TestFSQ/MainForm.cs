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
            InitSpecAn();
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
            SpecAnRefreshTask = new Task(() => { SpecAnRefreshWorker(); });
            SpecAnRefreshTask.Start();
        }

        const string SpecAnAddress = "TCPIP0::192.168.18.31::inst0::INSTR";

        private SpecAn SpecAn { get; set; }

        private Task SpecAnRefreshTask { get; set; }

        private CancellationTokenSource CancelSpecAnRefresh { get; } = new CancellationTokenSource();

        private void InitSpecAn() 
        {
            if (SpecAn is null)
            {
                SpecAn = new SpecAn(SpecAnAddress);
                Console.WriteLine(SpecAn.ToString());
                SpecAn.SelectScreen(FSQScreen.A);
            }
        }

        private void SpecAnRefreshWorker()
        {
            if (!SpecAn.IsRefreshing) SpecAn.IsRefreshing = true;
            while (!CancelSpecAnRefresh.IsCancellationRequested)
            {
                Thread.Sleep(10);
                SpecAn.GetTraceData(Program.SpectrumTable, 1);
            }
        }

        private void BtnAutoFindFSQ_Click(object sender, EventArgs e)
        {
            if (SpecAn.IsRefreshing) SpecAn.IsRefreshing = false;

            //Console.WriteLine("Center = " + SpecAn.SetCenterFreq(2802236583.2145587487));
            //Console.WriteLine("Span = " + SpecAn.SetSpanFreq(63256489.85225522266));
            SpecAn.SetCenterFreq(2802236583.2145587487);
            SpecAn.SetSpanFreq(63256489.85225522266);
            Thread.Sleep(100);

            SpecAn.GetTraceData(Program.SpectrumTable, 1);

            //result.ForEach(n => Console.WriteLine(n.freq + " | " + n.value));
        }
    }
}
