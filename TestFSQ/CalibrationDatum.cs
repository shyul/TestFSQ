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
    public class Test 
    {

        public static double[] array = { -10, -7, 1, 2  };

        public void FindZeroCrossing() 
        {
            int j = 0;
            for(int i = 0; i < array.Length; i++) 
            {
                if(array[i] > 0) 
                {
                    j = i;
                    break;
                }
            
            }

            Console.WriteLine(array[j]);
        }
    

        
    }













    public class CalibrationDataTable
    {



        private List<CalibrationDatum> Data { get; } = new List<CalibrationDatum>();

        public IEnumerable<CalibrationDatum> DataList => Data.OrderBy(n => n.Frequency); 
     }

    // c# 9.0 public data class CalibrationDatum
    public class CalibrationDatum
    {
        public CalibrationDatum(double freq) 
        {
            Frequency = freq;
        }

        public double Frequency { get; }
    
        // Simplest way
        public double Magnitude { get; set; }

        // For averge purpose, and giving uncertainty bounds.
        public List<double> MagList { get; } = new List<double>();

        public double Magnitude2 => MagList.Count > 0 ? MagList.Sum() / MagList.Count : 0;

        // Each test point also has the instruments status
        public List<TestData> TestMagList { get; } = new List<TestData>();

        public double Magnitude3 => TestMagList.Count > 0 ? TestMagList.Select(n => n.Value).Sum() / MagList.Count : 0;
    }

    public class TestData
    {
        public double Value { get; }

        public DateTime Time { get; }

        public List<InstrumentStatus> InstrumentStatuses { get; } = new List<InstrumentStatus>();
    }

    public class InstrumentStatus 
    {
        public DateTime TimeSincePowerOn { get; }

        public double Temperature { get; }


    }
}
