using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_DBase_01.Models;

namespace WpfApp_DBase_01
{
    //klasa dziedziczy po DbContext z EntityFramework
    public class Context : DbContext
    {
              
        //podajemy jakie klasy chcemy użyć do utrwalenia w bazie danych
        public DbSet<PersonModel> Persons { get; set; }


        //W konstruktorze podajemy nazwę wpisu w pliku konfig w sekcji connection strings

        //:base znaczy wywołaj konstruktor bazowy

        //:base("WPF_DBASEConnectionString") //określamy nazwę nowo tworzonej bazy danych
        public Context()
            :base("WPF_DBase2") //określamy nazwę nowo tworzonej bazy danych
        {
        }
    }
}
