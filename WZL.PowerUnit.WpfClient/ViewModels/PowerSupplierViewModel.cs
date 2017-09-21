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


// przykłady łączenia interfejsu z modelem za pomocą Databinding

namespace WZL.PowerUnit.WpfClient.ViewModels
{
    public class PowerSupplierViewModel : BaseviewModel  //dziedziczenie po klasie BaseViewModel
    {
        #region isPowerON

        private bool isPowerOn;

        public bool IsPowerOn
        {
            get { return isPowerOn; }
            set
            {
                isPowerOn = value;
                OnPropoertyChanged(nameof(IsPowerOn));
            }
        }


        #endregion

      
        #region FrequencyInput


        //tworzenie właściwości przechowującej wartość częstotliwości

        private float _FrequencyInput;

        public float FrequencyInput
        {
            get { return _FrequencyInput; }
            set
            {
                _FrequencyInput = value;
                //notyfikacja właściwości aby odświerzyła się częstotliwość
                OnPropoertyChanged("FrequencyInput");
            }
        }


        #endregion

        #region ThreePhaseMeasure

        private ThreePhaseMeasure _ThreePhaseMeasure;

        public ThreePhaseMeasure ThreePhaseMeasure
        {
            get { return _ThreePhaseMeasure; }
            set
            {
                _ThreePhaseMeasure = value;
                OnPropoertyChanged(nameof(ThreePhaseMeasure));
            }
        }
        #endregion


        #region Voltage  //tworzenie obszaru w celu przejżystosci kodu

        //public float Voltage { get; set; } //Właściwości 

        //private float voltage;


        //public float Voltage
        //{
        //    get { return voltage; }
        //    set
        //    {
        //        voltage = value;

        //        OnPropoertyChanged("Voltage");  // wysyła sygnał żeby się oświerzyły wszystkie konrolki które nasłuchują właściwości Voltage
        //    }
        //}




        #endregion

        #region SupplierVoltage

        private float _SupplierVoltage;

        public float SupplierVoltage
        {
            get { return _SupplierVoltage; }
            set
            {
                _SupplierVoltage = value;
                OnPropoertyChanged(nameof(SupplierVoltage));
            }
        }

        #endregion


        #region SupplierCurrent

        private float _SupplierCurrent;

        public float SupplierCurrent
        {
            get { return _SupplierCurrent; }
            set {
                _SupplierCurrent = value;
                OnPropoertyChanged(nameof(SupplierCurrent));
            }
        }



        #endregion

        #region Measure

        private Measure _Measure;
        public Measure Measure
        {
            get
            {
                return _Measure;
            }
            set
            {
                _Measure = value;
                OnPropoertyChanged(nameof(Measure));
            }
        }

        #endregion

    

        public MeasureSearchCriteria MeasureSearchCriteria
        {
            get;
            set;
        }



        #region Voltages
        private object _lock = new object(); // stworzenie obiektu do blokady wątku

        private ObservableCollection<float> voltages; //lista która wysyła sygnał o zmianie ilości elementów

        public ObservableCollection<float> Voltages
        {
            get { return voltages; }

            set
            {
                voltages = value;
                BindingOperations.EnableCollectionSynchronization(voltages, _lock); //blokuje główny wątek na czas wywołania dodatkowego

                OnPropoertyChanged("Voltages");
            }
        }
        #endregion

        #region Current

        // public float Current { get; set; } //Właściwości 

        //private float current;
        //public float Current
        //{
        //    get { return current; }
        //    set
        //    {
        //        current = value;

        //        OnPropoertyChanged("Current");  // wysyła sygnał żeby się oświerzyły wszystkie konrolki które nasłuchują właściwości Current
        //    }
        //}
        #endregion

        public float SettingsVoltageHighLimit { get; } = 40; // właściwości  gdy nie ma set jest to wartość tylko do odczytu
        public float SettingsVoltageLowLimit { get; } = 0;
        public float SettingsCurrentHighLimit { get; } = 60; // właściwości gdy nie ma set jest to wartość tylko do odczytu
        public float SettingsCurrentLowLimit { get; } = 0;

        #region SettingsVoltage 

        private float settingsVoltage;
        public float SettingsVoltage
        {
            get { return settingsVoltage; }
            set
            {
                //walidacja zakresu
                if (value < SettingsVoltageLowLimit)
                    settingsVoltage = SettingsVoltageLowLimit;
                else
                    if (value > SettingsVoltageHighLimit)
                    settingsVoltage = SettingsVoltageHighLimit;
                else
                    settingsVoltage = value;

                OnPropoertyChanged(nameof(SettingsVoltage)); //nameof pobiera nazwy obiektów nie ma połyłki w literówkach
            }
        }

        #endregion

        #region SettingsCurrent 

        private float settingsCurrent;
        public float SettingsCurrent
        {
            get { return settingsCurrent; }
            set
            {
                //walidacja zakresu
                if (value < SettingsCurrentLowLimit)
                    settingsCurrent = SettingsCurrentLowLimit;
                else
                    if (value > SettingsCurrentHighLimit)
                    settingsCurrent = SettingsCurrentHighLimit;
                else
                    settingsCurrent = value;

                OnPropoertyChanged(nameof(SettingsCurrent)); //nameof pobiera nazwy obiektów nie ma połyłki w literówkach
            }
        }

        #endregion

       

       

      


        //  public float Power { get; set; } //Właściwości 

        private IAnalaogInput VoltageService;  //tworzymy prywatną zmienną która przechowuje obiekt Voltomierza

        private IAnalaogInput CurrentService;

        private IVoltageOutputService VoltageOutputService;

