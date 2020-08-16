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

    public class FSQ
    {
        public FSQ()
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
        }

        public FSQ(string resourceName)
        {
            Client = new VisaClient(resourceName);
            if (!Client.Model.Contains("FSQ")) throw new Exception("Not an FSQ!");
        }

        public void SelectMode(FSQMode mode)
        {
            switch (mode)
            {
                case FSQMode.SpectrumAnalyzer: Client.Write("INST:SEL SAN\n"); break;
                default: break;
            }
        }

        public void SelectScreen(FSQScreen sc)
        {
            switch (sc)
            {
                case FSQScreen.A: Client.Write("DISP:WIND1:SEL\n"); break;
                case FSQScreen.B: Client.Write("DISP:WIND2:SEL\n"); break;
                default: break;
            }
        }

        public double SetCenterFreq(double freq) => double.Parse(SetCenterFreq(freq.ToString("0.#########") + "Hz"));

        public string SetCenterFreq(string freq)
        {
            Client.Write("FREQ:CENT " + freq + "\n");
            return Client.Query("FREQ:CENT?\n").Trim();
        }

        public double SetSpanFreq(double freq) 
        { 
            SetSpanFreq(freq.ToString("0.#########") + "Hz");
            return GetSpanFreq();
        }

        public void SetFullSpan() => Client.Write("FREQ:SPAN:FULL");
        public void SetZeroSpan() => Client.Write("FREQ:SPAN 0Hz");
        public void SetSpanFreq(string freq) => Client.Write("FREQ:SPAN " + freq + "\n");
        public double GetSpanFreq() => GetNumber("FREQ:SPAN?\n");

        public void SetStartFreq(string freq) => Client.Write("FREQ:STAR " + freq + "\n");
        public double GetStartFreq() => GetNumber("FREQ:STAR?\n");

        public void SetStopFreq(string freq) => Client.Write("FREQ:STOP " + freq + "\n");
        public double GetStopFreq() => GetNumber("FREQ:STOP?\n");

        public void SetOffsetFreq(string freq) => Client.Write("FREQ:OFFS " + freq + "\n");
        public double GetOffsetFreq() => GetNumber("FREQ:OFFS?\n");


        public void SelectTrace(int num) => Client.Write("DISP:WIND:TRAC" + num.ToString() + "\n");

        public IEnumerable<double> GetTraceData(int num = 1) => Client.Query("TRAC? TRACE" + num.ToString() + "\n").Split(',').Select(n => n.ToDouble());

        public void GetTraceData(SpectrumTable st, int num = 1)
        {
            double freq = GetStartFreq();
            double stopFreq = GetStopFreq();
            double delta = Math.Abs(stopFreq - freq);

            var list = Client.Query("TRAC? TRACE" + num.ToString() + "\n").Split(',').Select(n => n.ToDouble()).ToList();
            double space = delta / (list.Count - 2);

            st.Status = TableStatus.Downloading;
            lock (st.DataLockObject)
            {
                st.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    SpectrumPoint sp = new SpectrumPoint(freq, list[i]);
                    st.Add(sp);

                    //Console.WriteLine(i + ": " + sp.Frequency + " | " + sp.Amplitude);

                    freq += space;
                }


            }
            st.Status = TableStatus.CalculateFinished;
        }

        public double GetNumber(string cmd) => double.Parse(Client.Query(cmd).Trim());

        public VisaClient Client { get; }
        public override string ToString() => Client.ToString();
    }
}
