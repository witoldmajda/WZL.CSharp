using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.CSharp.Models;

namespace WZL.CSharpSampleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            ArrayTest();

            VarTest();

            InitTest();

            NullableTest();

            //ConvertTest();

            // TypesTest();

            //HelloWorldTest();
        }

        private static void ArrayTest()
        {
            //znamy ilość elementów w tablicy
            int[] values = new int[4];
            values[0] = 2;
            values[255] = 255;

            //Wyświetlenie tablicy jako ciągu tekstowego
            var message = String.Join(", ", values);
            Console.WriteLine(message);


            Console.WriteLine(values[2]);
            //Utworzenie tablicy za pomocą inicjalizatora


            int[] weights = new int[] { 20, 0, 255, 0 };
            Console.WriteLine(values[3]);

            //Wyświetlenie wszystkich wartości tablicy
            foreach (var value in values)
            {
                Console.WriteLine(value);
            }
        }

        private static void VarTest()
        {
            string y = "Hello";
            var x = "Hello";  //przypisanie innego typu, kopilator prYpisuje typ

            //generowanie anonimowej klasy w locie, kompilator tworzy to na podstawie wnioskowania
            var device3 = new { DeviceId = 10, Model = "Siemens" };
        }

        private static void InitTest()
        {
            float result01 = 5.06f;

            float result02 = result01;

            result02 = result02 + 1;



            Device device = new Device
            {                               //jeśli nie chce odwoływać się poprzez nazwę obiektu to mam inicjalizatory
                SlaveId = 2,
                Manufacture = "Siemens",
                Model = "PD8",
                SerialNumber = "SN 001"
            };

            //device.SetSlaveId(1);
            device.SlaveId = 1;

            //device.SetSerialNumber("S/N 123");
            device.SerialNumber = "S/N 123";
            device.Model = "S4A0";
            device.Manufacture = "Siemens";

            Device device2 = device; //referencja do obiektu
            device2.Manufacture = "Lumel"; //każdy wskaźnik zmiany wykonuje na oryginalnym obiekcie w obszarze w pamięci


            Console.WriteLine(device.SlaveId);
            Console.WriteLine(device.SerialNumber);



            float result7 = device.GetMeasure();


            Calculator calculator = new Calculator();

            int result = calculator.Add(10, 20);

            int result2 = calculator.Multiply(5, 2);

            int result3 = calculator.Add(10, 20, 30);

            int result4 = calculator.Multiply(5, 2, 3);
        }

        private static void NullableTest()
        {
            int? age = null;

            if (age.HasValue)
            {
                Console.WriteLine(age.Value);

            }
            else
            {
                Console.WriteLine("Wartość nieokreślona");
            }
        }

        private static void ConvertTest()
        {
            byte x = 255;
            int y = x; //niejawna konwersja int jest bardziej pojemny niż byte (Implicit Conversion)

            int a = 252;

            //sprawdzanie przekroczenia
            checked
            {
                byte b = (byte)a; //jawna konwersja, rzutownie (Explicit Conversion)
            }

            float voltage = 5.06f;

            double output = voltage;

            double amper = 10.3004d; //niejawna konwersja

            float input = (float)amper; //jawna konwersja

            //konwersja tekstu na liczbę

            Console.Write("Podaj próg: ");
            string response = Console.ReadLine();

            int range = int.Parse(response);

            float lowBound = float.Parse(response);

            int quantity;

            if (int.TryParse(response, out quantity))   //out parametr wyjściowy
            {
                Console.WriteLine($"Ilość: {quantity}");
            }
            else
            {
                Console.WriteLine("Wprowadzono złą wartość");
            }

            string size = "10";
        }

        private static void TypesTest()
        {
            string firstname = "Witek";  //dla ciagu znakow podwojny cudzyslow

            char sign = 'a'; //dla znaku pojedynczy cudzyslow

            char firstLetter = firstname[0];

            short a = 10; //Int16

            int b = 10;  //alias Int32

            long c = 10; //Int64

            // uint, ushort, ulong bez znaku

            byte x1 = 255; // 8 bitow
            sbyte x2 = -127; // 8 bitow ze znakiem

            // typ bitowy
            bool bit = true;

            //liczby ułamkowe 

            //stałoprzecinkowe

            decimal amount = 100.04m;

            //liczby zmienno przecinkowe

            float voltage = 5.04f;

            double size = 100.01d;
        }

        private static void HelloWorldTest()
        {
            Console.WriteLine("Hello World");

            Console.Write("Podaj imię: ");

            string firstname = Console.ReadLine();

            Console.Write("Podaj nazwisko: ");

            string lastname = Console.ReadLine();

            //  string message = String.Format("Witaj {0} {1}", firstname, lastname);

            //C# 6.0
            string message = $"Witaj {firstname} {lastname} !???";

            Console.WriteLine(message);


            Device device = new Device();
        }
    }
}
