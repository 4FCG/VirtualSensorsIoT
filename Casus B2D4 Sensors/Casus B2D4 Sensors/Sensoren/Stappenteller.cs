using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class Stappenteller : VirtualSensor
    {
        public int Id { get; set; }

        public Stappenteller (int interval)
            :base(interval)
        {
            Id = 1;
        }

        override public void GenerateRandomValue ()
        {
            Console.WriteLine($"Patient met ID:{Id} TESTWAARDE");
        }
    }
}
