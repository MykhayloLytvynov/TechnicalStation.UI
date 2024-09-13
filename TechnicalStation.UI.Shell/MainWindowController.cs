using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.Control;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.VewModel;
using System.Collections.ObjectModel;
using TechnicalStation.UI.Control.Customer;
using TechnicalStation.UI.Control.Worker;

namespace TechnicalStation.UI.Shell
{
    public partial class MainWindowController : IMainWindowController
    {
        private MainWindow mainWindow;

        private ControlManager controlManager;

        private IApplication application;
        private AddWorkViewModel addWorkViewModel;
        IComboBoxItemsAdder comboBoxItemsAdder;
        private OrderInfo currentOrderInfo;
        private List<OrderInfo> orderInfoCollectionForFilter = new List<OrderInfo>();
        private ObservableCollection<OrderInfo> orderInfoObservableCollection = new ObservableCollection<OrderInfo>();
        private ObservableCollection<WorkerInfo> workerInfoObservableCollection = new ObservableCollection<WorkerInfo>();
        public MainWindowController(IApplication application)
        {
            this.application = application;
            this.mainWindow = new MainWindow();
            this.controlManager = new ControlManager();
        }

        public void RegisterControls(IControlFactory controlFactory)
        {
            this.controlManager.Register("MainWindow", mainWindow);
            //this.controlManager.Register("LoginControl", controlFactory.Create<LoginControl>());
            this.controlManager.Register("DashboardControl", controlFactory.Create<DashboardControl>());

            controlManager.Register("OrderEditorControl", controlFactory.Create<OrderEditorControl>());
            controlManager.Register("AddOrderControl", controlFactory.Create<AddOrderControl>());
            controlManager.Register("OrderFilterControl", controlFactory.Create<OrderFilterControl>());

            controlManager.Register("AddWorkControl", controlFactory.Create<AddWorkControl>());

            controlManager.Register("AddCarControl", controlFactory.Create<AddCarControl>());

            controlManager.Register("AddCustomerControl", controlFactory.Create<AddCustomerControl>());
            
            controlManager.Register("WorkerEditorControl", controlFactory.Create<WorkerEditorControl>());
            //controlManager.Register("ContentOrderControl", controlFactory.Create<ContentOrderControl>());
            controlManager.Register("AddWorkerControl", controlFactory.Create<AddWorkerControl>());
            controlManager.Register("CustomerEditorControl", controlFactory.Create<CustomerEditorControl>());
         
        }

        public void LoadLogin()
        {

            #region Main window definition

            mainWindow.WindowStyle = WindowStyle.None;
            mainWindow.WindowState = WindowState.Normal;
            this.mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.mainWindow.Show();

            #endregion

            controlManager.Place("MainWindow", "MainRegion", "LoginControl");
        }

        public void Exit()
        {
            this.application.Exit();
        }

