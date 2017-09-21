using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.Services
{
    public interface ICurrentInputService
    {
        float Get();
    }

    public interface ICurrentOutputService
    {
        void Set(float value);
    }

    public interface IELCurrentOutputService
    {
        void Set(float value);
    }
}