using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.Services;

namespace WZL.MocServices
{
    public class MockVoltageService : IAnalaogInput
    {
        public float Get()
        {
            return 7.2f;
        }
    }
}
