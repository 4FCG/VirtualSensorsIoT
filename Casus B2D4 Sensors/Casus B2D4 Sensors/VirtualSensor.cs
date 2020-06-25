using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Casus_B2D4_Sensors.Models;

namespace Casus_B2D4_Sensors
{
    public abstract class VirtualSensor
    {
        /// <summary>
        /// Sensor property.
        /// </summary>
        /// <value>
        /// The database sensor this virtual sensor is bound to
        /// </value>
        public Sensor Sensor { get; private set; }

        private CancellationTokenSource StopSource { get; set; }

        //Random number assigned to sensor to create different readings from other sensors
        public readonly double randomFactor;

        /// <summary>
        /// Running property.
        /// </summary>
        /// <value>
        /// Tracks if the sensor is running.
        /// </value>
        public bool Running { get; private set; }

        /// <summary>
        /// Amount of readings done.
        /// </summary>
        public int Readings { get; private set; }

        public VirtualSensor(Sensor sensor)
        {
            Sensor = sensor;
            Running = false;
            Readings = 0;

            Random rnd = new Random();
            randomFactor = rnd.NextDouble();

            if ((bool)sensor.Aan)
            {
                Start();
            }
        }

        /// <summary>
        /// Updates the sensor with new sensor data
        /// </summary>
        /// <param name="sensor">The new sensor data</param>
        public void UpdateSensor(Sensor sensor)
        {
            //Check if any relevant settings changed
            if (Sensor.Interval != sensor.Interval || Sensor.Aan != sensor.Aan)
            {
                Sensor = sensor;
                //Turn on/off based on database
                if (Running)
                {
                    if ((bool)sensor.Aan)
                    {
                        //Restart with new changes
                        Start();
                    }
                    else
                    {
                        Stop();
                    }
                }
                else
                {
                    if ((bool)sensor.Aan)
                    {
                        Start();
                    }
                }
            }
        }

        /// <summary>
        /// Main async loop that creates data each interval.
        /// </summary>
        private async void GenerateData()
        {
            while (!StopSource.IsCancellationRequested)
            {
                //Generate value and add it to the database
                AddReading(GenerateRandomReading());

                //Cancel delay to stop task on request
                try
                {
                    await Task.Delay(Sensor.Interval, StopSource.Token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

            }
        }

        /// <summary>
        /// Starts the sensor.
        /// </summary>
        public void Start()
        {
            //Ensure previous instances are stopped before running a new one
            if (StopSource != null)
            {
                Stop();
            }

            //Set new CancellationSource to stop process later and run a new data generator
            StopSource = new CancellationTokenSource();
            Task.Run(GenerateData, StopSource.Token);

            Running = true;
        }

        /// <summary>
        /// Stops the sensor.
        /// </summary>
        public void Stop()
        {
            try
            {
                StopSource.Cancel();
                StopSource.Dispose();
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("An attempt was made to stop a sensor that was not running.");
            }

            Running = false;
        }

        /// <summary>
        /// Adds reading to database.
        /// </summary>
        private void AddReading(SensorMeting meting)
        {
            using (b2d4ziekenhuisContext context = new b2d4ziekenhuisContext())
            {
                //Check if sensor has not been deleted
                if (context.Sensor.Find(this.Sensor.SensorId) == null)
                {
                    //Sensor with this ID no longer exists, stop sensor
                    // TODO: Delete sensor in this case
                    Stop();
                }
                else if (meting != null)
                {
                    //Sensor still exists, add data
                    context.SensorMeting.Add(meting);
                    context.SaveChanges();

                    Readings++;
                }
            }
        } 

        /// <summary>
        /// Creates a random value for the sensor.
        /// </summary>
        public abstract SensorMeting GenerateRandomReading();
    }
}
