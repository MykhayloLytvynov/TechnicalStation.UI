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
using TechnicalStation.UI.VewModel.Customer;

namespace TechnicalStation.UI.Control.Customer
{
    /// <summary>
    /// Interaction logic for CustomerEditorControl.xaml
    /// </summary>
    public partial class CustomerEditorControl : UserControl
    {
        public CustomerEditorViewModel editorViewModel;
        public CustomerEditorControl(CustomerEditorViewModel editorViewModel)
        {
            this.editorViewModel = editorViewModel;
            InitializeComponent();
            this.DataContext = editorViewModel;
        }
    }
}
