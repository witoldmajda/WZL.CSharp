using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentABike.Model;

namespace RentABike
{
    //Klasa dziedzicząca po EntityFramework, obsługa bazy danych

    public class RentBikeContext : DbContext
    {
        //zestaw właściwości w którym wskazujemy jakie klasy chcemy obsługiwać
        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public RentBikeContext()
            :base("RentBike")
        {
        }
    }
}
