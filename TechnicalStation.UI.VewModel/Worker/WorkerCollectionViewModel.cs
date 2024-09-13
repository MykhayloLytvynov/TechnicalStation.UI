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

namespace TechnicalStation.UI.VewModel.Worker
{
    public class WorkerCollectionViewModel : BindableBase, INotifyPropertyChanged
    {
        IFrontServiceClient frontServiceClient;
        private ObservableCollection<WorkerViewModel> workerViewModelCollection = new ObservableCollection<WorkerViewModel>();
        public WorkerCollectionViewModel(IFrontServiceClient frontServiceClient)
        {
            this.frontServiceClient = frontServiceClient;
        }

        private WorkerViewModel selectedWorker;
        public WorkerViewModel SelectedWorker
        {
            get
            {
                return selectedWorker;
            }
            set
            {
                this.SetProperty<WorkerViewModel>(ref this.selectedWorker, value);
                //this.RaiseNotification("WorkCollection");
            }
        }

        public ObservableCollection<WorkerViewModel> WorkerViewModelCollection
        {
            get
            {
                return this.workerViewModelCollection;
            }
            set 
            {
                this.SetProperty<ObservableCollection<WorkerViewModel>>(ref this.workerViewModelCollection, value); 
            }

            //get
            //{
            //    return this.LoadWorkers();
            //}
        }


        public ObservableCollection<WorkerViewModel> LoadWorkers()
        {

            ObservableCollection<WorkerViewModel> workerViewModelCollection = this.GetWorkerCollection(); 
            return workerViewModelCollection;
        }

        public ObservableCollection<WorkerViewModel> GetWorkerCollection()
        {
            ObservableCollection<WorkerViewModel> collection = new ObservableCollection<WorkerViewModel>();
            List<WorkerInfo> workerInfoCollection = Task.Run(async () => await this.frontServiceClient.GetWorkerInfoCollectionAsync()).Result;

            foreach (WorkerInfo worker in workerInfoCollection)
            {
                WorkerViewModel viewModel = new WorkerViewModel(worker);

                collection.Add(viewModel);
            }

            return collection;
        }

        List<WorkerInfo> workerInfoCollection;

        public void Load(List<WorkerInfo> workerInfoCollection)
        {
            this.workerViewModelCollection.Clear();

            this.workerInfoCollection = workerInfoCollection;
            this.Transform(this.workerInfoCollection);
        }

        private void Transform(List<WorkerInfo> workerInfoCollection)
        {
            foreach (WorkerInfo workerInfo in workerInfoCollection)
            {
                this.Add(workerInfo);
            }
        }

        public void Add(WorkerInfo workerInfo)
        {
            var result = this.workerViewModelCollection.Where(o => o.Id == workerInfo.Id).ToList();

            if (result.Count == 0)
            {

                WorkerViewModel workerViewModel = new WorkerViewModel(workerInfo);
                this.workerViewModelCollection.Add(workerViewModel);
            }
            else
            {
                int index = this.workerViewModelCollection.IndexOf(result[0]);
                this.workerViewModelCollection[index] = new WorkerViewModel(workerInfo);
                this.SelectedWorker = this.workerViewModelCollection[index];
            }
        }
    }
}
