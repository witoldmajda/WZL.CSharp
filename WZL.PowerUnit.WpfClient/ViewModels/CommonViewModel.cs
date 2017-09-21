using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WZL.PowerUnit.WpfClient.ViewModels
{
    public class CommonViewModel : BaseviewModel
    {
        public CommonViewModel()
        {
            
            
        }
        PowerSupplierViewModel PSVM1 = new PowerSupplierViewModel();

        ElectronicLoadViewModel ELLoad1 = new ElectronicLoadViewModel();

        private PowerSupplierViewModel _PSVM;
        public PowerSupplierViewModel PSVM
        {
            get
            {
                return this.PSVM1;
            }
        }

        private ElectronicLoadViewModel _ELLoad;
        public ElectronicLoadViewModel ELLoad
        {
            get
            {
                return this.ELLoad1;
            }
        }
    }
}
