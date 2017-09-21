using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.Services
{
    public interface IElectronicLoadService
    {
        void SendEL();

        void On();

        void Off();

        bool IsOn();

        void InputOn();

        void InputOff();
    }
}
