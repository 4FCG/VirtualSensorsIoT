using Casus_B2D4_Sensors.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class Temperatuurmeter : VirtualSensor
    {
        public Temperatuurmeter(Sensor sensor)
            : base(sensor)
        {
        }

        override public SensorMeting GenerateRandomReading()
        {
            //Healthy temperature range
            Tuple<double, double> range = new Tuple<double, double> (35.5, 37.5);
            Random rnd = new Random();

            //5% chance to be alarming value
            if (rnd.Next(0, 100) > 5)
            {
                //Minimal value
                double temperatuur = range.Item1;
                //Random number in range weighted to middle
                for (int i = 0; i < 3; i++)
                {
                    temperatuur += rnd.NextDouble() * ((range.Item2 - range.Item1) / 3);
                }

                return new SensorMeting()
                {
                    SensorId = this.Sensor.SensorId,
                    MetingWaarde = temperatuur
                };
            }
            else
            {
                //Alarm reading
                double temperatuur = range.Item2 + 0.5 + 2 * this.randomFactor;
                return new SensorMeting()
                {
                    SensorId = this.Sensor.SensorId,
                    MetingWaarde = temperatuur,
                    Alarm = true
                };
            }

        }
    }
}
