﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.Modbus.ConsoleClient
{
    public static class Converter
    {
        public static float ConvertToFloat(ushort[] registers, int index = 0)
        {

            var lRegister = registers[index];
            var hRegister = registers[index+1];

            byte l0 = (byte)lRegister;
            byte l1 = (byte)(lRegister >> 8);  // przesunięcie bitowe o 8

            byte h0 = (byte)hRegister;
            byte h1 = (byte)(hRegister >> 8);

            byte[] bytes = new[] { h0, h1, l0, l1 };  //kolejność jest ważna

            float result = BitConverter.ToSingle(bytes, 0); //metoda przyjmuje tablice bajtów i konwertuje do float

            return result;
        }


        public static ushort[] ConvertToUshort(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            ushort[] result = new ushort[] { BitConverter.ToUInt16(bytes, 2), BitConverter.ToUInt16(bytes, 0) };

            return result;
        }
    }
}
