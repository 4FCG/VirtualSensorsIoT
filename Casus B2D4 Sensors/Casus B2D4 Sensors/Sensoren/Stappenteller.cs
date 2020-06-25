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

        override public SensorMeting GenerateRandomReading()
        {
            Random rnd = new Random();
            double gelopenAfstand;
            //50% chance to have walked, modified by random value assigned to sensor
            if (rnd.Next(100) * this.randomFactor < 50)
            {
                double[] wandelsnelheden = { 4 / 3.6, 5 / 3.6, 6 / 3.6 };
                double wandelsnelheid = wandelsnelheden[rnd.Next(wandelsnelheden.Length)];
                //Distance in meter * random factor
                gelopenAfstand = wandelsnelheid * (this.Sensor.Interval / 1000) * this.randomFactor;
            }
            else
            {
                gelopenAfstand = 0;
            }

            return new SensorMeting()
            {
                SensorId = this.Sensor.SensorId,
                MetingWaarde = gelopenAfstand
            };
        }
    }
}
