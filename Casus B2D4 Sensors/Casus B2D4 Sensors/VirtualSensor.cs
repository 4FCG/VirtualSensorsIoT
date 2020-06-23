using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Casus_B2D4_Sensors
{
    public abstract class VirtualSensor
    {
        /// <summary>
        /// Interval property.
        /// </summary>
        /// <value>
        /// Sets the interval in milliseconds for the data loop
        /// </value>
        private int Interval { get; set; }
        private CancellationTokenSource StopSource { get; set; }

        public VirtualSensor(int interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Main async loop that creates data each interval.
        /// </summary>
        private async void GenerateData()
        {
            while (!StopSource.IsCancellationRequested)
            {
                //Generate value and add it to the database
                GenerateRandomValue();

                //Cancel delay to stop task on request
                try
                {
                    await Task.Delay(Interval, StopSource.Token);
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

        }

        /// <summary>
        /// Creates a random value for the sensor and adds it to the database.
        /// </summary>
        public abstract void GenerateRandomValue();
    }
}
