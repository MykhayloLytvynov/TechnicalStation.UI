using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Common.UI.Utility.Commands;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Customer
{
    public class CustomerEditorViewModel: ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        IFrontServiceClient frontServiceClient;
        int customerId = 0;
        int carId = 0;
        List<CustomerInfo> customerInfoCollection = new List<CustomerInfo>();

        public CustomerEditorViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            this.CustomerCollectionViewModel = new CustomerCollectionViewModel(frontServiceClient);
        }

        public void Load(List<CustomerInfo> customerInfoCollection)
        {
            this.CustomerCollectionViewModel.Load(customerInfoCollection);
        }

        #region CustomerCollectionViewModel

        /// <summary>
        /// Gets or sets the NickName.
        /// </summary>
        public CustomerCollectionViewModel CustomerCollectionViewModel
        {
            get
            {
                return (CustomerCollectionViewModel)this.GetUIValue(CustomerCollectionViewModelProperty);
            }
            set
            {
                SetUIValue(CustomerCollectionViewModelProperty, value);
            }
        }

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty CustomerCollectionViewModelProperty =
            DependencyProperty.Register("CustomerCollectionViewModel", typeof(CustomerCollectionViewModel),
                typeof(CustomerEditorViewModel), new PropertyMetadata(null));

        #endregion

        protected RelayCommandAsync addCustomerCommand;
        protected RelayCommandAsync removeCustomerCommand;
        protected RelayCommandAsync updateCustomerCommand;

        protected RelayCommandAsync addCarCommand;

        protected RelayCommandAsync removeCarCommand;
        protected RelayCommandAsync updateCarCommand;


        public ICommand AddCustomerCommand
        {
            get
            {
                if (this.addCustomerCommand == null)
                {
                    this.addCustomerCommand = new RelayCommandAsync(() => this.AddCustomer(), () =>this.CanAddCustomer());
                }
                return this.addCustomerCommand;
            }
        }

        public ICommand UpdateCustomerCommand
        {
            get
            {
                if (this.updateCustomerCommand == null)
                {
                    this.updateCustomerCommand = new RelayCommandAsync(() => this.UpdateCustomer(), () => this.CanUpdateCustomer());
                }
                return this.updateCustomerCommand;
            }
        }

        public ICommand UpdateCarCommand
        {
            get
            {
                if (this.updateCarCommand == null)
                {
                    this.updateCarCommand = new RelayCommandAsync(() => this.UpdateCar(), () => this.CanUpdateCar());
                }
                return this.updateCarCommand;
            }
        }

        private bool CanUpdateCustomer()
        {
            return true;
        }

        public virtual void AddCustomer()
        {
            DateTime cStartDate = new DateTime(2020, 5, 6);
            DateTime cEndDate = new DateTime(2021, 7, 8);

            var customerInfo = new CustomerInfo(customerId, "FirstName", "LastName", "Address", "983794",  DateTime.Now);

            this.mainWindowController.LoadAddCustomerControl(customerInfo);
        }

        protected virtual void UpdateCustomer()
        {

            var customerInfo =this.CustomerCollectionViewModel.SelectedCustomer.Extract();
            // new CustomerInfo(customerId, cStartDate, cEndDate, "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client", 1, 1);

            this.mainWindowController.LoadAddCustomerControl(customerInfo);
        }

        protected virtual void UpdateCar()
        {
            var customerInfo = this.CustomerCollectionViewModel.SelectedCustomer.Extract();
            var carInfo = this.CustomerCollectionViewModel.SelectedCar.Extract();
            // new CustomerInfo(customerId, cStartDate, cEndDate, "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client", 1, 1);

            this.mainWindowController.LoadUpdateCarControl(carInfo);
        }

        public void Load(List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            this.CustomerCollectionViewModel.Load(customerInfoCollection);
        }


        public void AddCustomer(CustomerInfo customerInfo)
        {
            this.CustomerCollectionViewModel.Add(customerInfo);
        }
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

        public ICommand RemoveCustomerCommand
        {
            get
            {
                if (this.removeCustomerCommand == null)
                {
                    this.removeCustomerCommand = new RelayCommandAsync(() => this.RemoveAsync(), () => this.CanRemove());
                }

                return this.removeCustomerCommand;
            }
        }

        public ICommand RemoveCarCommand
        {
            get
            {
                if (this.removeCarCommand == null)
                {
                    this.removeCarCommand = new RelayCommandAsync(() => this.RemoveCarAsync(), () => this.CanRemoveCar());
                }

                return this.removeCarCommand;
            }
        }

        public ICommand AddCarCommand
        {
            get
            {
                if (this.addCarCommand == null)
                {
                    this.addCarCommand = new RelayCommandAsync(() => this.AddCar(), () => this.CanAddCar());
                }

                return this.addCarCommand;
            }
        }

        protected virtual void AddCar()
        {
            //DateTime cStartDate = new DateTime(2020, 5, 6);
            //DateTime cEndDate = new DateTime(2021, 7, 8);

            if (this.CustomerCollectionViewModel.SelectedCustomer != null)
            {
                int customerId = this.CustomerCollectionViewModel.SelectedCustomer.Id;

                this.mainWindowController.LoadAddCarControl(customerId);
            }
            else
            {
                MessageBox.Show("Select repair first");
            }
            //workId++;
        }

        



        protected virtual async Task RemoveAsync()
        {
            try
            {
                CustomerViewModel customerViewModel = null;
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            customerViewModel = this.CustomerCollectionViewModel.SelectedCustomer as CustomerViewModel;
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

                CustomerInfo customerInfo = customerViewModel.Extract();
                await this.frontServiceClient.RemoveCustomerInfoAsync(customerInfo.Id);
                //mainWindowController.RemoveFromList(customerInfo);

                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            this.CustomerCollectionViewModel.CustomerViewModelCollection.Remove(customerViewModel);

                            var collection = this.CustomerCollectionViewModel.CarCollection.Where(o => o.CustomerId == customerInfo.Id).ToList();
                            if(collection.Count > 0)
                            {
                                foreach(var element in collection)
                                {
                                    this.CustomerCollectionViewModel.CarCollection.Remove(element);
                                }
                            }

                            //this.CustomerCollectionViewModel.CollectionView.SetFocus();
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        protected virtual async Task RemoveCarAsync()
        {
            try
            {
                CarViewModel carViewModel = null;
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                           carViewModel = this.CustomerCollectionViewModel.SelectedCar as CarViewModel;
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

                CarInfo carInfo = carViewModel.Extract();
                await this.frontServiceClient.RemoveCarInfoAsync(carInfo.Id);

                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            this.CustomerCollectionViewModel.RaiseNotification("CarCollection");
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }


        protected virtual bool CanAddCustomer()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanAddCar()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanRemove()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanRemoveCar()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanUpdateCar()
        {
            bool valid = true;
            return valid;
        }


        protected override string GetValidationError(string property)
        {
            return string.Empty;
        }
    }
}
