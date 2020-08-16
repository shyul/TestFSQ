using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xu;

namespace TestFSQ
{
    public enum FSQMode
    {
        SpectrumAnalyzer,
    }

    public enum FSQScreen
    {
        A,
        B,
    }

    public class SpecAn : VisaClient
    {
        /*
        public SpecAn()
        {
            var list = VisaClient.FindResources();
            foreach (string s in list)
            {
                VisaClient client = new VisaClient(s);

                if (client.Model.Contains("FSQ"))
                {
                    Client = client;
                    break;
                }

                client.Dispose();

                //Thread.Sleep(100);
            }

            if (Client is null) MessageBox.Show("Unable to find FSQ");
        }*/

        public SpecAn(string resourceName) : base(resourceName)
        {
            if (!Model.Contains("FSQ")) throw new Exception("Not an FSQ!");
        }

        public void SelectMode(FSQMode mode)
        {
            switch (mode)
            {
                case FSQMode.SpectrumAnalyzer: Write("INST:SEL SAN\n"); break;
                default: break;
            }
        }

        public void SelectScreen(FSQScreen sc)
        {
            switch (sc)
            {
                case FSQScreen.A: Write("DISP:WIND1:SEL\n"); break;
                case FSQScreen.B: Write("DISP:WIND2:SEL\n"); break;
                default: break;
            }
        }

        public double SetCenterFreq(double freq) => double.Parse(SetCenterFreq(freq.ToString("0.#########") + "Hz"));

        public string SetCenterFreq(string freq)
        {
            Write("FREQ:CENT " + freq + "\n");
            return Query("FREQ:CENT?\n").Trim();
        }

        public double SetSpanFreq(double freq) 
        { 
            SetSpanFreq(freq.ToString("0.#########") + "Hz");
            return GetSpanFreq();
        }

        public void SetFullSpan() => Write("FREQ:SPAN:FULL");
        public void SetZeroSpan() => Write("FREQ:SPAN 0Hz");
        public void SetSpanFreq(string freq) => Write("FREQ:SPAN " + freq + "\n");
        public double GetSpanFreq() => GetNumber("FREQ:SPAN?\n");

        public void SetStartFreq(string freq) => Write("FREQ:STAR " + freq + "\n");
        public double GetStartFreq() => GetNumber("FREQ:STAR?\n");

        public void SetStopFreq(string freq) => Write("FREQ:STOP " + freq + "\n");
        public double GetStopFreq() => GetNumber("FREQ:STOP?\n");

        public void SetOffsetFreq(string freq) => Write("FREQ:OFFS " + freq + "\n");
        public double GetOffsetFreq() => GetNumber("FREQ:OFFS?\n");


        public void SelectTrace(int num) => Write("DISP:WIND:TRAC" + num.ToString() + "\n");

        public IEnumerable<double> GetTraceData(int num = 1) => Query("TRAC? TRACE" + num.ToString() + "\n").Split(',').Select(n => n.ToDouble());

        public void GetTraceData(SpectrumTable st, int num = 1)
        {
            //SyncWait();
            while (!IsReady) { }

            double freq = GetStartFreq();
            double stopFreq = GetStopFreq();
            double delta = Math.Abs(stopFreq - freq);

            var list = GetTraceData(num).ToList();
            double space = delta / (list.Count - 2);

            //st.Status = TableStatus.Downloading;
            lock (st.DataLockObject)
            {
                st.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    SpectrumDatum sp = new SpectrumDatum(freq, list[i]);
                    st.Add(sp);

                    //Console.WriteLine(i + ": " + sp.Frequency + " | " + sp.Amplitude);

                    freq += space;
                }


            }
            st.Status = TableStatus.CalculateFinished;
        }

        public bool IsRefreshing
        {
            get => Query("INIT:CONT?\n").Trim() == "1";
            set
            {
                if (value)
                    Write("INIT:CONT ON");
                else
                    Write("INIT:CONT OFF");
            }
        }
    }
}
