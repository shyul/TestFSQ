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
    public class PowerSensor : ViClient
    {
        public PowerSensor(string resourceName) : base(resourceName)
        {
            Calibrate();
        }

        public double Frequency
        {
            get => GetNumber("FREQ?\n");
            set => Write("FREQ " + value.ToString("0.#########") + "Hz\n");
        }

        public double Power => GetNumber("FETC?\n");

        public bool IsAutoZero
        {
            get => Query("CAL:ZERO:AUTO?\n").Trim() == "1";
            set
            {
                if (value)
                    Write("CAL:ZERO:AUTO ON");
                else
                    Write("CAL:ZERO:AUTO OFF");
            }
        }

        public void Calibrate() => Write("CAL:ALL");
    }
}
