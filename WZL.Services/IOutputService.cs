﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.Services
{
    public interface IOutputService
    {
        void On();

        void Off();

        bool IsOn();

        
    }
}
