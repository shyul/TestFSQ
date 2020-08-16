using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xu;
using Xu.Chart;

namespace TestFSQ
{
    public class SpectrumPoint : IRow, IEquatable<SpectrumPoint>
    {
        public SpectrumPoint(double freq, double amp)
        {
            Frequency = freq;
            Amplitude = amp;
        }

        public object this[Column column]
        {
            get
            {
                return column switch
                {
                    NumericColumn nc => this[nc],
                    _ => null,
                };
            }
        }

        private Dictionary<NumericColumn, double> NumericDatums { get; } = new Dictionary<NumericColumn, double>();

        public double this[NumericColumn column]
        {
            get
            {
                return column switch
                {
                    NumericColumn dc when dc == Column_Amplitude => Amplitude,
                    NumericColumn ic when NumericDatums.ContainsKey(ic) => NumericDatums[ic],
                    _ => double.NaN,
                };
            }
            set
            {
                if (double.IsNaN(value) && NumericDatums.ContainsKey(column))
                    NumericDatums.Remove(column);
                else
                    switch (column)
                    {
                        case NumericColumn dc when dc == Column_Amplitude: Amplitude = value; break;
                        default:
                            if (!NumericDatums.ContainsKey(column))
                                NumericDatums.Add(column, value);
                            else
                                NumericDatums[column] = value;
                            break;
                    }
            }
        }

        public double Frequency { get; set; }

        public double Amplitude { get; set; }

        public static NumericColumn Column_Amplitude { get; } = new NumericColumn("Amplitude", "dB");

        public bool Equals(SpectrumPoint other) => Frequency == other.Frequency;
        public static bool operator !=(SpectrumPoint s1, SpectrumPoint s2) => !s1.Equals(s2);
        public static bool operator ==(SpectrumPoint s1, SpectrumPoint s2) => s1.Equals(s2);
        public override bool Equals(object other) => other is SpectrumPoint sp && Equals(sp);

        public override int GetHashCode() => Frequency.GetHashCode();
    }
}