        public void LoadDashboard()
        {
            //Configure Main window

            //Window mainWindow = this.controlManager.GetControl("MainWindow") as Window;
            //mainWindow.WindowState = WindowState.Maximized;
            //mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            #region Main window definition

            mainWindow.WindowStyle = WindowStyle.None;
            mainWindow.WindowState = WindowState.Normal;
            this.mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.mainWindow.Show();

            #endregion

            //this.controlManager.Place("MainWindow", "MainRegion", "DashboardControl");

            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    mainWindow.WindowState = WindowState.Maximized;
                    mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                }));

            this.controlManager.Place("MainWindow", "MainRegion", "DashboardControl");

            //LoadEditorControl();
            //this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");
        }

        public void RemoveFromList(OrderInfo orderInfo)
        {
            orderInfoCollectionForFilter.Remove(orderInfo);
        }

        

        public void LoadOrderFilterControl()
        {
            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderFilterControl");
        }

        public void LoadEditorControl()
        {

            this.controlManager.Place("DashboardControl", "EditControlRegion", "OrderEditorControl");

        }



        //public void LoadAddWorkerControl(WorkerInfo workerInfo) 
        //{
        //    AddWorkerControl addWorkerCollectionControl = this.controlManager.GetControl("AddWorkerControl") as AddWorkerControl;
        //    //addOrderControl.SetFocus();
        //    this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
        //        new Action(() =>
        //        {
        //            addWorkerCollectionControl.addWorkerViewModel.LoadWorkerViewModel();
        //        }));

        //    this.controlManager.Place("DashboardControl", "EditControlRegion", "AddWorkerControl");
        //}

        //public void LoadAddCustomerControl(CustomerInfo customerInfo)
        //{
        //    AddCustomerControl addWorkCollectionControl = this.controlManager.GetControl("AddCustomerControl") as AddCustomerControl;
        //    //addOrderControl.SetFocus();
        //    //this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
        //    //    new Action(() =>
        //    //    {
        //    //        addWorkCollectionControl.addCustomerViewModel.CustomerIdValue = orderId;
        //    //    }));

        //    this.controlManager.Place("DashboardControl", "EditControlRegion", "AddCustomerControl");
        //}

        public void LoadAddCarControl(CarInfo carInfo)
        {
            AddCarControl addOrderControl = this.controlManager.GetControl("AddCarControl") as AddCarControl;
            //addOrderControl.SetFocus();
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    //orderInfo.Id = currentOrderInfo.Id;
                    addOrderControl.addCarViewModel.CarViewModel.Load(carInfo);
                }));

            this.controlManager.Place("DashboardControl", "EditControlRegion", "AddCarControl");
        }






        //public void LoadContentWorkerControl(WorkerInfo workerInfoResult) {
        //    WorkerEditorControl editControl = this.controlManager.GetControl("WorkerEditorControl") as WorkerEditorControl;
        //    //EditControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
        //    this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
        //        new Action(() =>
        //        {
        //            try
        //            {
        //                int orderId = workerInfoResult.Id;
        //                //orderInfoCollectionForFilter.Add(workerInfoResult);
        //                workerInfoObservablollection.Add(workerInfoResult);
        //                //comboBoxItemsAdder.SetCollectionValue(orderInfoObservableCollection);
        //                editControl.editorViewModel.Add(workerInfoResult);
        //            }
        //            catch (Exception ex)
        //            {
        //                int i = 0;
        //            }
        //        }));

        //    //this.controlManager.Clear("DashboardControl", "AddFuncRegion");
        //    this.controlManager.Place("DashboardControl", "EditControlRegion", "WorkerEditorControl");
        //}



        void LoadContentWorkCollectionControl(WorkInfo workInfoResult)
        {
            //ContentOrderControl сontentOrderControl =
            //    this.controlManager.GetControl("ContentOrderControl") as ContentOrderControl;
            //сontentOrderControl.SetFocus();
            OrderEditorControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            //EditControl editControl = this.controlManager.GetControl("OrderEditorControl") as OrderEditorControl;
            this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    try
                    {
                        int workId = workInfoResult.Id;
                        //orderInfoCollectionForFilter.Add(workInfoResult);
                        //editControl.editorViewModel.AddWork(workInfoResult);
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

        //public void LoadContentWorkerControl(){
        //    WorkerEditorControl workerEditorControl = this.controlManager.GetControl("WorkerEditorControl") as WorkerEditorControl;
        //    this.controlManager.Place("DashboardControl", "EditControlRegion", "WorkerEditorControl");
        //}

        //public void LoadUpdateWorkerControl(WorkerInfo workerInfo)
        //{
        //    AddWorkerControl addWorkerCollectionControl = this.controlManager.GetControl("AddWorkerControl") as AddWorkerControl;
        //    //addOrderControl.SetFocus();
        //    this.mainWindow.Dispatcher.Invoke(DispatcherPriority.Normal,
        //        new Action(() =>
        //        {
        //            addWorkerCollectionControl.addWorkerViewModel.Load(workerInfo);
        //        }));

        //    //this.controlManager.Clear("DashboardControl", "EditControlRegion");
        //    this.controlManager.Place("DashboardControl", "EditControlRegion", "AddWorkerControl");
        //}

        public void LoadWorkCollectionControl(List<OrderInfo> orderInfoCollection)
        {
            throw new NotImplementedException();
        }
    }
}
