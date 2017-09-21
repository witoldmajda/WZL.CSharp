﻿using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WZL.Services;
using WZL.PowerUnit.Models;

namespace WZL.LumelServices
{

    //implementacja usługi
    public class N10Service : IFrequencyInputService, IThreePhaseInputService
    {
        private string hostname;
        private int port;
        private byte slaveId;

        const ushort startAddress = 7000;
        const ushort numRegisters = 64;

        const float infinity = 1E+20f;

       
        public N10Service()
        {
            hostname = ConfigurationManager.AppSettings["hostname"];
            port = int.Parse(ConfigurationManager.AppSettings["port"]);
            slaveId = byte.Parse(ConfigurationManager.AppSettings["N10"]);
        }



       

        float IFrequencyInputService.Get()
        {
            using (var client = new TcpClient(hostname, port))
            using (var master = ModbusIpMaster.CreateIp(client))
            {
                ushort[] inputs = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                var frequency = Converter.ConvertToFloat(inputs, 56);

                return frequency;

            }
        }

        ThreePhaseMeasure IThreePhaseInputService.Get()
        {
            using (var client = new TcpClient(hostname, port))
            using (var master = ModbusIpMaster.CreateIp(client))
            {
                ushort[] inputs = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                var measure = new ThreePhaseMeasure
                {
                    Frequency = Converter.ConvertToFloat(inputs, 56),
                    L1 = GetPhase(inputs, 1),
                    L2 = GetPhase(inputs, 2),
                    L3 = GetPhase(inputs, 3),
                };

                return measure;

            }
        }

        private static Phase GetPhase(ushort[] inputs, byte phaseNumber)
        {
            var startIndex = (phaseNumber - 1) * 14;

            var phase = new Phase
            {
                Voltage = Converter.ConvertToFloat(inputs, startIndex),
                Current = Converter.ConvertToFloat(inputs, startIndex + 2),
                ActivePower = Converter.ConvertToFloat(inputs, startIndex + 4),
                ReactivePower = Converter.ConvertToFloat(inputs, startIndex + 6),
                ApparentPower = Converter.ConvertToFloat(inputs, startIndex + 8),
                PowerFactor = Converter.ConvertToFloat(inputs, startIndex + 10),
                PhaseFactor = Converter.ConvertToFloat(inputs, startIndex + 12)
            };

            if (phase.PowerFactor == infinity) phase.PowerFactor = 0;
            if (phase.PhaseFactor == infinity) phase.PhaseFactor = 0;

            return phase;
        }
    }
}
