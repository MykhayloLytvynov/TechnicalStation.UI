using Common.UI.Utility.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.VewModel.Context;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel
{
    public class AddCarViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        private IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync cancelCommand;
        protected RelayCommandAsync addCarContentCommand;
        public AddCarViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            this.CarViewModel = new CarViewModel(new CarInfo());
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

        public CarViewModel CarViewModel
        {
            get
            {
                return (CarViewModel)this.GetUIValue(CarViewModelProperty);
            }
            set
            {
                SetUIValue(CarViewModelProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty CarViewModelProperty =
            DependencyProperty.Register(
                "CarViewModel",
                typeof(CarViewModel),
                typeof(AddCarViewModel),
                new PropertyMetadata(null));

        public ICommand AddCarContentCommand
        {
            get
            {
                if (this.addCarContentCommand == null)
                {
                    this.addCarContentCommand = new RelayCommandAsync(() => this.AddCar());
                }
                return this.addCarContentCommand;
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

        protected virtual async Task AddCar() 
        {
            CarInfo carInfo = this.CarViewModel.Extract();
            CarInfo carInfoResult;
            if (carInfo.Id == 0)
            {
                carInfoResult = await frontServiceClient.AddCarInfoAsync(carInfo);
            }
            else
            {
                carInfoResult = await frontServiceClient.UpdateCarInfoAsync(carInfo);
            }

            this.CarViewModel.Transform(carInfoResult);

            this.mainWindowController.LoadContentCarCollectionControl();
        }

        public void Load(CarInfo carInfo)
        {
            //this.Mode = "Edit";
            this.CarViewModel = new CarViewModel(carInfo);
        }

        public void LoadCarViewModel(int customerId)
        {
            //this.Mode = "Add";

           // List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;

            this.CarViewModel = new CarViewModel(new CarInfo());
            this.CarViewModel.CustomerId = customerId;
        }

        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
