using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WZL.SCPI.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PingTest();
        }

        private static void PingTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine($"Connecting to {hostname} : {port} ");

            try
            {

                Console.WriteLine("Wpisz polecenie");

                while (true)
                {
                    string command = Console.ReadLine();

                    if (command.ToUpper() == "EXIT")
                        break;

                    byte[] data = Encoding.ASCII.GetBytes(command); //komenda do konwersji polecenia na kody ASCII

                    using (var client = new TcpClient(hostname, port))
                    {
                        NetworkStream stream = client.GetStream(); // strumień danych, płynące informacje

                        stream.Write(data, 0, data.Length); // metoda do zapisu komendy w kodzie ASCII od pozycji 0

                        byte[] lf = { (byte) '\n'};

                        stream.Write(lf, 0, 1);

                        if(command.IndexOf("?") >=0)
                        {

                            byte[] buffer = new byte[1024];

                            int bytesRead = stream.Read(buffer, 0, buffer.Length); //

                            string response = Encoding.ASCII.GetString(buffer);

                            Console.WriteLine($"Response: {response}");
                        }

                    }
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("Błąd połączenia w okablowaniu lub błąd w konfiguracji");
            }
        }
    }
}
