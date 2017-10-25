using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;

namespace KoloryWPF.ModelWidoku
{
    using Model;
    using System.Windows.Input;

    public class EdycjaKoloru : ObservedObject
    {
        private readonly Kolor kolor = Ustawienia.Czytaj();

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged(params string[] nazwyWłasności)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        foreach (string nazwaWłasności in nazwyWłasności)
        //            PropertyChanged(this, new PropertyChangedEventArgs(nazwaWłasności));
        //    }
        //}

        private ICommand resetujCommand;

        public ICommand Resetuj
        {
            get
            {
                if (resetujCommand == null) resetujCommand = new ResetujCommand(this);
                return resetujCommand;
            }
        }

        private ICommand ustawCommand;

        public ICommand Ustaw
        {
            get
            {
                if (ustawCommand == null) ustawCommand = new UstawCommand(this);
                return ustawCommand;
            }
        }

        public byte R
        {
            get
            {
                return kolor.R;
            }
            set
            {
                kolor.R = value;
                OnPropertyChanged("R", "Color");
            }
        }

        public byte G
        {
            get
            {
                return kolor.G;
            }
            set
            {
                kolor.G = value;
                OnPropertyChanged("G", "Color");
            }
        }

        public byte B
        {
            get
            {
                return kolor.B;
            }
            set
            {
                kolor.B = value;
                OnPropertyChanged("B", "Color");
            }
        }

        //public Color Color
        //{
        //    get
        //    {
        //        return kolor.ToColor();
        //    }
            
        //}

        

        public void Zapisz()
        {
            Ustawienia.Zapisz(kolor);
        }
    }

    //static class Rozszerzenia
    //{
    //    public static Color ToColor(this Kolor kolor)
    //    {
    //        return new Color()
    //        {
    //            A = 255,
    //            R = kolor.R,
    //            G = kolor.G,
    //            B = kolor.B
    //        };
    //    }
    //}

    
}
