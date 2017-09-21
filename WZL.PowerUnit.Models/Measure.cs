using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.Models
{
    public class Measure : Base
    {
        public int MeasureId { get; set; }

        public DateTime MeasureDate { get; set; }

        public float Voltage { get; set; }

        public float Current { get; set; }

        public float Power { get; set; }

        public Measure()
        {

        }
        public Measure(DateTime measureDate, float voltage, float current, float power)
        {
            this.MeasureDate = measureDate;
            this.Voltage = voltage;
            this.Current = current;
            this.Power = power;
        }


    }
}
