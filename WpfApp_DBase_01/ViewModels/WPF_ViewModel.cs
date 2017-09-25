using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp_DBase_01.ViewModels
{
    public class WPF_ViewModel : BaseViewModel
    {
        SqlConnection polaczenie;
        SqlCommand komenda;

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


        private string _Name;
        public string Name
        {
            get { return _Name;  }
            set
            {
              _Name = value;
                OnPropoertyChanged(nameof(Name));
            }
            
        }

        private string _Surname;
        public string Surname
        {
            get { return _Surname; }
            set
            {
                _Surname = value;
                OnPropoertyChanged(nameof(Surname));
            }

        }

        private string _City;
        public string City
        {
            get { return _City;  }
            set
            {
              _City = value;
                OnPropoertyChanged(nameof(City));
            }
            
        }

       
        private void Add()
        {
            polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Dokumenty\WPF_DBASE.mdf;Integrated Security=True;Connect Timeout=30");
            polaczenie.Open();
            komenda = polaczenie.CreateCommand();
            komenda.CommandType = CommandType.Text;
            komenda.CommandText = "insert into Person (Name, Surname, City) values('" + Name + "', '" + Surname + "', '" + City + "')";
            komenda.ExecuteNonQuery();
            if (polaczenie.State == ConnectionState.Open)
            {
                MessageBox.Show("Polączenie z bazą danych", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show("Record inserter sucesfully", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            polaczenie.Close();
        }









        public WPF_ViewModel()
        {
            //polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WitekM\Documents\WpfApp_Dbase_01.mdf;Integrated Security=True;Connect Timeout=30");
            //polaczenie.Open();
            //disp_data();
            //polaczenie.Close();
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
