using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.PowerUnit.Models;
using WZL.Services;

namespace WZL.Services
{
    public interface IthreePhaseMeasureService
    {
        void Add(ThreePhaseMeasure measure);
    }
}
