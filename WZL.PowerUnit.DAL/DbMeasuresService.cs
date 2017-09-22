using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.PowerUnit.Models;
using WZL.Services;

namespace WZL.PowerUnit.DAL
{
    public class DbMeasuresService : IMeasuresServices
    {       
        public DbMeasuresService()
        {
           
        }

        public void Add(Measure measure)
        {
            //Utworzenie instancji kontekstu
            using (var context = new PowerUnitContext())
            {
                //Dodanie do kontekstu
                context.Measures.Add(measure);

                //Zapis do bazy danych
                context.SaveChanges();
            }
        }

        public List<Measure> Get(MeasureSearchCriteria criteria)
        {
            using (var context = new PowerUnitContext())
            {
                //wyniki z bazy danych do listy
                //var measures = context.Measures.ToList();

                var endDate = criteria.EndDate.AddDays(1); // rozwiązanie problemu z pobieraniem daty

                /*
                 * składnia za pomocą wyrażenia Lambda
                 * 
                var measures = context.Measures
                    .Where(measure => measure.MeasureDate >= criteria.StartDate)
                    .Where(measure => measure.MeasureDate <= endDate)
                    .ToList();
                */

                //ekwiwalent zapytania powyżej z dodanym sortowaniem


                var measures = (
                                from measure in context.Measures
                                where measure.MeasureDate >= criteria.StartDate
                                && measure.MeasureDate < endDate
                                orderby measure.MeasureDate
                                select measure
                               )
                               .ToList();

                return measures;
            }
        }
    }
}
