using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
            InitInstruments();
        }

        private void BtnTestFindResources_Click(object sender, EventArgs e)
        {
            var list = VisaClient.FindResources();
            foreach(string s in list) 
            {
                Console.WriteLine(s);
            }
        }



        const string SpecAnAddress = "TCPIP0::192.168.18.31::inst0::INSTR";
        const string SigGenAddress = "GPIB0::25::INSTR";

        private SpecAn SpecAn { get; set; }

        private SigGen SigGen { get; set; }

        private Task SpecAnRefreshTask { get; set; }

        private CancellationTokenSource CancelSpecAnRefresh { get; set; } = new CancellationTokenSource();

        private void InitInstruments()
        {
            if (SpecAn is null)
            {
                SpecAn = new SpecAn(SpecAnAddress);
                Console.WriteLine(SpecAn.ToString());
                SpecAn.SelectMode(FSQMode.SpectrumAnalyzer);
                SpecAn.SelectScreen(FSQScreen.A);
            }

            if (SigGen is null)
            {
                SigGen = new SigGen(SigGenAddress);
                Console.WriteLine(SigGen.ToString());
            }
        }

        private void SetTestLink(double freq, double power) 
        {
            SpecAn.CenterFrequency = SigGen.Frequency = freq;
            SpecAn.InputAttenuation = 20;
            SpecAn.ReferenceLevel = 10;
            SpecAn.RBW = 1e5;
            SigGen.Power = power;

            double delta = SigGen.Frequency - SpecAn.CenterFrequency;
            double actual_power = power - SigGen.Power;
            SigGen.RFOutputEnable = true;
            SpecAn.SpanFrequency = 40e6;

            Console.WriteLine("freq error = " + delta);
            Console.WriteLine("power error = " + actual_power);
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

        private void BtnTestFSQ_Click(object sender, EventArgs e)
        {
            SetTestLink(2702236583.2145587487, 10);
            SpecAnRefreshTask = new Task(() => { SpecAnRefreshWorker(); });
            SpecAnRefreshTask.Start();
        }

        private void BtnAutoFindFSQ_Click(object sender, EventArgs e)
        {
            if (SpecAn.IsRefreshing) SpecAn.IsRefreshing = false;

            //Console.WriteLine("Center = " + SpecAn.SetCenterFreq(2802236583.2145587487));
            //Console.WriteLine("Span = " + SpecAn.SetSpanFreq(63256489.85225522266));
            SpecAn.CenterFrequency = 2802236583.2145587487;
            SpecAn.SpanFrequency = 63256489.85225522266;
            Thread.Sleep(100);

            SpecAn.GetTraceData(Program.SpectrumTable, 1);

            //result.ForEach(n => Console.WriteLine(n.freq + " | " + n.value));
        }
    }
}
