using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WZL.Services;

namespace WZL.EAServices
{
    public class SupplierService : IVoltageInputService, IVoltageOutputService, ICurrentOutputService, ICurrentInputService, IOutputService
    {
        private string hostname;
        private int port;

        public SupplierService()
        {
            hostname = ConfigurationManager.AppSettings["supplierHostname"];
            port = int.Parse(ConfigurationManager.AppSettings["supplierPort"]);
        }

        float IVoltageInputService.Get()
        {
           var response = Send("MEAS:VOLT?");

           float result = Convert(response);

           return result;
        }

        void IVoltageOutputService.Set(float value)
        {
            Send("SYST:LOCK ON");
            string command = $"VOLT {value.ToString(CultureInfo.InvariantCulture)}";  //zamiana formatowania na niezależne bez ustawień regionalnych
            Send(command);

           
        }

        void ICurrentOutputService.Set(float value)
        {
            Send("SYST:LOCK ON");
            string command = $"CURRENT {value.ToString(CultureInfo.InvariantCulture)}";  //zamiana formatowania na niezależne bez ustawień regionalnych
            Send(command);
        }

        public string Send(string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command);

            using (var client = new TcpClient(hostname, port))
            using (var stream = client.GetStream())
            {

                
                stream.Write(data, 0, data.Length);
                byte[] lf = { (byte)'\n' };

                stream.Write(lf, 0, 1);

                //Pobieranie odpowiedzi

                if (command.IndexOf("?") >= 0)
                {

                    byte[] buffer = new byte[1024];

                    int bytesRead = stream.Read(buffer, 0, buffer.Length); //

                    string response = Encoding.ASCII.GetString(buffer);

                    return response;
                }
                else
                {
                    return string.Empty;  //zwracanie póstego ciągu
                }


            }
        }

        float ICurrentInputService.Get()
        {
            var response = Send("MEAS:CURR?");

            float result = Convert(response);

            return result;
        }

        private static float Convert(string response)
        {
            var index = response.IndexOf(' '); //zwraca położenie pierwszego znaku

            //Wycina określoną ilość znaków od podanej pozycji
            var number = response.Substring(0, index);

            float result = float.Parse(number, CultureInfo.InvariantCulture);
            return result;
        }

        void IOutputService.On()
        {
            Send("SYST:LOCK ON");
            Send("OUTPUT ON");
        }

        void IOutputService.Off()
        {
            Send("SYST:LOCK ON");
            Send("OUTPUT OFF");
        }

        public bool IsOn()
        {
            var response = Send("OUTPUT?");

            var result = response.StartsWith("ON");  //sprawdza czy tekst rozpoczyna się od ON

            return result;
        }
    }
}
