using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_DBase_01.Models;

namespace WpfApp_DBase_01.Interfaces
{
    public interface IDbServices
    {
        List<PersonModel> Get();

        void Add(PersonModel person);

        PersonModel Edit(int id);

        void Delete(int id);

        void Save(PersonModel person);
    }
}
