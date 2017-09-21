using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.Models
{
    //klasa gotowa w celu rozwijania projektu
    public abstract class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropoertyChanged(string propertyName)
        {
            if (PropertyChanged != null)  // sprawdzamy czy ktoś słucha
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));   //generuje zdarzenie
            }
        }
    }
}
