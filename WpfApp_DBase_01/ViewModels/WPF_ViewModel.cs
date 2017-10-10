using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WpfApp_DBase_01.Models;
using WpfApp_DBase_01;
using WpfApp_DBase_01.Views;


namespace WpfApp_DBase_01.ViewModels
{
    public class WPF_ViewModel : BaseViewModel
    {
        SqlConnection polaczenie;
        SqlCommand komenda;

        private DbServices Services = new DbServices();

        //tworzenie zmiennej interfejsu
        private Timer timer;

      //  Services = new DbServices();

        private ICommand _AddCommand;

        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(Add);
                }
                return _AddCommand;
            }
        }

        private ICommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if(_EditCommand == null)
                {
                    _EditCommand = new RelayCommand(Edit);
                }
                return _EditCommand;
            }
        }


        private List<PersonModel> _Persons;
        public List<PersonModel> Persons
        {
            get { return _Persons; }
            set
            {
                _Persons = value;

                OnPropoertyChanged(nameof(Persons));
            }
        }


        private string _T_Name;
        public string T_Name
        {
            get { return _T_Name;  }
            set
            {
              _T_Name = value;
                OnPropoertyChanged(nameof(T_Name));
                if(value != _T_Name)
                {
                    this.AddIsEnabled = true;
                }
            }
            
        }

        private string _T_Surname;
        public string T_Surname
        {
            get { return _T_Surname; }
            set
            {
                _T_Surname = value;
                OnPropoertyChanged(nameof(T_Surname));
            }

        }

        private string _T_City;
        public string T_City
        {
            get { return _T_City;  }
            set
            {
              _T_City = value;
                OnPropoertyChanged(nameof(T_City));
            }
            
        }

        private PersonModel _PersonModel;
        public PersonModel PersonModel
        {
            get
            {
                return _PersonModel;
            }
            set
            {
                _PersonModel = value;
                OnPropoertyChanged(nameof(PersonModel));
            }
        }

        private PersonModel _sellectedPerson;
        public PersonModel SellectedPerson
        {
            get
            {
                return _sellectedPerson;                
            }
            set
            {
                if(_sellectedPerson != value)
                {
                    _sellectedPerson = value;
                    OnPropoertyChanged(nameof(SellectedPerson));
                    this.EditIsEnabled = true;
                }
            }
        }

        //UnSellectedPerson

        private PersonModel _UnSellectedPerson;
        public PersonModel UnSellectedPerson
        {
            get
            {
                return _UnSellectedPerson;
            }
            set
            {
                if (_UnSellectedPerson != value)
                {
                    _UnSellectedPerson = value;
                    OnPropoertyChanged(nameof(UnSellectedPerson));
                    this.EditIsEnabled = true;
                }
            }
        }

        //public ICommand UnSellectedPerson => new RelayCommand(() => Unselected());

        //private void Unselected()
        //{
        //    this.EditIsEnabled = false;
        //}


        private bool _AddIsEnabled;
        public bool AddIsEnabled
        {
            get
            {
                return _AddIsEnabled;
            }
            set
            {
                _AddIsEnabled = value;
                OnPropoertyChanged(nameof(AddIsEnabled));

            }
        }

        private bool _SaveIsEnabled;
        public bool SaveIsEnabled
        {
            get
            {
                return _SaveIsEnabled;
            }
            set
            {
                _SaveIsEnabled = value;
                OnPropoertyChanged(nameof(SaveIsEnabled));

            }
        }

        private bool _DeleteIsEnabled;
        public bool DeleteIsEnabled
        {
            get
            {
                return _DeleteIsEnabled;
            }
            set
            {
                _DeleteIsEnabled = value;
                OnPropoertyChanged(nameof(DeleteIsEnabled));

            }
        }

        private bool _EditIsEnabled;
        public bool EditIsEnabled
        {
            get
            {
                return _EditIsEnabled;
            }
            set
            {
                _EditIsEnabled = value;
                OnPropoertyChanged(nameof(EditIsEnabled));

            }
        }

        private bool _Details;
        public bool Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
                OnPropoertyChanged(nameof(Details));                
            }
        }

        private bool _TextBoxIsVisible;
        public bool TextBoxIsVisible
        {
            get
            {
                return _TextBoxIsVisible;
            }
            set
            {
                _TextBoxIsVisible = value;
                OnPropoertyChanged(nameof(TextBoxIsVisible));
                if(_TextBoxIsVisible == true)
                {
                    this.EditIsEnabled = true;
                }
                
            }
        }

        //private void Add()
        //{
        //    //polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Dokumenty\WPF_DBASE.mdf;Integrated Security=True;Connect Timeout=30"); // DELL
        //    polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WitekM\Source\Repos\WZLCSharp\WpfApp_DBase_01\WPF_DBASE.mdf;Integrated Security=True;Connect Timeout=30"); // Samsung
        //    polaczenie.Open();
        //    komenda = polaczenie.CreateCommand();
        //    komenda.CommandType = CommandType.Text;
        //    komenda.CommandText = "insert into Person (Name, Surname, City) values('" + T_Name + "', '" + T_Surname + "', '" + T_City + "')";
        //    komenda.ExecuteNonQuery();
        //    if (polaczenie.State == ConnectionState.Open)
        //    {
        //        MessageBox.Show("Polączenie z bazą danych", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    MessageBox.Show("Record inserter sucesfully", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        //    polaczenie.Close();
        //}

        private void Add()
        {
            this.PersonModel = new PersonModel(T_Name, T_Surname, T_City);
            Services.Add(this.PersonModel);
                
        }

        
        //przypisanie zmiennych z bazy danych do elementów formularza
        private void Edit()
        {
            this.PersonModel = Services.Edit(SellectedPerson.Id);
            this.T_Name = this.PersonModel.Name;  // zamiennie this.T_Name = SellectedPerson.Name;
            this.T_Surname = this.PersonModel.Surname;
            this.T_City = this.PersonModel.City;
            this.AddIsEnabled = false;
            this.SaveIsEnabled = true;
            this.DeleteIsEnabled = true;
            this.EditIsEnabled = true;



        }
        

        public WPF_ViewModel()
        {

            //timer = new Timer(1000);
            //timer.Elapsed += Timer_Elapsed;
            //timer.Enabled = true;
            this.AddIsEnabled = true;
            this.SaveIsEnabled = false;
            this.DeleteIsEnabled = false;
            this.EditIsEnabled = false;

            Persons = Services.Get();

           

            //polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WitekM\Documents\WpfApp_Dbase_01.mdf;Integrated Security=True;Connect Timeout=30");
            //polaczenie.Open();
            //disp_data();
            //polaczenie.Close();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Persons = Services.Get();
        }


            //    polaczenie.Open();
            //        komenda = polaczenie.CreateCommand();
            //        komenda.CommandType = CommandType.Text;
            //        komenda.CommandText = "insert into Table1 (name, city, country) values('"+TextBox_name.Text+"', '"+TextBox_City.Text+"', '"+TextBox_Country.Text+"')";
            //        komenda.ExecuteNonQuery();
            //        if(polaczenie.State == ConnectionState.Open)
            //        {
            //            MessageBox.Show("Polączenie z bazą danych", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            //        }
            //MessageBox.Show("Record inserter sucesfully", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            //        polaczenie.Close();


            //public void disp_data()
            //{
            //    // polaczenie.Open();
            //    komenda = polaczenie.CreateCommand();
            //    komenda.CommandType = CommandType.Text;
            //    komenda.CommandText = "select * from Table1";
            //    komenda.ExecuteNonQuery();
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(komenda);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    Table1Data.ItemsSource = ds.Tables[0].DefaultView;
            //}
        }
    }
