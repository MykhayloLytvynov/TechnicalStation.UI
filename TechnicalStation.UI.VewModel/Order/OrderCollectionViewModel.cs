using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Base;
using TechnicalStation.Service.Client.Contract;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.VewModel
{
    public class OrderCollectionViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<OrderViewModel> orderViewModelCollection = new ObservableCollection<OrderViewModel>();

        private ObservableCollection<WorkCollectionViewModel> workViewModelCollection = new ObservableCollection<WorkCollectionViewModel>(); 

        private ObservableCollection<WorkerViewModel> workerViewModelCollection = new ObservableCollection<WorkerViewModel>();
        private ObservableCollection<CarViewModel> carViewModelCollection = new ObservableCollection<CarViewModel>();
        private ObservableCollection<CustomerViewModel> customerViewModelCollection = new ObservableCollection<CustomerViewModel>();

        public ObservableCollection<OrderViewModel> OrderViewModelCollection
        {
            get
            {
                return this.orderViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<OrderViewModel>>(ref this.orderViewModelCollection, value); }
        }

        public ObservableCollection<CarViewModel> CarViewModelCollection
        {
            get
            {
                return this.carViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CarViewModel>>(ref this.carViewModelCollection, value); }
        }

        public ObservableCollection<CustomerViewModel> CustomerViewModelCollection
        {
            get
            {
                return this.customerViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CustomerViewModel>>(ref this.customerViewModelCollection, value); }
        }

        public ObservableCollection<WorkerViewModel> WorkerViewModelCollection
        {
            get
            {
                return this.workerViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<WorkerViewModel>>(ref this.workerViewModelCollection, value); }
        }

        private List<OrderInfo>orderInfoCollection;

        public OrderCollectionViewModel(IFrontServiceClient frontServiceClient)
        {
            this.frontServiceClient = frontServiceClient;
        }

        private OrderViewModel selectedOrder;
        public OrderViewModel SelectedOrder
        {
            get { 
                return selectedOrder; }
            set
            {
                this.SetProperty<OrderViewModel>(ref this.selectedOrder, value);
                this.RaiseNotification("WorkCollection");
            }
        }


        public  WorkViewModel SelectedWork
        {
            get
            {
                return (WorkViewModel)this.GetUIValue(selectedWorkProperty);
            }
            set
            {
                SetUIValue(selectedWorkProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty selectedWorkProperty =
            DependencyProperty.Register(
            "SelectedWork",
            typeof(WorkViewModel),
            typeof(OrderCollectionViewModel),
            new PropertyMetadata(null));


        private ObservableCollection<WorkViewModel> workCollection;
        public  ObservableCollection<WorkViewModel> WorkCollection
        {
            get
            {
                if (this.selectedOrder == null)
                {
                    return null;
                }

                return this.LoadWorks(this.SelectedOrder.Id);//
            }
            //set { this.SetProperty<ObservableCollection<WorkCollectionViewModel>>(ref this.workCollection, value); }

        }

        IFrontServiceClient frontServiceClient;

        private ObservableCollection<WorkViewModel> LoadWorks(int orderId)
        {
            ObservableCollection<WorkViewModel> workViewModelCollection = this.GetWorkCollection(orderId);
            ObservableCollection<WorkViewModel> selectedWorkViewModelCollection = new ObservableCollection<WorkViewModel>();

            foreach (WorkViewModel workViewModel in workViewModelCollection)
            {
                // todo: make await frontServiceClient.GetWorkInfoCollectionByOrderIdAsync(orderId)
                if (workViewModel.OrderId == orderId)
                {
                    selectedWorkViewModelCollection.Add(workViewModel);
                }
            }

            return selectedWorkViewModelCollection;

        }

        public ObservableCollection<WorkViewModel> GetWorkCollection(int orderId)
        {
            ObservableCollection<WorkViewModel> collection = new ObservableCollection<WorkViewModel>();
            List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;
            List<WorkInfo> workInfoCollection = Task.Run(async () => await frontServiceClient.GetWorkInfoCollectionAsync()).Result;

            foreach (WorkInfo work in workInfoCollection)
            {
                WorkViewModel viewModel = new WorkViewModel(work, workerInfoCollection);

                collection.Add(viewModel);
            }

            workerViewModelCollection.Clear();

            foreach (var workerInfo in workerInfoCollection)
            {
                this.workerViewModelCollection.Add(new WorkerViewModel(workerInfo));
            }

            return collection;
        }

        //public void AddWork(WorkInfo workInfo)
        //{
        //    List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;
        //    WorkViewModel workViewModel = new WorkViewModel(workInfo, workerInfoCollection);
        //    //workViewModelCollection.Add(workViewModel);
        //    //this.orderViewModelCollection.Add(orderViewModel);
        //}

        private void Transform(List<OrderInfo> orderInfoCollection)
        {
            foreach (OrderInfo orderInfo in orderInfoCollection)
            {
                this.Add(orderInfo);
            }
        }

        public void Load(List<OrderInfo> orderInfoCollection, 
            List<CarInfo> carInfoCollection, 
            List<CustomerInfo> customerInfoCollection)
        {
            this.orderViewModelCollection.Clear();

            this.orderInfoCollection = orderInfoCollection;
            this.Transform(this.orderInfoCollection);

            carViewModelCollection.Clear();
            customerViewModelCollection.Clear();

            foreach (var customerInfo in customerInfoCollection)
            {
                this.customerViewModelCollection.Add(new CustomerViewModel(customerInfo));
            }

            foreach (var carInfo in carInfoCollection)
            {
                this.carViewModelCollection.Add(new CarViewModel(carInfo));
            }
        }

        //protected override string GetValidationError(string property)
        //{
        //    return string.Empty;
        //}



        public void Add(OrderInfo orderInfo)
        {
            var result = this.orderViewModelCollection.Where(o => o.Id == orderInfo.Id).ToList();
            List<CarInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;
            List<CustomerInfo> customerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCustomerInfoCollectionAsync()).Result;

            if (result.Count == 0)
            {

                OrderViewModel orderViewModel = new OrderViewModel(orderInfo, customerInfoCollection, carInfoCollection);
                this.orderViewModelCollection.Add(orderViewModel);
            }
            else
            {
                int index = this.orderViewModelCollection.IndexOf(result[0]);
                this.orderViewModelCollection[index] = new OrderViewModel(orderInfo, customerInfoCollection, carInfoCollection);
                this.SelectedOrder = this.orderViewModelCollection[index];
            }
        }

        
    }
}
