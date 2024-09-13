using Common.UI.Utility.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TechnicalStation.Service.Domain.Data;

namespace TechnicalStation.UI.ViewModel
{
    partial class DashboardViewModel
    {
        protected RelayCommand loadCustomerCommand;

        public ICommand LoadCustomersCommand
        {
            get
            {
                if (this.loadCustomerCommand == null)
                {
                    this.loadCustomerCommand = new RelayCommandAsync(() => this.LoadCustomerAsync(), () => this.CanLoadCustomers());
                }

                return this.loadCustomerCommand;
            }
        }

        protected async Task LoadCustomerAsync()
        {
            //string login = "login";
            //string password = "password";

            //if (!isConnected)
            //{
            //    await this.frontServiceClient.ConnectAsync(this.serviceUrl);
            //    await this.frontServiceClient.EnterAsync(login, password);
            //    isConnected = true;
            //}

            List<CarInfo> carInfoCollection = await this.frontServiceClient.GetCarInfoCollectionAsync();
            List<CustomerInfo> customerInfoCollection = await this.frontServiceClient.GetCustomerInfoCollectionAsync();

            this.mainWindowController.LoadContentCustomerControl(carInfoCollection, customerInfoCollection);
        }

        protected virtual bool CanLoadCustomers()
        {
            bool valid = true;


            return valid;
        }
    }
}

