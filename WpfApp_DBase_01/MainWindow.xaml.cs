using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace WpfApp_DBase_01
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //SQLiteConnection poloczenie = new SQLiteConnection(string.Format("Data Source={0}", Path.Combine(Application.StartupPath, "kursbaza.db")));
        //SQLiteCommand komenda;
        //string zapytanieSQL = "";

        SqlConnection polaczenie;
        SqlCommand komenda;

        public event PropertyChangedEventHandler PropertyChanged; //deklaracja zdarzenia 

        public void OnPropoertyChanged(string propertyName)  //metoda do odświerzania wyświetlanej wartości właściwości jeśli ulegną zmianie
        {
            if (PropertyChanged != null)  // sprawdzamy czy ktoś słucha
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));   //generuje zdarzenie
            }
        }



        private List<Table1> _dane;

        public List<Table1> Dane
        {
            get { return _dane; }
            set
            {
               _dane = value;
                OnPropoertyChanged(nameof(Dane));
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            polaczenie = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\WitekM\Documents\WpfApp_Dbase_01.mdf;Integrated Security=True;Connect Timeout=30");
            polaczenie.Open();
            disp_data();
            polaczenie.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            polaczenie.Open();
            komenda = polaczenie.CreateCommand();
            komenda.CommandType = CommandType.Text;
            komenda.CommandText = "insert into Table1 (name, city, country) values('"+TextBox_name.Text+"', '"+TextBox_City.Text+"', '"+TextBox_Country.Text+"')";
            komenda.ExecuteNonQuery();
            if(polaczenie.State == ConnectionState.Open)
            {
                MessageBox.Show("Polączenie z bazą danych", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show("Record inserter sucesfully", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            polaczenie.Close();
            
        }

        public void disp_data()
        {
           // polaczenie.Open();
            komenda = polaczenie.CreateCommand();
            komenda.CommandType = CommandType.Text;
            komenda.CommandText = "select * from Table1";
            komenda.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komenda);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Table1Data.ItemsSource = ds.Tables[0].DefaultView;  
        }

    }
}
