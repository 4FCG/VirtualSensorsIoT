using System;
using System.Collections.Generic;

namespace Casus_B2D4_Sensors.Models
{
    public partial class Medicijn
    {
        public Medicijn()
        {
            Inname = new HashSet<Inname>();
        }

        public int MedicijnId { get; set; }
        public int PatientId { get; set; }
        public string Naam { get; set; }
        public int DagTotaalInname { get; set; }
        public int InnameInterval { get; set; }
        public TimeSpan StarttijdInname { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual ICollection<Inname> Inname { get; set; }
    }
}
