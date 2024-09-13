using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void LoadContentCustomerControl()
        {
            //this.ClearRegions();
            this.controlManager.Place("DashboardControl", "EditControlRegion", "CustomerEditorControl");
        }

        //protected void ClearRegions()
        //{
        //    this.controlManager.Clear("DashboardControl", "AddFuncRegion");
        //    this.controlManager.Clear("DashboardControl", "EditControlRegion");
        //}

        public void LoadAddCustomerControl(CustomerInfo customerInfo)
        {
            AddCustomerControl addCustomerControl = this.controlManager.GetControl("AddCustomerControl") as AddCustomerControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    addCustomerControl.addCustomerViewModel.CustomerViewModel.Load(customerInfo);
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddCustomerControl");
        }

        public void LoadContentCustomerControl(List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            CustomerEditorControl customerEditControl = this.controlManager.GetControl("CustomerEditorControl") as CustomerEditorControl;

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        
                        customerEditControl.editorViewModel.Load(carInfoCollection, customerInfoCollection);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "CustomerEditorControl");

        }

        public void LoadContentCustomerControl(CustomerInfo customerInfo)
        {
            CustomerEditorControl editControl = this.controlManager.GetControl("CustomerEditorControl") as CustomerEditorControl;
            //EditControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        int customerId = customerInfo.Id;
                        //orderInfoCollectionForFilter.Add(customerInfo);
                        //orderInfoObservableCollection.Add(customerInfo);

                        editControl.editorViewModel.AddCustomer(customerInfo);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            //this.controlManager.Clear("DashboardControl", "AddFuncRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "CustomerEditorControl");

        }

        

        //public ObservableCollection<CustomerInfo> GetCustomerInfoObsCollection()
        //{
        //    return this.customerInfoObservableCollection;
        //}
    }
}

