using System.Collections.Generic;
using TechnicalStation.UI.Contract;
using Common.UI.Utility.Commands;
using TechnicalStation.Service.Client.Contract;
using System.Windows.Input;
using System.Threading.Tasks;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.UI.ViewModel
{
    public partial class DashboardViewModel
    {
        public IMainWindowController mainWindowController;
        IFrontServiceClient frontServiceClient;
        private string serviceUrl;
        //int orderId = 0;
        //int carId = 0;
        //int customerId = 0;

       

        public DashboardViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient, string serviceUrl)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            this.serviceUrl = serviceUrl;
        }

        protected RelayCommand loadWorkersCommand;

        public ICommand LoadWorkersCommand
        {
            get
            {
                if (this.loadWorkersCommand == null)
                {
                    this.loadWorkersCommand = new RelayCommandAsync(() => this.LoadWorkersAsync(), () => this.CanLoadWorkers());
                }

                return this.loadWorkersCommand;
            }
        }

        protected async Task LoadWorkersAsync()
        {
            //string login = "login";
            //string password = "password";

            //if (!isConnected)
            //{
            //    await this.frontServiceClient.ConnectAsync(this.serviceUrl);
            //    await this.frontServiceClient.EnterAsync(login, password);
            //    isConnected = true;
            //}

            //List<CarInfo> carInfoCollection = this.frontServiceClient.GetCarInfoCollection();
            //List<CustomerInfo> customerInfoCollection = this.frontServiceClient.GetCustomerInfoCollection();
            //ordersCollection = this.frontServiceClient.GetOrderInfoCollection();

            List<WorkerInfo> workersCollection = await this.frontServiceClient.GetWorkerInfoCollectionAsync();

            this.mainWindowController.LoadContentWorkerCollectionControl(workersCollection);
        }

        protected virtual bool CanLoadWorkers()
        {
            bool valid = true;


            return valid;
        }
        //protected RelayCommandAsync addOrderCommand;
        //protected RelayCommandAsync addCarCommand;
        //protected RelayCommandAsync addCustomerCommand;



        //public ICommand AddOrderCommand
        //{
        //    get
        //    {
        //        if (this.addOrderCommand == null)
        //        {
        //            this.addOrderCommand = new RelayCommandAsync(() => this.AddOrder(), () => this.CanAddOrder());
        //        }
        //        return this.addOrderCommand;
        //    }
        //}

        //public ICommand AddCustomerCommand
        //{
        //    get
        //    {
        //        if (this.addCustomerCommand == null)
        //        {
        //            this.addCustomerCommand = new RelayCommandAsync(() => this.AddCustomer(), () => this.CanAddCustomer());
        //        }
        //        return this.addCustomerCommand;
        //    }
        //}

        //public ICommand AddCarCommand
        //{
        //    get
        //    {
        //        if (this.addCarCommand == null)
        //        {
        //            this.addCarCommand = new RelayCommandAsync(() => this.AddCar(), () => this.CanAddCar());
        //        }
        //        return this.addCarCommand;
        //    }
        //}

        //protected async Task AddOrder()
        //{
        //    DateTime cStartDate = new DateTime(2020, 5, 6);
        //    DateTime cEndDate = new DateTime(2021, 7, 8);
        //    //this.frontServiceClient.A
        //    this.mainWindowController.LoadAddOrderControl(new OrderInfo(orderId, cStartDate, cEndDate, "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client", 1, 1));
        //}

        //protected async Task AddCar()
        //{

        //    //this.frontServiceClient.A
        //    this.mainWindowController.LoadAddCarControl(new CarInfo(carId,"Model1"));
        //}

        //protected async Task AddCustomer()
        //{
        //    //this.frontServiceClient.A
        //    this.mainWindowController.LoadAddCustomerControl(new CustomerInfo(customerId, "firstname"));
        //}

        private bool isConnected = false;



        //protected virtual bool CanAddOrder()
        //{
        //    bool valid = true;


        //    return valid;
        //}

        //protected virtual bool CanAddCar()
        //{
        //    bool valid = true;


        //    return valid;
        //}

        //protected virtual bool CanAddCustomer()
        //{
        //    bool valid = true;


        //    return valid;
        //}

    }
}