        private ICurrentOutputService CurrentOutputService;

        private IOutputService OutputService;

        private IVoltageInputService SupplierVoltageInputService;

        private ICurrentInputService SupplierCurrentInputService;

        private IMeasuresServices MeasuresService;

        private IthreePhaseMeasureService ThreePhaseMeasureService;

        //tworzenie zmiennej interfejsu 
        private IFrequencyInputService FrequencyInputService;

        private IThreePhaseInputService ThreePhaseINputService;

        

        private Timer timer;

        #region PowerSwitchCommand

        private ICommand _PowerSwitchCommand;

        public ICommand PowerSwitchCommand
        {
            get
            {
                if(_PowerSwitchCommand == null)
                {
                    _PowerSwitchCommand = new RelayCommand(PowerSwitch);
                }

                return _PowerSwitchCommand;
            }
        }

        private void PowerSwitch()
        {
            if(IsPowerOn)
            {
                OutputService.On();
            }
            else
            {
                OutputService.Off();
            }
        }
        #endregion


        #region SetCommand

        //Wzorzec projektowy do obsługi kontrolek
        private ICommand _SetCommand;  //prywatna zmienna

        public ICommand SetCommand //właściwość
        {
            get
            {
                if(_SetCommand == null)
                {
                    _SetCommand = new RelayCommand(Set, CanSet); // wywołuje metody poprzez wskaźniki do funkcji
                }

                return _SetCommand;
            }
        }

        private void Set()
        {
            //TODO: sterowanie sprzętem
            VoltageOutputService.Set(SettingsVoltage);
            CurrentOutputService.Set(SettingsCurrent);
            

        }

        private bool CanSet()
        {
            //TODO: warunek sterowania sprzętem
            return this.SettingsVoltage > 0 && SettingsCurrent > 0;
        }

        #endregion

        

        

        



        #region SaveCommand

        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if(_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(Save);
                }
                return _SaveCommand;
            }
        }

        private void Save()
        {
            

            MeasuresService.Add(this.Measure);
                       
        }


        #endregion

        #region SaveThreePhaseMeasureCommand

        private ICommand _SaveThreePhaseMeasureCommand;

        public ICommand SaveThreePhaseMeasureCommand
        {
            get
            {
                if (_SaveThreePhaseMeasureCommand == null)
                {
                    _SaveThreePhaseMeasureCommand = new RelayCommand(SaveThreePhaseMeasure);
                }

                return _SaveThreePhaseMeasureCommand;
            }
        }

        private void SaveThreePhaseMeasure()
        {
            //przekazujemy jako obiektwłaściwość klasy ThreePhaseMeasure

            ThreePhaseMeasureService.Add(ThreePhaseMeasure);

        }


        #endregion

        #region Measures

        private List<Measure> _Measures;
        public List<Measure> Measures
        {
            get { return _Measures; }
            set
            {
                _Measures = value;

                OnPropoertyChanged(nameof(Measures));
            }
        }
        #endregion


        #region Search

        private ICommand _SearchCommand;
        
        public ICommand SearchCommand
        {
            get
            {
                if(_SearchCommand == null)
                {
                    _SearchCommand = new RelayCommand(Search);
                }
                return _SearchCommand;
            }
        }

        private void Search()
        {
            Measures = MeasuresService.Get(MeasureSearchCriteria);
        }

       

        #endregion


        //konstruktor klasy głównej
        public PowerSupplierViewModel()
        {
            //VoltageService = new MockVoltageService(); //inicjalizacja implementacji wirtualnego urządzenia

            VoltageService = new N30HVoltageService(); //inicjalizacja implementacji fizycznego urządzenia

            CurrentService = new N30UCurrentService();

            VoltageOutputService = new SupplierService();

            CurrentOutputService = new SupplierService();

            OutputService = new SupplierService();

            SupplierVoltageInputService = new SupplierService();

            SupplierCurrentInputService = new SupplierService();

            //MeasuresService = new FileMeasuresService("Wyniki.csv");

            MeasuresService = new DbMeasuresService();

            ThreePhaseMeasureService = new DbThreePhaseMeasureService();

            Voltages = new ObservableCollection<float>();

            MeasureSearchCriteria = new MeasureSearchCriteria();

            FrequencyInputService = new N10Service();

            ThreePhaseINputService = new N10Service();

           




            //Current = CurrentService.Get();

            //Voltage = VoltageService.Get(); // pobranie wartości z miernika wirtualnego

            timer = new Timer(1000);

            timer.Elapsed += Timer_Elapsed; //wywołuje zdarzenie co 1000ms

            timer.Enabled = true;



            //Voltage = 2.4f;

            //Current = 3.0f;

            //Power = 15.0f;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsPowerOn = OutputService.IsOn();

            //ELIsPowerON = ElectronicLoadService.IsOn();


            //Voltage = VoltageService.Get();

            //Current = CurrentService.Get();

            //Voltages.Add(Voltage);

            //przypisanie pobranej wartości do właściwości publicznej FrequencyInput

            FrequencyInput = FrequencyInputService.Get();

            ThreePhaseMeasure = ThreePhaseINputService.Get();

            var voltage = VoltageService.Get();
            var current = CurrentService.Get();
            var power = 0;

            this.Measure = new Measure(DateTime.Now, voltage, current, power );

            this.Measure.Voltage = VoltageService.Get();
            this.Measure.Current = CurrentService.Get();
            
            SupplierVoltage = SupplierVoltageInputService.Get();

            SupplierCurrent = SupplierCurrentInputService.Get();

            Voltages.Add(this.Measure.Voltage);

        }

        






    }
}
