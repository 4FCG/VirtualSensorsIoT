using Casus_B2D4_Sensors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class MedicijnDispenser : VirtualSensor
    {
        public MedicijnDispenser(Sensor sensor)
            : base(sensor)
        {
        }

        override public SensorMeting GenerateRandomReading()
        {
            Random rnd = new Random();

            //max 5% chance to be alarming value, slighty altered by sensor randomFactor
            if (rnd.Next(0, 100) < (1 + 4 * this.randomFactor))
            {
                //Patient did not take medicine
                return new SensorMeting()
                {
                    SensorId = this.Sensor.SensorId,
                    MetingWaarde = 0,
                    Alarm = true
                };
            }
            else
            {
                //Patient took medicine
                return new SensorMeting()
                {
                    SensorId = this.Sensor.SensorId,
                    MetingWaarde = 1
                };
            }

        }
    }
}
