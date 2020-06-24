using Casus_B2D4_Sensors.Sensoren;
using Casus_B2D4_Sensors.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casus_B2D4_Sensors
{
    class Program
    {
        static void Main(string[] args)
        {
            SensorManager manager = new SensorManager();
            manager.MainLoop().Wait();
        }
    }
}
