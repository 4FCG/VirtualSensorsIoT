using Casus_B2D4_Sensors.Sensoren;
using System;

namespace Casus_B2D4_Sensors
{
    class Program
    {
        static void Main(string[] args)
        {
            var tester = new Stappenteller(5000);
            tester.Start();

            //main loop
            while (true)
            {
                Console.ReadLine();
                tester.Stop();
            }
        }
    }
}
