using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;
using System.Windows.Input;
using Common.UI.Utility.Commands;
using TechnicalStation.UI.ViewModel.Base;
using TechnicalStation.Service.Domain.Data;
using System.Windows;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.UI.VewModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace TechnicalStation.UI.ViewModel
{
    public class AddOrderViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync addOrderContentCommand;
        protected RelayCommandAsync cancelCommand;
        //private List<OrderInfo> orderInfoCollection = new List<OrderInfo>();
        public AddOrderViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;

            List<CarInfo> carInfoCollection = Task.Run(async () =>
                await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;

            List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;

            this.OrderViewModel = new OrderViewModel(new OrderInfo(), customerInfoCollection, carInfoCollection);
        }

        private ObservableCollection<CarViewModel> carViewModelCollection = new ObservableCollection<CarViewModel>();
        public ObservableCollection<CarViewModel> CarViewModelCollection
        {
            get
            {
                return this.carViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CarViewModel>>(ref this.carViewModelCollection, value); }
        }

        private void Transform(List<CarInfo> carInfoCollection)
        {
            foreach (var car in carInfoCollection)
            {
                CarViewModelCollection.Add(new CarViewModel(car));
            }
        }

        public AddOrderViewModel()
        {     
        }


        #region OrderViewModel

        /// <summary>
        /// Gets or sets the DateOfBirth.
        /// </summary>
        public OrderViewModel OrderViewModel
        {
            get
            {
                return (OrderViewModel)this.GetUIValue(OrderViewModelProperty);
            }
            set
            {
                SetUIValue(OrderViewModelProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty OrderViewModelProperty =
            DependencyProperty.Register(
                "OrderViewModel",
                typeof(OrderViewModel),
                typeof(AddOrderViewModel),
                new PropertyMetadata(null));

        #endregion

        public ICommand AddOrderCommand
        {
            get
            {
                if (this.addOrderContentCommand == null)
                {
                    this.addOrderContentCommand = new RelayCommandAsync(() => this.AddOrderAsync());
                }
                return this.addOrderContentCommand;
            }
        }

        protected virtual async Task AddOrderAsync()
        {
            OrderInfo orderInfo = this.OrderViewModel.Extract();
            OrderInfo orderInfoResult;

            if (orderInfo.Id == 0)
            {
                orderInfoResult = await frontServiceClient.AddOrderInfoAsync(orderInfo);
            }
            else
            {
                orderInfoResult = await frontServiceClient.UpdateOrderInfoAsync(orderInfo);
            }

            this.OrderViewModel.Transform(orderInfoResult);

            this.mainWindowController.LoadContentOrderControl(orderInfoResult);
        }

        protected virtual bool CanAddOrder()
        {
            bool valid = true;
            return valid;
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
            
            this.mainWindowController.LoadContentOrderControl();
        }



        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
