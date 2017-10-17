using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_DBase_01.Interfaces;
using WpfApp_DBase_01.Models;

namespace WpfApp_DBase_01
{
    public class DbServices : IDbServices
    {
        public DbServices()
        {

        }

        public void Add(PersonModel person)
        {
            using (var context = new Context())
            {
                //PersonModel person = new PersonModel();
                //person.Name = T_Name;
                //person.Surname = T_Surname;
                //person.City = T_City;
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new Context())
            {
                var person = context.Persons.Where(p => p.Id == id).First();

                context.Persons.Remove(person);

                context.SaveChanges();
            }
        }

        //pobranie z bazy danych elementu o przekazanym id
        public PersonModel Edit(int id)
        {
            using (var context = new Context())
            {
                var query = from sel in context.Persons
                            where sel.Id == id
                            select sel;

                var selected = query.SingleOrDefault();

                return selected;
            }
        }

        

        public List<PersonModel> Get()
        {            
            using (var context = new Context())
            {
                //wyniki z bazy danych do listy
                //var measures = context.Measures.ToList();

                //var endDate = criteria.EndDate.AddDays(1); // rozwiązanie problemu z pobieraniem daty

                /*
                 * składnia za pomocą wyrażenia Lambda
                 * 
                var measures = context.Measures
                    .Where(measure => measure.MeasureDate >= criteria.StartDate)
                    .Where(measure => measure.MeasureDate <= endDate)
                    .ToList();
                */

                //ekwiwalent zapytania powyżej z dodanym sortowaniem


                //var persons = (
                //                from person in context.Persons                                                               
                //                select person
                //               )
                //               .ToList();

                var persons = context.Persons.ToList();

                return persons;
            }
        }

        public void Save(PersonModel person)
        {
            using (var context = new Context())
            {
                var personToUpdate = context.Persons.Where(p => p.Id == person.Id).First();

                personToUpdate.Name = person.Name;
                personToUpdate.Surname = person.Surname;
                personToUpdate.City = person.City;

                context.Entry(personToUpdate).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}
