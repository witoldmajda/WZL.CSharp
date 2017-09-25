using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_DBase_01
{
    public class Table1 : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Table1()
        {

        }

        public Table1(string name, string city, string country)
        {
            this.Name = name;
            this.City = city;
            this.Country = country;
        }
    }
}
