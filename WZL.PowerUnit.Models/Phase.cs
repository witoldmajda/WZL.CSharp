using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.Models
{
    public class Phase :Base
    {
        public float Voltage { get; set; }

        public float Current { get; set; }

        public float ActivePower { get; set; }

        public float ReactivePower { get; set; }

        public float ApparentPower { get; set; }

        public float PowerFactor { get; set; }

        public float PhaseFactor { get; set; }



    }
}
