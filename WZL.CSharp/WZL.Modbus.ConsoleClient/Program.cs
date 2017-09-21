using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WZL.Modbus.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            
            do
            {
                Console.WriteLine();
                Console.WriteLine("Wybierz pomiar:");
                Console.WriteLine("(T) Temperature");
                Console.WriteLine("(V) Voltage");
                Console.WriteLine("(B) Binary");
                Console.WriteLine("(A) Analog");
                Console.WriteLine("(N) Network");
                var response = Console.ReadKey(true); //gdy jest true nie wypisywany klawisz

                switch (response.KeyChar)
                {
                    case 'T': GetTemperatureTest(); break;
                    case 'V': GetVoltageTest(); break;
                    case 'B': SetBinaryTest(); break;
                    case 'A': SetAnalogTest(); break;
                    case 'N': GetACVoltageTest(); break;

                    default: Console.WriteLine("Nieznana wartość"); break;
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);   //znak wprowadzony z klawiatury różny od Escape
           
           
           // GetVoltageTest();

           // GetTemperatureTest();
        }

        private static void GetACVoltageTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            var slaveId = byte.Parse(ConfigurationManager.AppSettings["N10"]);

            //Wyjście analogowe nr 1

            ushort startAddress = 7000;  // adres rejestru z instrukcji miernika
            ushort numRegisters = 64; // ile rejestrów chcemy przeczytać od rejestru startu

           Console.WriteLine($"Connecting to {hostname} : {port} ");

            try
            {

                //using wykożystujemy do określenia zakresu działania obiektu i automatycznie wywołuje metodę Dispose do czyszczenia rejestru
                // using można wykożystać tylko dla obiektów, które implemepntują interfejs  IDisposable

              


                using (var client = new TcpClient(hostname, port))
                using (var master = ModbusIpMaster.CreateIp(client))
                {
                   Console.WriteLine("Connected");

                    while (!Console.KeyAvailable)
                    {
                        ushort[] inputs = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                        var voltageL1 = Converter.ConvertToFloat(inputs);

                        var amperA1 = Converter.ConvertToFloat(inputs, 2);

                        var frequency = Converter.ConvertToFloat(inputs, 56);

                        Console.WriteLine($"Napięcie L1: {voltageL1}");

                        Console.WriteLine($"Prąd A1: {amperA1}");

                        Console.WriteLine($"Częstotliwość: {frequency}");

                        Thread.Sleep(1000);  //przerwanie wątku co 1 sekundę
                    }
                    
                }
            }
            //obsługa wyjatku np SocetException - wyjątki połączenia
            catch (SocketException e)
            {
                Console.WriteLine("Błąd połączzenia w okablowaniu lub błąd w konfiguracji");
            }
        }


        private static void SetAnalogTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            
            var slaveId = byte.Parse(ConfigurationManager.AppSettings["S4AO"]);

            //Wyjście analogowe nr 1
            
            ushort startAddress = 4100;  // adres rejestru z instrukcji miernika
            ushort numRegisters = 1; // ile rejestrów chcemy przeczytać od rejestru startu

            float voltage = 4.01f;

            //Konwersja
            ushort output = (ushort)(voltage * 100);

            Console.WriteLine($"Connecting to {hostname} : {port} ");

            try
            {

                //using wykożystujemy do określenia zakresu działania obiektu i automatycznie wywołuje metodę Dispose do czyszczenia rejestru
                // using można wykożystać tylko dla obiektów, które implemepntują interfejs  IDisposable

                using (var client = new TcpClient(hostname, port))
                using (var master = ModbusIpMaster.CreateIp(client))
                {
                    //ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    Console.WriteLine("Connected");

                    //ushort[] outputs = Converter.ConvertToUshort(voltage*100);

                    //master.WriteMultipleRegisters(slaveId, startAddress, outputs);

                   // master.WriteSingleRegister(slaveId, startAddress, output);

                    var input = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                    float volt = input[0] / 100f;

                    Console.WriteLine($"Napięcie: {volt}");
                }
            }
            //obsługa wyjatku np SocetException - wyjątki połączenia
            catch (SocketException e)
            {
                Console.WriteLine("Błąd połączzenia w okablowaniu lub błąd w konfiguracji");
            }
        }


        private static void SetBinaryTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine($"Connecting to {hostname} : {port} ");
            var slaveId = byte.Parse(ConfigurationManager.AppSettings["SM4"]);

            ushort startAddress = 2200;  // adres rejestru z instrukcji miernika
            ushort numRegisters = 4; // ile rejestrów chcemy przeczytać od rejestru startu

            try
            {

                //using wykożystujemy do określenia zakresu działania obiektu i automatycznie wywołuje metodę Dispose do czyszczenia rejestru
                // using można wykożystać tylko dla obiektów, które implemepntują interfejs  IDisposable

                using (var client = new TcpClient(hostname, port))
                using (var master = ModbusIpMaster.CreateIp(client))
                {
                    //ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    Console.WriteLine("Connected");
                    
                    
                    bool[] outputs = { true, false, true, false };

                    master.WriteMultipleCoils(slaveId, startAddress, outputs); //zapis do wektora wyjść

                    // master.WriteSingleCoil(slaveId, startAddress, false); //zapis do pojedynczego rejestru

                    //odczyt wielu rejestrów
                    bool[] inputs = master.ReadCoils(slaveId, startAddress, 4);

                    Console.WriteLine(String.Join(", ", inputs));


                }
            }
            //obsługa wyjatku np SocetException - wyjątki połączenia
            catch (SocketException e)
            {
                Console.WriteLine("Błąd połączzenia w okablowaniu lub błąd w konfiguracji");
            }
        }


        private static void GetVoltageTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine($"Connecting to {hostname} : {port} ");
            var slaveId = byte.Parse(ConfigurationManager.AppSettings["N30H_VoltDC"]);

            ushort startAddress = 7010;  // adres rejestru z instrukcji miernika
            ushort numRegisters = 2; // ile rejestrów chcemy przeczytać od rejestru startu

            try
            {

                //using wykożystujemy do określenia zakresu działania obiektu i automatycznie wywołuje metodę Dispose do czyszczenia rejestru
                // using można wykożystać tylko dla obiektów, które implemepntują interfejs  IDisposable

                using (var client = new TcpClient(hostname, port))
                using (var master = ModbusIpMaster.CreateIp(client))
                {
                    //ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    Console.WriteLine("Connected");

                    Console.WriteLine("Press any kay to exit!");

                    while (!Console.KeyAvailable)
                    {
                        ushort[] inputs = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                        float voltage = Converter.ConvertToFloat(inputs);

                        Console.WriteLine($"Napięcie prądu stałego: {voltage}  V");

                        Thread.Sleep(1000);  //przerwanie wątku co 1 sekundę
                    }



                }
            }
            //obsługa wyjatku np SocetException - wyjątki połączenia
            catch (SocketException e)
            {
                Console.WriteLine("Błąd połączzenia w okablowaniu lub błąd w konfiguracji");
            }
        }

        private static void GetTemperatureTest()
        {
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine($"Connecting to {hostname} : {port} ");
            var slaveId = byte.Parse(ConfigurationManager.AppSettings["N30U_Temp"]);

            ushort startAddress = 7010;  // adres rejestru z instrukcji miernika
            ushort numRegisters = 2; // ile rejestrów chcemy przeczytać od rejestru startu

            try
            {

                //using wykożystujemy do określenia zakresu działania obiektu i automatycznie wywołuje metodę Dispose do czyszczenia rejestru
                // using można wykożystać tylko dla obiektów, które implemepntują interfejs  IDisposable

                using (var client = new TcpClient(hostname, port))
                using (var master = ModbusIpMaster.CreateIp(client))
                {
                    //ModbusIpMaster master = ModbusIpMaster.CreateIp(client);
                    Console.WriteLine("Connected");

                    Console.WriteLine("Press any kay to exit!");

                    while (!Console.KeyAvailable)
                    {
                        ushort[] inputs = master.ReadInputRegisters(slaveId, startAddress, numRegisters);

                        float temperature = Converter.ConvertToFloat(inputs);

                        Console.WriteLine($"Temperature: {temperature}  C");

                        Thread.Sleep(1000);  //przerwanie wątku co 1 sekundę
                    }



                }
            }
            //obsługa wyjatku np SocetException - wyjątki połączenia
            catch(SocketException e)
            {
                Console.WriteLine("Błąd połączzenia w okablowaniu lub błąd w konfiguracji");
            }
                               
        }
    }
}
