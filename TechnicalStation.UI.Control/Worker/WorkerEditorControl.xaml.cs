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
using TechnicalStation.UI.VewModel.Worker;

namespace TechnicalStation.UI.Control.Worker
{
    /// <summary>
    /// Interaction logic for WorkerEditorControl.xaml
    /// </summary>
    public partial class WorkerEditorControl : UserControl
    {
        public WorkerEditorViewModel editorViewModel;
        public WorkerEditorControl(WorkerEditorViewModel editorViewModel)
        {
            this.editorViewModel = editorViewModel;
            InitializeComponent();
            this.DataContext = this.editorViewModel;
        }
    }
}
