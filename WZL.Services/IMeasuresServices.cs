﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.PowerUnit.Models;

namespace WZL.Services
{
    public interface IMeasuresServices
    {
        void Add(Measure measure);

        List<Measure> Get(MeasureSearchCriteria criteria);
    }
}
