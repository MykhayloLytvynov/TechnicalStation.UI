using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalStation.UI.Contract;
using System.Windows.Input;
using Common.UI.Utility.Commands;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.UI.VewModel;
using System.Windows;
using TechnicalStation.UI.ViewModel.Base;
using System.Windows.Threading;

namespace TechnicalStation.UI.ViewModel
{
    public class OrderEditorViewModel: ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        IFrontServiceClient frontServiceClient;
        int orderId = 0;
        int workId = 0;
        List<OrderInfo> orderInfoCollection = new List<OrderInfo>();
        public OrderEditorViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            this.OrderCollectionViewModel = new OrderCollectionViewModel(frontServiceClient);
        }

        public void Load(List<OrderInfo> orderInfoCollection, List<CarInfo> carInfoCollection, List<CustomerInfo> customerInfoCollection)
        {
            this.OrderCollectionViewModel.Load(orderInfoCollection, carInfoCollection, customerInfoCollection);
        }

        #region OrderCollectionViewModel

        /// <summary>
        /// Gets or sets the NickName.
        /// </summary>
        public OrderCollectionViewModel OrderCollectionViewModel
        {
            get
            {
                return (OrderCollectionViewModel)this.GetUIValue(OrderCollectionViewModelProperty);
            }
            set
            {
                SetUIValue(OrderCollectionViewModelProperty, value);
            }
        }

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty OrderCollectionViewModelProperty =
            DependencyProperty.Register("OrderCollectionViewModel", typeof(OrderCollectionViewModel),
                typeof(OrderEditorViewModel), new PropertyMetadata(null));

        #endregion

        protected RelayCommandAsync addOrderCommand;
        protected RelayCommandAsync removeOrderCommand;
        protected RelayCommandAsync updateOrderCommand;

        protected RelayCommandAsync addCarCommand;

        protected RelayCommandAsync addWorkCommand;
        protected RelayCommandAsync removeWorkCommand;
        protected RelayCommandAsync updateWorkCommand;

        protected RelayCommandAsync addFilterCommand;
        protected RelayCommandAsync resetFilterCommand;

        public ICommand AddOrderCommand
        {
            get
            {
                if (this.addOrderCommand == null)
                {
                    this.addOrderCommand = new RelayCommandAsync(() => this.AddOrder(), () =>this.CanAddOrder());
                }
                return this.addOrderCommand;
            }
        }

        public ICommand UpdateOrderCommand
        {
            get
            {
                if (this.updateOrderCommand == null)
                {
                    this.updateOrderCommand = new RelayCommandAsync(() => this.UpdateOrder(), () => this.CanUpdateOrder());
                }
                return this.updateOrderCommand;
            }
        }

        public ICommand UpdateWorkCommand
        {
            get
            {
                if (this.updateWorkCommand == null)
                {
                    this.updateWorkCommand = new RelayCommandAsync(() => this.UpdateWork(), () => this.CanUpdateWork());
                }
                return this.updateWorkCommand;
            }
        }

        private bool CanUpdateOrder()
        {
            return true;
        }

        protected virtual void AddOrder()
        {
            DateTime cStartDate = new DateTime(2020, 5, 6);
            DateTime cEndDate = new DateTime(2021, 7, 8);
            List<CarInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;
            List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;

            var orderInfo = new OrderInfo(orderId, 1, 1, cStartDate, cEndDate,  DateTime.Now);

            this.mainWindowController.LoadAddOrderControl(orderInfo, carInfoCollection, customerInfoCollection);
        }

        protected virtual void UpdateOrder()
        {
            DateTime cStartDate = new DateTime(2020, 5, 6);
            DateTime cEndDate = new DateTime(2021, 7, 8);
            List<CarInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;
            List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;

            var orderInfo =this.OrderCollectionViewModel.SelectedOrder.Extract();
            // new OrderInfo(orderId, cStartDate, cEndDate, "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client", 1, 1);

            this.mainWindowController.LoadAddOrderControl(orderInfo, carInfoCollection, customerInfoCollection);
        }

        protected virtual void UpdateWork()
        {
            //DateTime cStartDate = new DateTime(2020, 5, 6);
            //DateTime cEndDate = new DateTime(2021, 7, 8);
            List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;
            //List<CustomerInfo> customerInfoCollection = this.frontServiceClient.GetCustomerInfoCollection();
            var orderInfo = this.OrderCollectionViewModel.SelectedOrder.Extract();
            var workInfo = this.OrderCollectionViewModel.SelectedWork.Extract();
            // new OrderInfo(orderId, cStartDate, cEndDate, "Undefined", DateTime.Now, "firstname_of_client", "secondname_of_client", "patronymic_of_client", 1, 1);

            this.mainWindowController.LoadUpdateWorkControl(workInfo, workerInfoCollection);
        }


        public ICommand AddWorkCommand
        {
            get
            {
                if (this.addWorkCommand == null)
                {
                    this.addWorkCommand = new RelayCommandAsync(() => this.AddWork(), () => this.CanAddWork());
                }
                return this.addWorkCommand;
            }
        }

        public ICommand ResetFilterCommand
        {
            get
            {
                if (this.resetFilterCommand == null)
                {
                    this.resetFilterCommand = new RelayCommandAsync(() => this.ResetFilter(), () => this.CanResetFilter());
                }
                return this.resetFilterCommand;
            }
        }

        public ICommand RemoveOrderCommand
        {
            get
            {
                if (this.removeOrderCommand == null)
                {
                    this.removeOrderCommand = new RelayCommandAsync(() => this.RemoveAsync(), () => this.CanRemove());
                }

                return this.removeOrderCommand;
            }
        }

        public ICommand RemoveWorkCommand
        {
            get
            {
                if (this.removeWorkCommand == null)
                {
                    this.removeWorkCommand = new RelayCommandAsync(() => this.RemoveWorkAsync(), () => this.CanRemoveWork());
                }

                return this.removeWorkCommand;
            }
        }

        public ICommand AddCarCommand
        {
            get
            {
                if (this.addCarCommand == null)
                {
                    this.addCarCommand = new RelayCommandAsync(() => this.AddCarAsync(), () => this.CanAddCar());
                }

                return this.addCarCommand;
            }
        }

        private bool CanAddCar()
        {
            return true;
        }

        public ICommand AddFilterCommand
        {
            get
            {
                if (this.addFilterCommand == null)
                {
                    this.addFilterCommand = new RelayCommandAsync(() => this.AddOrdersFilter());
                }

                return this.addFilterCommand;
            }
        }

        protected virtual void AddOrdersFilter()
        {

            this.mainWindowController.LoadOrderFilterControl();
        }

        protected virtual void ResetFilter()
        {
            this.orderInfoCollection = mainWindowController.GetOrderInfoCollection();
            List<CarInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;
            List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;
            this.mainWindowController.LoadFilteredOrderCollection(orderInfoCollection, carInfoCollection, customerInfoCollection);
        }

        protected virtual void AddCarAsync()
        {
            //DateTime cStartDate = new DateTime(2020, 5, 6);
            //DateTime cEndDate = new DateTime(2021, 7, 8);

            if (this.OrderCollectionViewModel.SelectedOrder != null)
            {
                int orderId = this.OrderCollectionViewModel.SelectedOrder.Id;

                this.mainWindowController.LoadAddWorkControl(orderId);
            }
            else
            {
                MessageBox.Show("Select repair first");
            }
            //workId++;
        }

        public virtual void AddWork()
        {
            //DateTime cStartDate = new DateTime(2020, 5, 6);
            //DateTime cEndDate = new DateTime(2021, 7, 8);

            if(this.OrderCollectionViewModel.SelectedOrder != null)
            { 
            int orderId = this.OrderCollectionViewModel.SelectedOrder.Id;

            this.mainWindowController.LoadAddWorkControl(orderId);
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
                OrderViewModel orderViewModel = null;
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            orderViewModel = this.OrderCollectionViewModel.SelectedOrder as OrderViewModel;
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

                OrderInfo orderInfo = orderViewModel.Extract();
                await this.frontServiceClient.RemoveOrderInfoAsync(orderInfo.Id);
                mainWindowController.RemoveFromList(orderInfo);
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            this.OrderCollectionViewModel.OrderViewModelCollection.Remove(orderViewModel);

                            var collection = this.OrderCollectionViewModel.WorkCollection.Where(o => o.OrderId == orderInfo.Id).ToList();
                            if(collection.Count > 0)
                            {
                                foreach(var element in collection)
                                {
                                    this.OrderCollectionViewModel.WorkCollection.Remove(element);
                                }
                            }

                            //this.OrderCollectionViewModel.CollectionView.SetFocus();
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

        protected virtual async Task RemoveWorkAsync()
        {
            try
            {
                WorkViewModel workViewModel = null;
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                           workViewModel = this.OrderCollectionViewModel.SelectedWork as WorkViewModel;
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

                WorkInfo workInfo = workViewModel.Extract();
                await this.frontServiceClient.RemoveWorkInfoAsync(workInfo.Id);

                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            this.OrderCollectionViewModel.RaiseNotification("WorkCollection");
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

        //public void AddWork(WorkInfo workInfo)
        //{
        //    this.OrderCollectionViewModel.AddWork(workInfo);
        //}

        public void AddOrder(OrderInfo orderInfo)
        {
            this.OrderCollectionViewModel.Add(orderInfo);
        }

        protected virtual bool CanAddOrder()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanAddWork()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanRemove()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanRemoveWork()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanUpdateWork()
        {
            bool valid = true;
            return valid;
        }
        protected virtual bool CanResetFilter()
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
