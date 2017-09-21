using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.Services
{
    public interface IVoltageInputService 
    {
        float Get();
    }

    public interface IVoltageOutputService
    {
        void Set(float value);
    }


    public interface IFrequencyInputService
    {
        float Get();
    }

    public interface IELVoltageOutputService
    {
        void Set(float value);
    }
}

