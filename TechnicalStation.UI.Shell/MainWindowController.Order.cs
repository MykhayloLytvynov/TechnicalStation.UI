using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void LoadContentOrderControl()
        {
            this.ClearRegions();
            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");
        }

        protected void ClearRegions()
        {
            this.controlManager.Clear("DashboardControl", "AddFuncRegion");
            this.controlManager.Clear("DashboardControl", "EditControlRegion");
        }

        public void LoadAddOrderControl(OrderInfo orderInfo, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            AddOrderControl addOrderControl = this.controlManager.GetControl("AddOrderControl") as AddOrderControl;
            
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    //orderInfo.Id = currentOrderInfo.Id;
                    addOrderControl.addOrderViewModel.OrderViewModel.Load(orderInfo, customerInfoCollection, carInfoCollection);
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddOrderControl");

        }

        public void LoadFilteredOrderCollection(List<OrderInfo> orderFilteredCollection, 
            List<CarInfo> carInfoCollection, 
            List<CustomerInfo> customerInfoCollection)
        {
            //ContentOrderControl сontentOrderControl =
            //    this.controlManager.GetControl("ContentOrderControl") as ContentOrderControl;
            //сontentOrderControl.SetFocus();o
           OrderEditorControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            //EditControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        editControl.editorViewModel.Load(orderFilteredCollection, carInfoCollection, customerInfoCollection);
                        //сontentOrderViewModel.Add(orderInfo);
                        //int index = сontentOrderViewModel.OrderViewModelCollection.Count - 1;
                        //сontentOrderControl.SetFocus(index);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));


            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");

        }

        public void LoadContentOrderControl(List<OrderInfo> orderInfoCollection, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
           OrderEditorControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        orderInfoCollectionForFilter = orderInfoCollection;
                        editControl.editorViewModel.Load(orderInfoCollection, carInfoCollection, customerInfoCollection);
                        //сontentOrderViewModel.Add(orderInfo);
                        //int index = сontentOrderViewModel.OrderViewModelCollection.Count - 1;
                        //сontentOrderControl.SetFocus(index);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");

        }

        public void LoadContentOrderControl(OrderInfo orderInfo)
        {
           OrderEditorControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            //EditControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        int orderId = orderInfo.Id;
                        orderInfoCollectionForFilter.Add(orderInfo);
                        orderInfoObservableCollection.Add(orderInfo);
                        //comboBoxItemsAdder.SetCollectionValue(orderInfoObservableCollection);
                        editControl.editorViewModel.AddOrder(orderInfo);
                    }
                    catch (Exception ex)
                    {
                        int i = 0;
                    }
                }));

            this.controlManager.Clear("DashboardControl", "AddFuncRegion");
            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");

        }

        public List<OrderInfo> GetOrderInfoCollection()
        {
            return this.orderInfoCollectionForFilter;
        }

        public ObservableCollection<OrderInfo> GetOrderInfoObsCollection()
        {
            return this.orderInfoObservableCollection;
        }
    }
}
