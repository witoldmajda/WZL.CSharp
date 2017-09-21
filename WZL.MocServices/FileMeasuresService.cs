using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WZL.PowerUnit.Models;
using WZL.Services;

namespace WZL.MocServices
{
    public class FileMeasuresService : IMeasuresServices
    {

        private string filename;

        public FileMeasuresService(string filename)
        {
            this.filename = filename;
        }

        public void Add(Measure measure)
        {
           

            //utworzenie nagłówka
            if(!File.Exists(filename))
            {
                File.WriteAllText(filename, "MeasureDate, Voltage[V], Current[A], Power[W]");
                File.AppendAllText(filename, Environment.NewLine);
            }

            //Dopisuje do istniejącego pliku lub tworzy nowy
            using (var stream = File.AppendText(filename))
            {
                stream.WriteLine($"{measure.MeasureDate}, {measure.Voltage.ToString(CultureInfo.InvariantCulture)}, {measure.Current.ToString(CultureInfo.InvariantCulture)}, {measure.Power.ToString(CultureInfo.InvariantCulture)}");
            }
               
        }

        public List<Measure> Get(MeasureSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
