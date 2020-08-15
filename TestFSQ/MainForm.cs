using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            Console.WriteLine(Fsq.Client.Query("FREQ:CENT?\n"));
        }

        FSQ Fsq { get; set; }

        private void BtnAutoFindFSQ_Click(object sender, EventArgs e)
        {
            Fsq = new FSQ("TCPIP0::192.168.18.31::inst0::INSTR");
            Console.WriteLine(Fsq.ToString());


            Fsq.SelectScreen(FSQScreen.A);
            Console.WriteLine("Center = " + Fsq.SetCenterFreq(3602236584.2145587487));
            Console.WriteLine("Span = " + Fsq.SetSpanFreq(43256489.85445522266));

            Fsq.GetTraceData().ToList().ForEach(n => Console.WriteLine(n));

        }
    }
}
