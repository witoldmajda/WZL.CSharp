using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.CSharp.Models
{
    public class Device
    {
        private string serialNumber; //pole prywatne (back filed)

        //właściwość (propoerty)
        public string SerialNumber
        {
            get
            {
                return this.serialNumber;
            }

            set
            {
                this.serialNumber = value;
            }
        }



        private byte slaveId;

        public byte SlaveId
        {
            get
            {
                return this.slaveId;
            }

            set
            {
                this.slaveId = value;
            }
        }

        //metoda uproszczona bez walidacji
        //public byte SlavedId { get; set;};

        public string Model { get; set; }

        public string Manufacture { get; set; }


        //public void SetSerialNumber(string serialNumber)
        //{
        //    this.serialNumber = serialNumber;
        //}

        public void SetSlaveId(byte slaveId)
        {
            this.slaveId = slaveId;
        }

        public byte GetSlaveId()
        {
            return this.slaveId;
        }


        public float GetMeasure()
        {
            Console.WriteLine($"Connecting to {slaveId}");

            float result = 5.05f;

            Console.WriteLine($"Result {result} from {slaveId}");

            return result;
        }

        //konstruktor obiektu klasy
        public Device()
        {
            this.Manufacture = "Lumel";
        }
        //przeciążenie konstruktora

        public Device(byte slaveId)
        {
            this.slaveId = slaveId;
        }
    }
}
