using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Control;
using TechnicalStation.UI.Control.Customer;

namespace TechnicalStation.UI.Shell
{
    public partial class MainWindowController
    {
        public void LoadContentCarCollectionControl()
        {
            CustomerEditorControl editControl = this.controlManager.GetControl("CustomerEditorControl") as CustomerEditorControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action(() =>
            {
                //orderInfo.Id = currentOrderInfo.Id;
                var selectedCustomer = editControl.editorViewModel.CustomerCollectionViewModel.SelectedCustomer;
                editControl.editorViewModel.CustomerCollectionViewModel.SelectedCustomer = selectedCustomer;
            }));

           // this.controlManager.Clear("DashboardControl", "AddFuncRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "CustomerEditorControl");
        }

        public void LoadAddCarControl(int customerId)
        {
            AddCarControl addCarCollectionControl = this.controlManager.GetControl("AddCarControl") as AddCarControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addCarCollectionControl.addCarViewModel.LoadCarViewModel(customerId);
                }));

           //this.controlManager.Clear("DashboardControl", "EditControlRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddCarControl");
        }

        public void LoadUpdateCarControl(CarInfo carInfo)
        {
            AddCarControl addCarCollectionControl = this.controlManager.GetControl("AddCarControl") as AddCarControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addCarCollectionControl.addCarViewModel.Load(carInfo);
                }));

            //this.controlManager.Clear("DashboardControl", "EditControlRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddCarControl");
        }

    }
}
