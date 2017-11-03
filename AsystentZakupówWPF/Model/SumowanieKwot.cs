using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsystentZakupówWPF.Model
{
    public class SumowanieKwot
    {
        public decimal Limit { get; private set; }
        public decimal Suma { get; private set; }

        public SumowanieKwot(decimal limit, decimal suma = 0)
        {
            this.Limit = limit;
            this.Suma = suma;
        }

        public void Dodaj(decimal kwota)
        {
            if (!CzyKwotaJestPoprawna(kwota))
                throw new ArgumentOutOfRangeException("Kwota zbyt duża lub ujemna");
            Suma += kwota;
        }

        public bool CzyKwotaJestPoprawna(decimal kwota)
        {
            bool czyDodatnia = kwota > 0;
            bool czyPrzekroczyLimit = Suma + kwota > Limit;
            return czyDodatnia && !czyPrzekroczyLimit;
        }
    }
}
