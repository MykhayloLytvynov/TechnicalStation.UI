using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Control;

namespace TechnicalStation.UI.Shell
{
    public partial class MainWindowController
    {
        public void LoadContentWorkCollectionControl()
        {
            OrderEditorControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() =>
            {           
                //orderInfo.Id = currentOrderInfo.Id;
                var selectedOrder = editControl.editorViewModel.OrderCollectionViewModel.SelectedOrder;
                editControl.editorViewModel.OrderCollectionViewModel.SelectedOrder = selectedOrder;
            }));

            this.controlManager.Clear("DashboardControl", "AddFuncRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");
        }

        public void LoadAddWorkControl(int orderId)
        {
            AddWorkControl addWorkCollectionControl = this.controlManager.GetControl("AddWorkControl") as AddWorkControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addWorkCollectionControl.addWorkViewModel.LoadWorkViewModel(orderId);
                }));

            this.controlManager.Clear("DashboardControl", "EditControlRegion");
            this.controlManager.Place("DashboardControl", "AddFuncRegion", "AddWorkControl");
        }

        public void LoadUpdateWorkControl(WorkInfo workInfo, List<WorkerInfo> workerInfoCollection)
        {
            AddWorkControl addWorkCollectionControl = this.controlManager.GetControl("AddWorkControl") as AddWorkControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addWorkCollectionControl.addWorkViewModel.Load(workInfo, workerInfoCollection);
                }));

            this.controlManager.Clear("DashboardControl", "EditControlRegion");
            this.controlManager.Place("DashboardControl", "AddFuncRegion", "AddWorkControl");
        }

    }
}
