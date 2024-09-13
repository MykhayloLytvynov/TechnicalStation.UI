using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.VewModel.Base;
using TechnicalStation.UI.ViewModel;

namespace TechnicalStation.UI.VewModel.Car
{
    public class CarCollectionViewModel : BindableBase, INotifyPropertyChanged
    {
        IFrontServiceClient frontServiceClient;
        private ObservableCollection<CarViewModel> carViewModelCollection = new ObservableCollection<CarViewModel>();
        public CarCollectionViewModel(IFrontServiceClient frontServiceClient)
        {
            this.frontServiceClient = frontServiceClient;
        }

        private CarViewModel selectedCar;
        public CarViewModel SelectedCar
        {
            get
            {
                return selectedCar;
            }
            set
            {
                this.SetProperty<CarViewModel>(ref this.selectedCar, value);
                //this.RaiseNotification("WorkCollection");
            }
        }

        public ObservableCollection<CarViewModel> CarViewModelCollection
        {
            get
            {
                return this.carViewModelCollection;
            }
            set 
            {
                this.SetProperty<ObservableCollection<CarViewModel>>(ref this.carViewModelCollection, value); 
            }

            //get
            //{
            //    return this.LoadCars();
            //}
        }


        public ObservableCollection<CarViewModel> LoadCars()
        {

            ObservableCollection<CarViewModel> carViewModelCollection = this.GetCarCollection(); 
            return carViewModelCollection;
        }

        public ObservableCollection<CarViewModel> GetCarCollection()
        {
            ObservableCollection<CarViewModel> collection = new ObservableCollection<CarViewModel>();
            List<CarInfo> carInfoCollection = Task.Run(async () => await this.frontServiceClient.GetCarInfoCollectionAsync()).Result;

            foreach (CarInfo car in carInfoCollection)
            {
                CarViewModel viewModel = new CarViewModel(car);

                collection.Add(viewModel);
            }

            return collection;
        }

        List<CarInfo> carInfoCollection;

        public void Load(List<CarInfo> carInfoCollection)
        {
            this.carViewModelCollection.Clear();

            this.carInfoCollection = carInfoCollection;
            this.Transform(this.carInfoCollection);
        }

        private void Transform(List<CarInfo> carInfoCollection)
        {
            foreach (CarInfo carInfo in carInfoCollection)
            {
                this.Add(carInfo);
            }
        }

        public void Add(CarInfo carInfo)
        {
            var result = this.carViewModelCollection.Where(o => o.Id == carInfo.Id).ToList();

            if (result.Count == 0)
            {

                CarViewModel carViewModel = new CarViewModel(carInfo);
                this.carViewModelCollection.Add(carViewModel);
            }
            else
            {
                int index = this.carViewModelCollection.IndexOf(result[0]);
                this.carViewModelCollection[index] = new CarViewModel(carInfo);
                this.SelectedCar = this.carViewModelCollection[index];
            }
        }
    }
}
