using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.VewModel.Context
{
    public class DataContext
    {
        static ObservableCollection<CustomerViewModel>  CustomerViewModelCollection = new ObservableCollection<CustomerViewModel>();
        static ObservableCollection<CarViewModel> CarViewModelCollection = new ObservableCollection<CarViewModel>();
        public static IFrontServiceClient FrontServiceClient;

        static DataContext()
        {
        }

        public static void LoadData()
        {
            var carInfoCollection = FrontServiceClient.GetCarInfoCollectionAsync();

            //CarViewModelCollection.Clear();

            //foreach (CarInfo carInfo in carInfoCollection)
            //{
            //    CarViewModelCollection.Add(new CarViewModel(carInfo));
            //}
            ////CarViewModelCollection.Add(new CarViewModel(1, "Model 1"));
            //CarViewModelCollection.Add(new CarViewModel(2, "Model 2"));

            ////
            //CustomerViewModelCollection.Add(new CustomerViewModel(1, "First"));
            //CustomerViewModelCollection.Add(new CustomerViewModel(2, "Second"));
        }

        public static ObservableCollection<CustomerViewModel> GetCustomerViewModelCollection()
        {
            return CustomerViewModelCollection;
        }

        public static async Task AddCar(CarViewModel carViewModel)
        {
            CarInfo carInfo = await FrontServiceClient.AddCarInfoAsync(carViewModel.Extract());
            carViewModel.Id = carInfo.Id; //CarViewModelCollection.Max(car => car.Id)+1;
            CarViewModelCollection.Add(new CarViewModel(carInfo));
        }

        public static ObservableCollection<CarViewModel> GetCarViewModelCollection()
        {
            return CarViewModelCollection;
        }

        //public static ObservableCollection<CustomerViewModel> Get
    }
}
