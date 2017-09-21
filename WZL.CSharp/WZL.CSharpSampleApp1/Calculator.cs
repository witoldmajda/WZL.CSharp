using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.CSharpSampleApp1
{
   internal class Calculator
    {
        public void Init()
        {

        }
        
        public void Print (int x)
        {

        }
        
        
        //Przeciążenie metod
        public int Add(int x, int y)
        {
            Init();

            Print(1);

            int result = x + y;

            return result;
        }

        public int Add(int x, int y, int z)
        {
            int result = x + y +z;

            return result;
        }

        //public int Multiply(int x, int y)
        //{
        //    int result = x * y;

        //    return result;
        //}

        //parametr opcjonalny
        public int Multiply(int x, int y, int z = 1)
        {
            int result = x * y * z;

            return result;
        }
    }
}
