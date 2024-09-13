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
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.Control
{
    /// <summary>
    /// Interaction logic for AddOrderControl.xaml
    /// </summary>
    public partial class AddOrderControl : UserControl
    {
        public AddOrderViewModel addOrderViewModel;
        public AddOrderControl(AddOrderViewModel addOrderViewModel)
        {
            this.addOrderViewModel = addOrderViewModel;
            InitializeComponent();
            this.DataContext = this.addOrderViewModel;
        }

    }
}
