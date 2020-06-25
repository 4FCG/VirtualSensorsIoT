using Casus_B2D4_Sensors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class DeurAlarm : VirtualSensor
    {
        public DeurAlarm(Sensor sensor)
            : base(sensor)
        {
        }

        override public SensorMeting GenerateRandomReading()
        {
            Random rnd = new Random();
            
            //max 5% chance that patient tries to leave the room, slightly randomized by sensor random value
            if (rnd.Next(0, 100) < (1 + 4 * this.randomFactor))
            {
                //Check if person is allowed to leave the room
                using (b2d4ziekenhuisContext context = new b2d4ziekenhuisContext())
                {
                    if ((bool)context.Patient.Find(this.Sensor.PatientId).GeslotenKamer)
                    {
                        return new SensorMeting()
                        {
                            SensorId = this.Sensor.SensorId,
                            MetingWaarde = 1,
                            Alarm = true
                        };
                    }
                }
            }

            return null;
        }
    }
}
