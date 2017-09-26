using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_DBase_01.Models
{
    public class PersonModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }

        public PersonModel()
        {

        }

        public PersonModel(string name, string surname, string city)
        {
            this.Name = name;
            this.Surname = surname;
            this.City = city;
        }
    }
}
