﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp_DBase_01.Models;

namespace WpfApp_DBase_01.ViewModels
{
    public class WPF_ViewModel : BaseViewModel
    {
        SqlConnection polaczenie;
        SqlCommand komenda;

        private DbServices Services;



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


        private string _T_Name;
        public string T_Name
        {
            get { return _T_Name;  }
            set
            {
              _T_Name = value;
                OnPropoertyChanged(nameof(T_Name));
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

       
        private void Add()
        {
            //polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Dokumenty\WPF_DBASE.mdf;Integrated Security=True;Connect Timeout=30"); // DELL
            polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WitekM\Source\Repos\WZLCSharp\WpfApp_DBase_01\WPF_DBASE.mdf;Integrated Security=True;Connect Timeout=30"); // Samsung
            polaczenie.Open();
            komenda = polaczenie.CreateCommand();
            komenda.CommandType = CommandType.Text;
            komenda.CommandText = "insert into Person (Name, Surname, City) values('" + T_Name + "', '" + T_Surname + "', '" + T_City + "')";
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
            Services = new DbServices();

            Persons = Services.Get();
           

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
