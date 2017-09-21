using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.PowerUnit.Models;

namespace WZL.PowerUnit.DAL
{
    //klasa dziedziczy po DbContext z EntityFramework
    public class PowerUnitContext : DbContext
    {
        //podajemy jakie klasy chcemy użyć do utrwalenia w bazie danych
        public DbSet<Measure> Measures {get; set;}

        public DbSet<ThreePhaseMeasure> ThreePhaseMeasures { get; set; }

        //W konstruktorze podajemy nazwę wpisu w pliku konfig w sekcji connection strings

            //:base znaczy wywołaj konstruktor bazowy
        public PowerUnitContext()
            : base("PowerUnitConnection")
        {

        }
    }
}
