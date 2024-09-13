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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechnicalStation.UI.VewModel;

namespace TechnicalStation.UI.Control
{
    /// <summary>
    /// Interaction logic for AddCustomerControl.xaml
    /// </summary>
    public partial class AddCustomerControl : UserControl
    {
        public AddCustomerViewModel addCustomerViewModel;
        public AddCustomerControl(AddCustomerViewModel addCustomerViewModel)
        {
            this.addCustomerViewModel = addCustomerViewModel;
            InitializeComponent();
            this.DataContext = this.addCustomerViewModel;
        }
    }
}
