﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.WpfClient.ViewModels
{
    public abstract class BaseviewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; //deklaracja zdarzenia 

        public void OnPropoertyChanged(string propertyName)  //metoda do odświerzania wyświetlanej wartości właściwości jeśli ulegną zmianie
        {
            if(PropertyChanged != null)  // sprawdzamy czy ktoś słucha
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));   //generuje zdarzenie
            }
        }
    }
}
