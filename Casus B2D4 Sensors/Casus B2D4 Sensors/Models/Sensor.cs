using System;
using System.Collections.Generic;

namespace Casus_B2D4_Sensors.Models
{
    public partial class Sensor
    {
        public Sensor()
        {
            SensorMeting = new HashSet<SensorMeting>();
        }

        public int SensorId { get; set; }
        public int Interval { get; set; }
        public int PatientId { get; set; }
        public int SensorType { get; set; }
        public bool? Aan { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual SensorType SensorTypeNavigation { get; set; }
        public virtual ICollection<SensorMeting> SensorMeting { get; set; }
    }
}
