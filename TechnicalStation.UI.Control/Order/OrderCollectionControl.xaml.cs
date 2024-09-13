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
    /// Interaction logic for OrderCollectionControl.xaml
    /// </summary>
    public partial class OrderCollectionControl : UserControl
    {
        public OrderCollectionControl()
        {
            // https://stackoverflow.com/questions/5409259/binding-itemssource-of-a-comboboxcolumn-in-wpf-datagrid
            InitializeComponent();
        }
    }
}
