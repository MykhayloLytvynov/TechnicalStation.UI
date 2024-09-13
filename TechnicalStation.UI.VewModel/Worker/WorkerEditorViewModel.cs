using Common.UI.Utility.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TechnicalStation.Service.Client.Contract;
using TechnicalStation.Service.Domain.Data;
using TechnicalStation.UI.Contract;
using TechnicalStation.UI.ViewModel;
using TechnicalStation.UI.ViewModel.Base;

namespace TechnicalStation.UI.VewModel.Worker
{
    public class WorkerEditorViewModel : ElementViewModelBase
    {
        IMainWindowController mainWindowController;
        IFrontServiceClient frontServiceClient;
        protected RelayCommandAsync removeWorkerCommand;
        protected RelayCommandAsync updateWorkerCommand;
        int orderId = 0;
        int workId = 0;
        List<WorkerInfo> orderInfoCollection = new List<WorkerInfo>();

        public WorkerEditorViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            this.mainWindowController = mainWindowController;
            this.frontServiceClient = frontServiceClient;
            this.WorkerCollectionViewModel = new WorkerCollectionViewModel(frontServiceClient);
        }

        public WorkerCollectionViewModel WorkerCollectionViewModel
        {
            get
            {
                return (WorkerCollectionViewModel)this.GetUIValue(WorkerCollectionViewModelProperty);
            }
            set
            {
                SetUIValue(WorkerCollectionViewModelProperty, value);
            }
        }

        /// <summary>
        /// The NickName property.
        /// </summary>
        public static readonly DependencyProperty WorkerCollectionViewModelProperty =
            DependencyProperty.Register("WorkerCollectionViewModel", typeof(WorkerCollectionViewModel),
                typeof(WorkerEditorViewModel), new PropertyMetadata(null));

        public void Load(List<WorkerInfo> workerInfoCollection)
        {
            this.WorkerCollectionViewModel.Load(workerInfoCollection);
        }

        protected RelayCommandAsync addWorkerCommand;

        public ICommand AddWorkerCommand
        {
            get
            {
                if (this.addWorkerCommand == null)
                {
                    this.addWorkerCommand = new RelayCommandAsync(() => this.AddWorker(), () => this.CanAddWorker());
                }
                return this.addWorkerCommand;
            }
        }

        public ICommand UpdateWorkerCommand
        {
            get
            {
                if (this.updateWorkerCommand == null)
                {
                    this.updateWorkerCommand = new RelayCommandAsync(() => this.UpdateWorker(), () => this.CanUpdateWorker());
                }
                return this.updateWorkerCommand;
            }
        }

        public void AddWorker(WorkerInfo workerInfo)
        {
            this.WorkerCollectionViewModel.Add(workerInfo);
        }

        protected virtual void AddWorker()
        {
            //DateTime cStartDate = new DateTime(2020, 5, 6);
            //DateTime cEndDate = new DateTime(2021, 7, 8);
            //List<CarInfo> carInfoCollection = this.frontServiceClient.GetCarInfoCollection();
            //List<CustomerInfo> customerInfoCollection = this.frontServiceClient.GetCustomerInfoCollection();
            //string firstName,

            //string lastName,

            //string address,

            //string phoneNumber,

            //string notes,
            //    DateTime modifyTime
            var workerInfo = new WorkerInfo(0, "FisrtName","LastName","Address","098908908","notes", DateTime.Now);

            this.mainWindowController.LoadAddWorkerControl(workerInfo);
        }

        protected virtual void UpdateWorker()
        {
            var workerInfo = this.WorkerCollectionViewModel.SelectedWorker.Extract();
            this.mainWindowController.LoadUpdateWorkerControl(workerInfo);
        }

        public ICommand RemoveWorkerCommand
        {
            get
            {
                if (this.removeWorkerCommand == null)
                {
                    this.removeWorkerCommand = new RelayCommandAsync(() => this.RemoveWorkerAsync(), () => this.CanRemoveWork());
                }

                return this.removeWorkerCommand;
            }
        }

        protected virtual void RemoveWorkerAsync()
        {
            try
            {
                WorkerViewModel workerViewModel = null;
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            workerViewModel = this.WorkerCollectionViewModel.SelectedWorker as WorkerViewModel;
                        }
                        catch (Exception ex)
                        {
                            int i = 0;
                        }
                    }));

                WorkerInfo workerInfo = workerViewModel.Extract();
                this.frontServiceClient.RemoveWorkerInfoAsync(workerInfo.Id);
                //mainWindowController.RemoveFromList(workerInfo);
                Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        try
                        {
                            this.WorkerCollectionViewModel.WorkerViewModelCollection.Remove(workerViewModel);

                            //var collection = this.WorkerCollectionViewModel.WorkCollection.Where(o => o.OrderId == orderInfo.Id).ToList();
                            //if (collection.Count > 0)
                            //{
                            //    foreach (var element in collection)
                            //    {
                            //        this.OrderCollectionViewModel.WorkCollection.Remove(element);
                            //    }
                            //}

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

        //public void Add(WorkerInfo workerInfo)
        //{
        //    this.WorkerCollectionViewModel.Add(workerInfo);
        //}

        protected virtual bool CanAddWorker()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanUpdateWorker()
        {
            bool valid = true;
            return valid;
        }

        protected virtual bool CanRemoveWork()
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
