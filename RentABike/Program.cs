using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentABike.Model;

namespace RentABike
{
    class Program
    {
        static void Main(string[] args)
        {
            //zbiór z przykładowymi rowerami
            var bikes = new List<Bike>
            {
                new Bike{Number = "001", BikeType = BikeType.Mountain},
                new Bike{Number = "002", BikeType = BikeType.Mountain},
                new Bike{Number = "003", BikeType = BikeType.Road},
                new Bike{Number = "004", BikeType = BikeType.Town},
                new Bike{Number = "005", BikeType = BikeType.Town}
            };
            using (var context = new RentBikeContext())
            {

            }
        }
    }
}
