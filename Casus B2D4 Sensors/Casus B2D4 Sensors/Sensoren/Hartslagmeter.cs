using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Casus_B2D4_Sensors.Models;

namespace Casus_B2D4_Sensors.Sensoren
{
    public class Hartslagmeter : VirtualSensor
    {
        //Represents age range, and the acceptable heart rate range that belongs to it. Based on what is accetable for sick kids.
        private static List<Dictionary<string, Tuple<int, int>>> hartslagen = new List<Dictionary<string, Tuple<int, int>>>
        {
            { new Dictionary<string, Tuple<int, int>>{  { "age", new Tuple<int, int>(0, 6) }, { "range", new Tuple<int, int>(85, 150) } } },
            { new Dictionary<string, Tuple<int, int>>{  { "age", new Tuple<int, int>(6, 12) }, { "range", new Tuple<int, int>(70, 135) } } },
            { new Dictionary<string, Tuple<int, int>>{  { "age", new Tuple<int, int>(12, 100) }, { "range", new Tuple<int, int>(60, 120) } } }

        };

        public Hartslagmeter(Sensor sensor)
            : base(sensor)
        {
        }

        override public double? GenerateRandomValue()
        {
            // TODO: Alarm versie
            int leeftijd;
            using (b2d4ziekenhuisContext context = new b2d4ziekenhuisContext())
            {
                leeftijd = context.Patient.Find(this.Sensor.PatientId).Leeftijd;
            }

            Tuple<int, int> range = hartslagen.Find(hartslag => leeftijd > hartslag["age"].Item1 && leeftijd <= hartslag["age"].Item2)["range"];
            Random rnd = new Random();
            double hartslag = range.Item1;
            //Random number in range weighted to middle
            for (int i = 0; i < 4; i++)
            {
                hartslag += rnd.NextDouble() * ((range.Item2 - range.Item1) / 4);
            }
            Console.WriteLine(hartslag);
            return hartslag;
        }
    }
}