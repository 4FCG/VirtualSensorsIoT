using System;
using System.Collections.Generic;

namespace Casus_B2D4_Sensors.Models
{
    public partial class Inname
    {
        public int InnameId { get; set; }
        public int MedicijnId { get; set; }
        public bool? Ingenomen { get; set; }
        public DateTime? InnameTimestamp { get; set; }

        public virtual Medicijn Medicijn { get; set; }
    }
}
