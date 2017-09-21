using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Data;
using System.Windows.Input;
using WZL.EAServices;
using WZL.LumelServices;
using WZL.MocServices;
using WZL.PowerUnit.DAL;
using WZL.PowerUnit.Models;
using WZL.PowerUnit.WpfClient;
using WZL.Services;

namespace WZL.PowerUnit.WpfClient.ViewModels
{
    public class ElectronicLoadViewModel : BaseviewModel     //dziedziczenie po klasie BaseViewModel  
    {
        #region ELIsPowerON
        //właściwość wskazująca czy obciążenie jest załączone

        private bool _ELIsPowerON;

        public bool ELIsPowerON
        {
            get { return _ELIsPowerON; }
            set
            {
                _ELIsPowerON = value;

                OnPropoertyChanged(nameof(ELIsPowerON)); //metoda informująca że zmieniła się właściwość
                                                        // i kontrolki z nią powiązane powinny się odświeżyć
            }
        }
        #endregion

        #region ELIsInputON

        //właściwość wskazująca czy wyjście  obciążenia jest załączone

        private bool _ELIsInputON;

        public bool ELIsInputON
        {
            get { return _ELIsInputON; }
            set
            {
                _ELIsInputON = value;
                OnPropoertyChanged(nameof(ELIsInputON));
            }
        }
        #endregion


        #region NastawyEL

        //utworzenie właściwości przechowującej wartość nastawianą na obciążeniu

        private float _SettingsEL;
        public float SettingsEL
        {
            set
            {
                _SettingsEL = value;
                OnPropoertyChanged(nameof(SettingsEL)); //nameof pobiera nazwy obiektów nie ma połyłki w literówkach
            }
            get { return _SettingsEL; }

        }
        #endregion

        public float ELSettingsVoltageHighLimit { get; } = 80; // właściwości  gdy nie ma set jest to wartość tylko do odczytu
        public float ELSettingsVoltageLowLimit { get; } = 0;
        public float ELSettingsCurrentHighLimit { get; } = 600; // właściwości gdy nie ma set jest to wartość tylko do odczytu
        public float ELSettingsCurrentLowLimit { get; } = 0;

        #region ELSettingsVoltage 

        // właściwość przechowjąca nastawione napięcie obciazenia EL
        private float _ELsettingsVoltage;
        public float ELSettingsVoltage
        {
            get { return _ELsettingsVoltage; }
            set
            {
                //walidacja zakresu
                if (value < ELSettingsVoltageLowLimit)
                    _ELsettingsVoltage = ELSettingsVoltageLowLimit;
                else
                    if (value > ELSettingsVoltageHighLimit)
                    _ELsettingsVoltage = ELSettingsVoltageHighLimit;
                else
                    _ELsettingsVoltage = value;

                OnPropoertyChanged(nameof(ELSettingsVoltage)); //nameof pobiera nazwy obiektów nie ma pomyłki w literówkach
            }
        }

        #endregion


        #region ELSettingsCurrent 

        // właściwość przechowjąca nastawiony prąd obciazenia EL
        private float _ELsettingsCurrent;
        public float ELSettingsCurrent
        {
            get { return _ELsettingsCurrent; }
            set
            {
                //walidacja zakresu
                if (value < ELSettingsCurrentLowLimit)
                    _ELsettingsCurrent = ELSettingsCurrentLowLimit;
                else
                    if (value > ELSettingsCurrentHighLimit)
                    _ELsettingsCurrent = ELSettingsCurrentHighLimit;
                else
                    _ELsettingsCurrent = value;

                OnPropoertyChanged(nameof(ELSettingsCurrent)); //nameof pobiera nazwy obiektów nie ma połyłki w literówkach
            }
        }

        #endregion


        private IElectronicLoadService ElectronicLoadService; //tworzymy zmienną interfejsu która przechowuje obiekt obciążenia

        private IELVoltageOutputService ELVoltageOutputService;

        private IELCurrentOutputService ELCurrentOutputService;

        #region ELSend

        //Wzorzec projektowy do obsługi kontrolek
        //właściwość służąca do przechowywania wywołania komendy po przyciśnięcu przycisku połączonego z tą właściwością


        private ICommand _ELSend;  //prywatna zmienna

        public ICommand ELSend
        {
            get
            {
                if (_ELSend == null)
                {
                    _ELSend = new RelayCommand(ELSendComm, ELCanSend); // wywołuje metody poprzez wskaźniki do funkcji
                }

                return _ELSend;
            }
        }

        private void ELSendComm()
        {
            //TODO: sterowanie sprzętem obciążeniem EL

            ELVoltageOutputService.Set(ELSettingsVoltage);
            ELCurrentOutputService.Set(ELSettingsCurrent);

        }

        private bool ELCanSend()
        {
            //TODO: warunek sterowania sprzętem
            return this.ELSettingsVoltage > 0 && ELSettingsCurrent > 0;
        }


        #endregion

        #region ELSwitchCommand

        //właściwość służąca do przechowywania wywołania komendy po przyciśnięcu przycisku połączonego z tą właściwością

        private ICommand _ELSwitchCommand;

        public ICommand ELSwitchCommand
        {
            get
            {
                if (_ELSwitchCommand == null)
                {
                    _ELSwitchCommand = new RelayCommand(ELSwitch);
                }

                return _ELSwitchCommand;
            }
        }

        private void ELSwitch()
        {
            if (ELIsPowerON)
            {
                ElectronicLoadService.On();
            }
            else
            {
                ElectronicLoadService.Off();
            }
        }
        #endregion

        #region ELInputSwitchCommand

        //właściwość służąca do przechowywania wywołania komendy po przyciśnięcu przycisku połączonego z tą właściwością

        private ICommand _ELInputSwitchCommand;

        public ICommand ELInputSwitchCommand
        {
            get
            {
                if (_ELInputSwitchCommand == null)
                {
                    _ELInputSwitchCommand = new RelayCommand(ELInputSwitch);
                }

                return _ELInputSwitchCommand;
            }
        }

        private void ELInputSwitch()
        {
            if (ELIsInputON)
            {
                ElectronicLoadService.InputOn();
            }
            else
            {
                ElectronicLoadService.InputOff();
            }
        }


        #endregion

        

        public ElectronicLoadViewModel() // konstruktor klasy głównej
        {
            ElectronicLoadService = new ElectronicLoadService();

            ELVoltageOutputService = new ElectronicLoadService();

            ELCurrentOutputService = new ElectronicLoadService();

           
        }

    }
}
