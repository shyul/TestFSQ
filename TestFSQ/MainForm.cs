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
        const string SpecAnAddress = "TCPIP0::192.168.18.31::inst0::INSTR";
        const string SigGen1Address = "GPIB0::25::INSTR";
        const string SigGen2Address = "TCPIP0::192.168.18.34::inst0::INSTR";
        const string PowerSensorAddress = "USB0::0x2A8D::0x2C18::MY57090007::INSTR";

        public MainForm()
        {
            InitializeComponent();
            InitInstruments();
        }

        

        private void BtnTestFindResources_Click(object sender, EventArgs e)
        {
            var list = ViClient.FindResources();
            foreach(string s in list) 
            {
                Console.WriteLine(s);
            }
        }

        private SpecAn SpecAn { get; set; }

        private SigGen SigGen1 { get; set; }

        private SigGen SigGen2 { get; set; }

        private PowerSensor PowerSensor { get; set; }

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
            
            if (SigGen1 is null)
            {
                SigGen1 = new SigGen(SigGen1Address);
                Console.WriteLine(SigGen1.ToString());

            }
            /*
            if (PowerSensor is null)
            {
                PowerSensor = new PowerSensor(PowerSensorAddress);
                Console.WriteLine(PowerSensor.ToString());
            }*/
        }

        private void ConfigTest(double freq, double power) 
        {
            SpecAn.CenterFrequency = SigGen1.Frequency = freq;
            SpecAn.InputAttenuation = 20;
            SpecAn.ReferenceLevel = 10;
            SpecAn.RBW = 1e5;
            SigGen1.Power = power;

            double delta = SigGen1.Frequency - SpecAn.CenterFrequency;
            double actual_power = power - SigGen1.Power;
            SigGen1.RFOutputEnable = true;
            SpecAn.SpanFrequency = 40e6;

            Console.WriteLine("freq error = " + delta);
            Console.WriteLine("power error = " + actual_power);
        }
        /*
        private async Task SpecAnRefresh() 
        {
            await ......
            if (!SpecAn.IsRefreshing) SpecAn.IsRefreshing = true;
            while (!CancelSpecAnRefresh.IsCancellationRequested)
            {
                Thread.Sleep(10);
                SpecAn.GetTraceData(Program.SpectrumTable, 1);
            }
        }*/

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
            ConfigTest(2702236583.2145587487, 10);

            // Looping Task, no await use here!!
            //Task.Run(SpecAnRefreshWorker);

            SpecAnRefreshTask = new Task(SpecAnRefreshWorker);
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

        private void BtnTestESGError_Click(object sender, EventArgs e)
        {
            if (SigGen1 is null)
            {
                SigGen1 = new SigGen(SigGen1Address);
                Console.WriteLine(SigGen1.ToString());
            }

            //ViException error = SigGen1.GetError();
            //Console.WriteLine(error.Code + " || " + error.Message);
        }

        private void BtnTestU200X_Click(object sender, EventArgs e)
        {
            double res = 0;
            for(int i = 0; i < 32; i++) 
            {
                res += PowerSensor.Power;
            }
            res /= 32;

            Console.WriteLine(PowerSensor.Frequency);
            Console.WriteLine(res);
        }

        private void BtnCalibrateU200X_Click(object sender, EventArgs e)
        {
            PowerSensor.Calibrate();
        }

        private void BtnCalibrateSigGen1_Click(object sender, EventArgs e)
        {
            SigGen1.Power = 0;
            SigGen1.RFOutputEnable = true;
            for (double i = 20e6; i < 3e9; i += 20e6)
            {
                SigGen1.Frequency = PowerSensor.Frequency = i;
                Thread.Sleep(100);
                double power_out = PowerSensor.Power;

                Console.WriteLine(power_out);
            }
        }
    }
}
