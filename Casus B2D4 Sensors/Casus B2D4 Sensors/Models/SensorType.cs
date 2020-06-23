using System;
using System.Collections.Generic;

namespace Casus_B2D4_Sensors.Models
{
    public partial class SensorType
    {
        public SensorType()
        {
            Sensor = new HashSet<Sensor>();
        }

        public int TypeId { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<Sensor> Sensor { get; set; }
    }
}
