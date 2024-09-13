using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Base;
using TechnicalStation.UI.VewModel.Car;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.VewModel.Customer
{
    public class CustomerCollectionViewModel : BindableBase, INotifyPropertyChanged
    {
        private ObservableCollection<CustomerViewModel> customerViewModelCollection = new ObservableCollection<CustomerViewModel>();
        //private ObservableCollection<CarCollectionViewModel> carViewModelCollection = new ObservableCollection<CarCollectionViewModel>(); 

        public ObservableCollection<CustomerViewModel> CustomerViewModelCollection
        {
            get
            {
                return this.customerViewModelCollection;
            }
            set { this.SetProperty<ObservableCollection<CustomerViewModel>>(ref this.customerViewModelCollection, value); }
        }

        private ObservableCollection<CarViewModel> carCollection;
        public ObservableCollection<CarViewModel> CarCollection
        {
            get
            {
                if (this.selectedCustomer == null)
                {
                    return null;
                }

                return this.LoadCars(this.SelectedCustomer.Id);//
            }
        }

        private List<CustomerInfo>customerInfoCollection;

        public CustomerCollectionViewModel(IFrontServiceClient frontServiceClient)
        {
            this.frontServiceClient = frontServiceClient;
        }

        private CustomerViewModel selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get { 
                return selectedCustomer; }
            set
            {
                this.SetProperty<CustomerViewModel>(ref this.selectedCustomer, value);
                this.RaiseNotification("CarCollection");
            }
        }


        public  CarViewModel SelectedCar
        {
            get
            {
                return (CarViewModel)this.GetUIValue(selectedCarProperty);
            }
            set
            {
                SetUIValue(selectedCarProperty, value);
            }
        }

        /// <summary>
        /// The Publish time property.
        /// </summary>
        public static readonly DependencyProperty selectedCarProperty =
            DependencyProperty.Register(
            "SelectedCar",
            typeof(CarViewModel),
            typeof(CustomerCollectionViewModel),
            new PropertyMetadata(null));




        IFrontServiceClient frontServiceClient;

        private ObservableCollection<CarViewModel> LoadCars(int customerId)
        {
            ObservableCollection<CarViewModel> carViewModelCollection = this.GetCarCollection(customerId);
            ObservableCollection<CarViewModel> selectedCarViewModelCollection = new ObservableCollection<CarViewModel>();
            foreach (CarViewModel carViewModel in carViewModelCollection)
            {
                if (carViewModel.CustomerId == customerId)
                {
                    selectedCarViewModelCollection.Add(carViewModel);
                }
            }
            return selectedCarViewModelCollection;

        }

        public ObservableCollection<CarViewModel> GetCarCollection(int customerId)
        {
            ObservableCollection<CarViewModel> collection = new ObservableCollection<CarViewModel>();
            List<CarInfo> carInfoCollection = Task.Run(async () => await frontServiceClient.GetCarInfoCollectionAsync()).Result;

            foreach (CarInfo carInfo in carInfoCollection)
            {
                CarViewModel viewModel = new CarViewModel(carInfo);

                collection.Add(viewModel);
            }


            return collection;
        }

        private void Transform(List<CustomerInfo> customerInfoCollection)
        {
            foreach (CustomerInfo customerInfo in customerInfoCollection)
            {
                this.Add(customerInfo);
            }
        }

        public void Load(List<CustomerInfo> customerInfoCollection)
        {
            this.customerViewModelCollection.Clear();

            this.customerInfoCollection = customerInfoCollection;
            this.Transform(this.customerInfoCollection);
        }

        //protected override string GetValidationError(string property)
        //{
        //    return string.Empty;
        //}



        public void Add(CustomerInfo customerInfo)
        {
            var result = this.customerViewModelCollection.Where(o => o.Id == customerInfo.Id).ToList();

            if (result.Count == 0)
            {

                CustomerViewModel customerViewModel = new CustomerViewModel(customerInfo);
                this.customerViewModelCollection.Add(customerViewModel);
            }
            else
            {
                int index = this.customerViewModelCollection.IndexOf(result[0]);
                this.customerViewModelCollection[index] = new CustomerViewModel(customerInfo);
                this.SelectedCustomer = this.customerViewModelCollection[index];
            }
        }

        
    }
}
