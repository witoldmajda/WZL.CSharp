using AsystentZakupówWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsystentZakupówWPF.ModelWidoku
{
    public class ModelWidoku : INotifyPropertyChanged
    {
        private static decimal limit = 2500;

        private SumowanieKwot model = new SumowanieKwot(limit);
        

        public string Suma
        {
            get
            {
                return model.Suma.ToString();
            }
        }

        public decimal Limit
        {
            get
            {
                return limit;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nazwaWłasności)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nazwaWłasności));
        }

        public bool CzyŁańcuchKwotyJestPoprawny(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            decimal kwota;
            if (!decimal.TryParse(s, out kwota)) return false;
            else return model.CzyKwotaJestPoprawna(kwota);
        }

        private ICommand dodajKwotęCommand;

        public ICommand DodajKwotę
        {
            get
            {
                if (dodajKwotęCommand == null)
                    dodajKwotęCommand = new RelayCommand(
                        (object argument) =>
                        {
                            decimal kwota = decimal.Parse((string)argument);
                            model.Dodaj(kwota);
                            OnPropertyChanged("Suma");
                        },
                        (object argument) =>
                        {
                            return CzyŁańcuchKwotyJestPoprawny((string)argument);
                        }
                        );
                return dodajKwotęCommand;
            }
        }
    }
}
