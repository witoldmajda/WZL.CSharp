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
    public class ElectronicLoadService : IElectronicLoadService, IELVoltageOutputService, IELCurrentOutputService
    {
        private string hostnameEL;
        private int portEL;
        

       
        public ElectronicLoadService()
        {
            hostnameEL = ConfigurationManager.AppSettings["supplierELHostname"];
            portEL = int.Parse(ConfigurationManager.AppSettings["supplierELPort"]);
        }

        public string Send(string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command);

            using (var clientEL = new TcpClient(hostnameEL, portEL))
            using (var streamEL = clientEL.GetStream())
            {


                streamEL.Write(data, 0, data.Length);
                byte[] lf = { (byte)'\n' };

                streamEL.Write(lf, 0, 1);

                //Pobieranie odpowiedzi

                if (command.IndexOf("?") >= 0)
                {

                    byte[] buffer = new byte[1024];

                    int bytesRead = streamEL.Read(buffer, 0, buffer.Length); //

                    string response = Encoding.ASCII.GetString(buffer);

                    return response;
                }
                else
                {
                    return string.Empty;  //zwracanie póstego ciągu
                }


            }
        }

        void IElectronicLoadService.InputOff()
        {
            Send("INP OFF");
        }

        void IElectronicLoadService.InputOn()
        {
            Send("INP ON");
        }

        bool IElectronicLoadService.IsOn()
        {
            var response = Send("LOCK?");

            var result = response.StartsWith("ON");  //sprawdza czy tekst rozpoczyna się od ON

            return result;
        }

        void IElectronicLoadService.Off()
        {
            Send("LOCK OFF");
            
        }

        void IElectronicLoadService.On()
        {
            Send("LOCK ON");
            
        }

        void IElectronicLoadService.SendEL()
        {
            Send("LOCK ON");
            
            // supServ.Send("OUTPUT ON");

            //return 102;
            
        }

        void IELVoltageOutputService.Set(float value)
        {
            Send("LOCK ON");
            string command = $"VOLT {value.ToString(CultureInfo.InvariantCulture)}";  //zamiana formatowania na niezależne bez ustawień regionalnych
            Send(command);
        }

        void IELCurrentOutputService.Set(float value)
        {
            Send("LOCK ON");
            string command = $"CURRENT {value.ToString(CultureInfo.InvariantCulture)}";  //zamiana formatowania na niezależne bez ustawień regionalnych
            Send(command);
        }
    }
}
