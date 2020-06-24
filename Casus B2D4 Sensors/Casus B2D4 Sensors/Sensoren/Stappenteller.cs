using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Casus_B2D4_Sensors.Models;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class Stappenteller : VirtualSensor
    {
        public Stappenteller (Sensor sensor)
            :base(sensor)
        {
        }

        override public double? GenerateRandomValue()
        {
            Random rnd = new Random();
            double gelopenAfstand;
            //75% chance to have walked
            if (rnd.Next(4) == 3)
            {
                double[] wandelsnelheden = { 4 / 3.6, 5 / 3.6, 6 / 3.6 };
                double wandelsnelheid = wandelsnelheden[rnd.Next(wandelsnelheden.Length)];
                //Distance in meter * random factor
                gelopenAfstand = wandelsnelheid * (this.Sensor.Interval / 1000) * (rnd.Next(1,11)/10);
            }
            else
            {
                gelopenAfstand = 0;
            }

            return gelopenAfstand;
        }
    }
}
