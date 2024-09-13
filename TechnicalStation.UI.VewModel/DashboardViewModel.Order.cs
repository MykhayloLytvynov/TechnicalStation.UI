using Common.UI.Utility.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.UI.ViewModel
{
    partial class DashboardViewModel
    {
        protected RelayCommand loadOrdersCommand;

        public ICommand LoadOrdersCommand
        {
            get
            {
                if (this.loadOrdersCommand == null)
                {
                    this.loadOrdersCommand = new RelayCommandAsync(() => this.LoadOrdersAsync(), () => this.CanLoadOrders());
                }

                return this.loadOrdersCommand;
            }
        }

        protected async Task LoadOrdersAsync()
        {
            string login = "login";
            string password = "password";

            if (!isConnected)
            {
                await this.frontServiceClient.ConnectAsync(this.serviceUrl);
                await this.frontServiceClient.EnterAsync(login, password);
                isConnected = true;
            }

            List<CarInfo> carInfoCollection = await this.frontServiceClient.GetCarInfoCollectionAsync();
            List<CustomerInfo> customerInfoCollection = await this.frontServiceClient.GetCustomerInfoCollectionAsync();
            List<OrderInfo> ordersCollection = await this.frontServiceClient.GetOrderInfoCollectionAsync();

            this.mainWindowController.LoadContentOrderControl(ordersCollection, carInfoCollection, customerInfoCollection);
        }

        protected virtual bool CanLoadOrders()
        {
            bool valid = true;


            return valid;
        }
    }
}
