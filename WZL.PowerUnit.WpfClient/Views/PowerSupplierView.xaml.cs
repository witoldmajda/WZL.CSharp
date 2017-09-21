using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WZL.PowerUnit.WpfClient.ViewModels;

namespace WZL.PowerUnit.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for PowerSupplierView.xaml
    /// </summary>
    public partial class PowerSupplierView : Window
    {
        public PowerSupplierView()
        {
            InitializeComponent();

            CommonViewModel commonVM = new CommonViewModel();

            this.DataContext = commonVM;

            //ElectronicLoadViewModel ELLoad = new ElectronicLoadViewModel();

            //PowerSupplierViewModel PSVM = new PowerSupplierViewModel();

            //CommonViewModel CVVM = new CommonViewModel();

            //this.DataContext = CVVM;

            //this.DataContext = ELLoad;

            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
