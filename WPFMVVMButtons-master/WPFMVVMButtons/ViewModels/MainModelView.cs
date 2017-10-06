using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFMVVMButtons.Core;
using WPFMVVMControls.ViewModels;

namespace WPFMVVMButtons.ViewModels
{
    public class MainModelView : ViewModelBase
    {

        private bool _IsStarted;

        public bool IsStarted
        {
            get { return _IsStarted; }
            set { _IsStarted = value; OnPropertyChanged(); }
        }


        public ICommand StartCommand => new RelayCommand(() => Start(), ()=>CanStart);

        private void Start()
        {
            IsStarted = true;
        }

        public bool CanStart
        {
            get
            {
                return !IsStarted;
            }
        }


        public ICommand StopCommand => new RelayCommand(() => Stop(), ()=>CanStop);

        private void Stop()
        {
            IsStarted = false;
        }

        public bool CanStop
        {
            get
            {
                return IsStarted;
            }
        }

    }
}
