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
        public Sensor Sensor { get; private set; }

        public Stappenteller (int interval)
            :base(interval)
        {
            
        }

        override public void GenerateRandomValue()
        {
            Random rnd = new Random();
            double[] wandelsnelheden = {4/3.6, 5/3.6, 6/3.6};
            double wandelsnelheid = wandelsnelheden[rnd.Next(wandelsnelheden.Length)];
            //Afstand in meter
            double gelopenAfstand = wandelsnelheid * (base.Interval / 1000);

            using (b2d4ziekenhuisContext context = new b2d4ziekenhuisContext())
            {
                context.SensorMeting.Add(new SensorMeting
                {
                    Sensor = this.Sensor,
                    MetingWaarde = gelopenAfstand
                });


            }
        }
    }
}
