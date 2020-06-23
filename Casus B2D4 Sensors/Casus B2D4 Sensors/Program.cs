using Casus_B2D4_Sensors.Sensoren;
using Casus_B2D4_Sensors.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace Casus_B2D4_Sensors
{
    class Program
    {
        
        static void Main(string[] args)
        {
            b2d4ziekenhuisContext context = new b2d4ziekenhuisContext();
            var patienten = context.Patient.ForEachAsync(patient => Console.WriteLine($"{patient.Voornaam} {patient.Achternaam}"));

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
