using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;
using System.Windows.Input;
using Common.UI.Utility;
using Common.UI.Utility.Commands;
using TechnicalStation.UI.ViewModel.Base;
using TechnicalStation.Service.Domain.Data;
using System.Windows;
using TechnicalStation.Service.Client;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.UI.VewModel;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.VewModel
{
    public class OrderFilterViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync filterCommand;
        protected RelayCommandAsync cancelCommand;
        string firstNameValue;
        string secondNameValue;
        string patronymicValue;
        DateTime startDateValue;
        DateTime finishDateValue;
        private List<OrderInfo> orderInfoCollection = new List<OrderInfo>();
        private List<OrderInfo> ordersForFilterCollection = new List<OrderInfo>();
        AddOrderViewModel addOrderViewModel = new AddOrderViewModel();

        public OrderFilterViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            //this.OrderViewModel = new OrderViewModel(new OrderInfo());
        }

        public ICommand FilterCommand
        {
            get
            {
                if (this.filterCommand == null)
                {
                    this.filterCommand = new RelayCommandAsync(() => this.FilterOrders());
                }
                return this.filterCommand;
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

        public string FirstNameValue
        {
            get
            {
                return firstNameValue;
            }
            set
            {
                firstNameValue = value;
            }
        }

        public string SecondNameValue
        {
            get
            {
                return secondNameValue;
            }
            set
            {
                secondNameValue = value;
            }
        }

        public string PatronymicValue
        {
            get
            {
                return patronymicValue;
            }
            set
            {
                patronymicValue = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDateValue;
            }
            set
            {
                startDateValue = value;
            }
        }

        public DateTime FinishDate
        {
            get
            {
                return finishDateValue;
            }
            set
            {
                finishDateValue=value;
           }
        }

        
        protected virtual async Task FilterOrders()
        {
            //NoteInfo noteInfo = this.NoteViewModel.Extract();

            List<CarInfo> carInfoCollection = await this.frontServiceClient.GetCarInfoCollectionAsync();
            List<CustomerInfo> customerInfoCollection = await this.frontServiceClient.GetCustomerInfoCollectionAsync();

            //OrderInfo orderInfo = new OrderInfo(0, DateTime.Now, DateTime.Now, "Undefined", "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client");
            //OrderInfo orderInfo2 = new OrderInfo(0, DateTime.Now, DateTime.Now, "Undefined", "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client");
            ordersForFilterCollection = mainWindowController.GetOrderInfoCollection();//.Add(orderInfo); //
            this.orderInfoCollection = await this.frontServiceClient.GetFilteredOrdersCollection(ordersForFilterCollection, firstNameValue, startDateValue, finishDateValue);
            this.mainWindowController.LoadFilteredOrderCollection(orderInfoCollection, carInfoCollection, customerInfoCollection);
        }

        protected virtual async Task CancelAsync()
        {
            this.mainWindowController.LoadEditorControl();
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
