using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentABike.Model;
using System.Data.Entity;

namespace RentABike
{
    class Program
    {
        private static string bikeNr;

        static void Main(string[] args)
        {
            do
            {
                NextAction();
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);   //znak wprowadzony z klawiatury różny od Escape


        }

        private static void NextAction()
        {
           
                System.Console.WriteLine("Wybież co chcesz zrobić:");
                System.Console.WriteLine("1: Wyświetlanie rowerów");
                System.Console.WriteLine("2: Dodanie roweru");
                System.Console.WriteLine("3: Usówanie roweru o wybranym ID");
                System.Console.WriteLine("4: Wypożyczenie roweru");
                System.Console.WriteLine("5: Zwrot roweru");
                System.Console.WriteLine("6: Wypełnienie bazy rowerów od zera");
                System.Console.WriteLine("Twój wybór: ");
                //int actionId = Convert.ToInt32(Console.ReadLine());
                var actionId = Console.ReadKey(true);                                

                switch (actionId.KeyChar)
                {
                    case '1':
                        ShowBikes();
                        break;

                    case '2':
                        AddOneBike();
                        break;
                    case '3':
                        DeleteBike();
                        break;
                    case '4':
                        AddRent();
                        break;
                    case '5':
                        ReturnRent();
                        break;
                    case '6':
                        AddBikes();
                        break;
                    default:
                        break;

                }
           
        }

        private static void ShowBikes()
        {
            using (var context = new RentBikeContext())
            {
                foreach (var bikes in context.Bikes.ToList())
                {
                    System.Console.WriteLine($"Id: {bikes.BikeId}");
                    System.Console.WriteLine($"Numer: {bikes.Number}");
                    System.Console.WriteLine($"Typ: {bikes.BikeType}");
                    System.Console.WriteLine();
                }
                NextAction();
            }
        }

        private static void DeleteBike()
        {
            System.Console.WriteLine("Podaj numer roweru: ");
            bikeNr = Console.ReadLine();
            Console.WriteLine();


            using (var context = new RentBikeContext())
            {
                //wyszukiwanie roweru o numerze
                var bike = context.Bikes.Where(b => b.Number == bikeNr).First();

                //usówanie wskazanego roweru
                context.Bikes.Remove(bike);

                //zapis polecenia do bazy
                context.SaveChanges();

                NextAction();
            }
        }

        private static void ReturnRent()
        {
            using (var context = new RentBikeContext())
            {
                //wyszukiwanie wypożyczenia
                // za pomocą wyrażenia Lambda wskazujemy że wraz z wypożyczeniem ma  być pobrany obiekt roweru
                //Entity Framework wykona left outer join
                var rent = context.Rents.Include(r => r.Bike)
                    .Where(r => r.Bike.Number == "004")
                    .First();

                //przy pobranym wypożyczeniu zmieniamy godzinę wypożyczenia na 2h później od zapisanej w bazie
                rent.ReturnDate = rent.RentDate.AddHours(2);

                context.SaveChanges();

                NextAction();
            }
        }

        private static void AddRent()
        {
            using (var context = new RentBikeContext())
            {
                //pobranie do zmiennej Roweru z bazy danych
                var bike = context
                    .Bikes
                    .Where(t => t.BikeType == BikeType.Town)
                    .FirstOrDefault();  //wybierz element pierwszy z brzegu
                // tworzenie wypożyczenia

                var rent = new Rent { RentDate = DateTime.Now, Bike = bike };

                //wysłanie do bazy danych
                context.Rents.Add(rent); //do kontekstu dodajemy wypożyczenie

                context.SaveChanges();

                NextAction();
            }
        }

        private static void AddBikes()
        {
            var bikes = new List<Bike> //zbiór z przykładowymi rowerami
            {
                new Bike {Number = "001", BikeType = BikeType.Mountain},
                new Bike {Number = "002", BikeType = BikeType.Mountain},
                new Bike {Number = "003", BikeType = BikeType.Road},
                new Bike {Number = "004", BikeType = BikeType.Town},
                new Bike {Number = "005", BikeType = BikeType.Town},
            };

            using (var context = new RentBikeContext())//twożony obiekt kontekstu
            {
                //metoda AddRange śledzi przekazaną listę obiektów, czy zostały dodane, zmienione, czy usunięte
                context.Bikes.AddRange(bikes);

                context.SaveChanges(); // zapisanie do bazy danych 

                NextAction();
            }
        }

        private static void AddOneBike()
        {
            Bike bike = new Bike();
           

            System.Console.WriteLine("Podaj numer roweru: ");
            bike.Number = Console.ReadLine();
            Console.WriteLine();

            System.Console.WriteLine("Wybierz typ roweru: ");                 

            using (var context = new RentBikeContext())//twożony obiekt kontekstu
            {
                BikeType typ = BikeType.Mountain;
                int count = 0;
                for(typ = BikeType.Mountain; typ <= BikeType.Town;  typ++)
                
                {
                    System.Console.WriteLine($"{count}: {typ}");
                    count++;
                    //System.Console.WriteLine();
                }

                int bikeType = Convert.ToInt32(Console.ReadLine());
                System.Console.WriteLine();

                
                
                switch (bikeType)
                {
                    case 0:
                        bike.BikeType = BikeType.Mountain;
                        break;
                    case 1:
                        bike.BikeType = BikeType.Road;
                        break;
                    case 2:
                        bike.BikeType = BikeType.Town;
                        break;
                    default:
                        break;
                }                

                //metoda AddRange śledzi przekazaną listę obiektów, czy zostały dodane, zmienione, czy usunięte
                context.Bikes.Add(bike);
                context.SaveChanges(); // zapisanie do bazy danych 
                Console.WriteLine();
                NextAction();
            }
        }
    }
}
