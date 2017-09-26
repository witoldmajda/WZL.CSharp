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
        static void Main(string[] args)
        {
            //ReturnRent();

            //AddRent();

            //  AddBikes();
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
            }
        }
    }
}
