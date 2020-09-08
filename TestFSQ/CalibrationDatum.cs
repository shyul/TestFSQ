using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xu;
using Xu.Chart;
using System.Reflection;
using System.Security.Policy;

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


    public class Sprinkle
    {


        public bool SetupEnable
        {
            get => SetupControlE.Enabled;
            set => SetupControlW.Enabled = SetupControlE.Enabled = value;
        }

        public Control SetupControlE { get; set; }
        public Control SetupControlW { get; set; }


        public Task SprinklerTask { get; set; }

        public void Sprinkler()
        {


        }
    }

    public interface ISprinkler 
    {
        string Address { get; }

        void Start();

        void Stop();
    }

    public class FancySprinker : ISprinkler 
    {
        public void Start() => Send("Start");
    }


    public class OldSchoolSprinker : ISprinkler
    {
        public void Start() => Send("Begin");
    }

    public class Zone 
    {
        public Zone(string name) => Name = name;

        public string Name { get; }

        public List<ISprinkler> Sprinklers;

        public void Start() => Sprinklers.ForEach(n=>n.Start);

        public void Stop() => Sprinklers.ForEach(n => n.Stop);
    }



    public class RandomTest
    {




        public int GetRandom(int min, int max)
        {
            /*
            int d = Random().ToInt32();

            d = d % (max - min) + min;
            return d;
            */

            return (Random() * (max - min)).ToInt32() + min;

            /*
            int d = min - 1;// rand();



            while (d < min && d > max) 
            {
        
                //d = c.ToInt32();

            }
          */
        }

        public int[] GetPRBS(int min, int max)
        {
            int s = GetRandom(min, max);

            List<int> plist = new List<int>();

            for (int i = 0; i < sizeof(int); i++)
            {
                plist.Add((s >> i) & 0x1);
            }

            return plist.ToArray();
        }

        public int GetPRBSAlt() => GetRandom(int.MinValue, int.MaxValue);

        public int GetPRBS() => GetRandom(0, 1);

        public int[] GetPRBS(int length)
        {
            List<int> plist = new List<int>();

            for (int i = 0; i < length; i++)
            {
                plist.Add(GetRandom(0, 1));
            }

            return plist.ToArray();
        }

        public void Swap(List<int> source, int X, int Y)
        {
            int i_X = -1, i_Y = -1;

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == X) i_X = i;
                else if (source[i] == Y) i_Y = i;

                if (i_X > 0 && i_Y > 0)
                {
                    source[i_X] = Y;
                    source[i_Y] = X;
                    break;
                }
            }
        }

        public void Swap(List<int> XY)
        {
            if (XY[0] > XY[1])
            {
                int x = XY[0];
                XY[0] = XY[1];
                XY[1] = x;
            }
        }

        public int IndexOfSorted(int needle, int[] haystack)
        {
            //var list = haystack.OrderBy(n => n);

            for (int i = 0; i < haystack.Length; i++)
            {
                if (haystack[i] == needle) return i;
            }

            return -1;
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
