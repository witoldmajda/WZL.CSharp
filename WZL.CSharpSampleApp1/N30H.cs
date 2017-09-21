using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.CSharpSampleApp1
{
    //implementacja interfejsu
    class N30H : IAnalogInput
    {
        public float Get(byte slaveId)
        {
            return 100.4f;
        }

        public void Set(float value)
        {

        }
    }
}
