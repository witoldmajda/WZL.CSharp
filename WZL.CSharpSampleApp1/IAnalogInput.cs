using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.CSharpSampleApp1
{
    interface IAnalogInput
    {
        float Get(byte slaveId);  //tylko nazwa metody i typy danych jakie pobiera i jakie zwraca

        void Set(float value);

    }
}
