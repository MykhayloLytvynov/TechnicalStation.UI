using Common.UI.Utility.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class AddCustomerViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync cancelCommand;
        protected RelayCommandAsync addCustomerContentCommand;

        public AddCustomerViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) 
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            //List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;
            List<CarInfo> carInfoCollection = Task.Run(async () =>
                await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;
            this.CustomerViewModel = new CustomerViewModel(new CustomerInfo());
        }

        public CustomerViewModel CustomerViewModel
        {
            get
            {
                return (CustomerViewModel)this.GetUIValue(CustomerViewModelProperty);
            }
            set
            {
                SetUIValue(CustomerViewModelProperty, value);
            }
        }

        public static readonly DependencyProperty CustomerViewModelProperty =
           DependencyProperty.Register(
               "CustomerViewModel",
               typeof(CustomerViewModel),
               typeof(AddCustomerViewModel),
               new PropertyMetadata(null));

        public ICommand AddCustomerContentCommand
        {
            get
            {
                if (this.addCustomerContentCommand == null)
                {
                    this.addCustomerContentCommand = new RelayCommandAsync(() => this.AddCustomer());
                }
                return this.addCustomerContentCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new RelayCommandAsync(() => this.CancelAsync());
                }

                return this.cancelCommand;
            }
        }

        protected virtual async Task CancelAsync()
        {

            this.mainWindowController.LoadContentCustomerControl();
        }

        protected virtual async Task AddCustomer()
        {
            CustomerInfo customerInfo = this.CustomerViewModel.Extract();
            CustomerInfo customerInfoResult;/* = await frontServiceClient.AddCustomerInfoAsync(customerInfo);*/
            //this.CustomerViewModel.Id = customerInfoResult.Id;
            if (customerInfo.Id == 0)
            {
                customerInfoResult = await frontServiceClient.AddCustomerInfoAsync(customerInfo);
            }
            else
            {
                customerInfoResult = await frontServiceClient.UpdateCustomerInfoAsync(customerInfo);
            }
            this.CustomerViewModel.Transform(customerInfoResult);
            this.mainWindowController.LoadContentCustomerControl(customerInfoResult);
        }
        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
