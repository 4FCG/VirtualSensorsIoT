using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Casus_B2D4_Sensors.Models;
using Casus_B2D4_Sensors.Sensoren;
using Microsoft.EntityFrameworkCore;

namespace Casus_B2D4_Sensors
{
    public class SensorManager
    {
        private List<VirtualSensor> ActiveSensors { get; set; }

        private static int refreshInterval = 5000;

        public SensorManager()
        {
            ActiveSensors = new List<VirtualSensor>();
        }

        //Main loop of the program, takes over the main thread.
        public async Task MainLoop()
        {
            Task refresher;
            Task statusInterface;

            //Firt time run
            refresher = new Task(DataRefresh);
            statusInterface = new Task(StatusInterface);
            refresher.Start();
            statusInterface.Start();

            while (true)
            {
                //Ensure that the action is completed before running a new one
                if (refresher.IsCompleted)
                {
                    refresher = new Task(DataRefresh);
                    refresher.Start();
                }

                if (statusInterface.IsCompleted)
                {
                    statusInterface = new Task(StatusInterface);
                    statusInterface.Start();
                }
                
                await Task.Delay(refreshInterval);
            }
        }

        private void StatusInterface()
        {
            Console.Clear();
            Console.WriteLine($"Aantal sensoren: {ActiveSensors.Count()}");
            Console.WriteLine($"Active sensoren: {ActiveSensors.Where(virtualSensor => virtualSensor.Running).Count()}");

            int readingTotal = 0;
            ActiveSensors.ForEach(sensor => readingTotal += sensor.Readings);
            Console.WriteLine($"Aantal metingen: {readingTotal}");
        }

        private async void DataRefresh()
        {
            using (b2d4ziekenhuisContext context = new b2d4ziekenhuisContext())
            {
                await context.Sensor.ForEachAsync(sensor => 
                {
                    //Check if the sensor already exists
                    VirtualSensor virtualSensor = ActiveSensors.Find(virtualSensor => virtualSensor.Sensor.SensorId == sensor.SensorId);
                    if (virtualSensor == null)
                    {
                        //Sensor does not yet exist, so make a new one
                        if (sensor.SensorType == 1)
                        {
                            ActiveSensors.Add(new Stappenteller(sensor));
                        }
                        else if (sensor.SensorType == 2)
                        {
                            ActiveSensors.Add(new Hartslagmeter(sensor));
                        }
                        else if (sensor.SensorType == 3)
                        {
                            ActiveSensors.Add(new Temperatuurmeter(sensor));
                        }
                        else if (sensor.SensorType == 4)
                        {
                            ActiveSensors.Add(new DeurAlarm(sensor));
                        }
                        else if (sensor.SensorType == 5)
                        {
                            ActiveSensors.Add(new MedicijnDispenser(sensor));
                        }
                    }
                    else
                    {
                        virtualSensor.UpdateSensor(sensor);
                    }
                });
            }
        }
    }
}
