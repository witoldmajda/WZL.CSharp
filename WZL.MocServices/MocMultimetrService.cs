using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.Services;

namespace WZL.MocServices
{
    public class MocMultimetrService : IVoltageOutputService, ICurrentOutputService, IBinaryService


    {
        public bool[] Get()
        {
            throw new NotImplementedException();
        }

        public void Set(bool[] outputs)
        {
            throw new NotImplementedException();
        }

        void ICurrentOutputService.Set(float value)
        {
            throw new NotImplementedException();
        }

        void IVoltageOutputService.Set(float value)
        {
            throw new NotImplementedException();
        }

      
    }
}
