using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.Models
{

    //klasa do obsługi pomiaru trójfazowego N10

    // klasa składająca się z obiektów innych klas np Phase
    public class ThreePhaseMeasure :Base
    {
        public int Id { get; set; }

        public float Frequency { get; set; }
        
        public Phase L1 { get; set; }

        public Phase L2 { get; set; }

        public Phase L3 { get; set; }
    }
}
